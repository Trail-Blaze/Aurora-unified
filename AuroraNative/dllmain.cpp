#include "pch.h"

CURLcode (*Curl_setopt)(struct Curl_easy*, CURLoption, va_list) = nullptr;

CURLcode Curl_setopt_detour(struct Curl_easy* data, CURLoption option, va_list param) {
    return Curl_setopt(data, option, param);
}

CURLcode Curl_setopt_va(struct Curl_easy* data, CURLoption option, ...) {
    va_list arg, arg_copy;

    CURLcode result;

    va_start(arg, option);

    result = Curl_setopt_detour(data, option, arg);

    va_end(arg);

    return result;
}

bool replace(std::string& value, const std::string& from, const std::string& to) {
    size_t lStartPosition = value.find(from);

    if (lStartPosition == std::string::npos)
        return false;

    value.replace(lStartPosition, from.length(), to);

    return true;
}

CURLcode (*curl_easy_setopt)(struct Curl_easy*, CURLoption, ...) = nullptr;

CURLcode curl_easy_setopt_detour(struct Curl_easy* data, CURLoption tag, ...) {
    va_list arg, arg_copy;

    CURLcode result;

    if (!data)
        return CURLE_BAD_FUNCTION_ARGUMENT;

    va_start(arg, tag);

    if (tag == CURLOPT_URL) {
        va_copy(arg_copy, arg);

        std::string url(va_arg(arg_copy, char*));

        //printf("curl_easy_setopt (va): tag = %i, url = %s\n", tag, url.c_str());

        result = Curl_setopt_va(data, tag, url.c_str());

        va_end(arg_copy);
    } else if (tag == CURLOPT_SSL_VERIFYPEER) {
        //printf("curl_easy_setopt (va): tag = %i\n", tag);

        result = Curl_setopt_va(data, tag, false);
    } else {
        //printf("curl_easy_setopt (detour): tag = %i\n", tag);

        result = Curl_setopt_detour(data, tag, arg);
    }

    //printf("Curl_setopt: result = %i\n", result);

    va_end(arg);

	return result;
}

BOOL bVehHook = false;

VOID EnableHook(LPVOID lpTarget, LPVOID lpDetour) {
    if (!bVehHook)
        MH_EnableHook(lpTarget);
    else {
        if (lpDetour != NULL)
            (new VehHook())->Run((uintptr_t)lpTarget, (uintptr_t)lpDetour);
    }
}

BOOL MaskCompare(PVOID pBuffer, LPCSTR lpPattern, LPCSTR lpMask) {
    for (auto value = reinterpret_cast<PBYTE>(pBuffer); *lpMask; ++lpPattern, ++lpMask, ++value) {
        if (*lpMask == 'x' && *reinterpret_cast<LPCBYTE>(lpPattern) != *value)
            return FALSE;
    }

    return TRUE;
}

PBYTE FindPattern(PVOID pBase, DWORD dwSize, LPCSTR lpPattern, LPCSTR lpMask) {
    dwSize -= static_cast<DWORD>(strlen(lpMask));

    for (auto index = 0UL; index < dwSize; ++index) {
        auto pAddress = reinterpret_cast<PBYTE>(pBase) + index;

        if (MaskCompare(pAddress, lpPattern, lpMask))
            return pAddress;
    }

    return NULL;
}

PBYTE FindPattern(LPCSTR lpPattern, LPCSTR lpMask) {
    MODULEINFO info = { 0 };

    GetModuleInformation(GetCurrentProcess(), GetModuleHandle(0), &info, sizeof(info));

    return FindPattern(info.lpBaseOfDll, info.SizeOfImage, lpPattern, lpMask);
}

VOID Main() {
    AllocConsole();

    FILE* pFile;
    freopen_s(&pFile, "CONOUT$", "w", stdout);

    printf("Initializing MinHook... ");

    if (MH_Initialize() == MH_OK)
        printf("Success!\n");
    else {
        printf("Failed, bailing-out immediately!\n");
        return;
    }

    if (/* TODO: Re-add configuration, in the future. */ true)
        bVehHook = true;

    printf("bVehHook: %d\n", bVehHook);

    // CurlEasySetopt = 89 54 24 10 4C 89 44 24 18 4C 89 4C 24 20 48 83 EC 28 48 85 C9 75 08 8D 41 2B 48 83 C4 28 C3 4C
    // CurlSetopt = 48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 30 33 ED 49 8B F0 48 8B D9

    auto lpCurlEasySetoptAddress = FindPattern("\x89\x54\x24\x10\x4C\x89\x44\x24\x18\x4C\x89\x4C\x24\x20\x48\x83\xEC\x28\x48\x85\xC9\x75\x08\x8D\x41\x2B\x48\x83\xC4\x28\xC3\x4C", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!lpCurlEasySetoptAddress) {
        printf("Finding pattern for CurlEasySetopt has failed, bailing-out immediately!\n");
        return;
    }

    printf("lpCurlEasySetoptAddress: %" PRIXPTR "\n", lpCurlEasySetoptAddress);

    auto lpCurlSetoptAddress = FindPattern("\x48\x89\x5C\x24\x08\x48\x89\x6C\x24\x10\x48\x89\x74\x24\x18\x57\x48\x83\xEC\x30\x33\xED\x49\x8B\xF0\x48\x8B\xD9", "xxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!lpCurlSetoptAddress) {
        printf("Finding pattern for CurlSetopt has failed, bailing-out immediately!\n");
        return;
    }

    printf("lpCurlSetoptAddress: %" PRIXPTR "\n", lpCurlSetoptAddress);

    LPVOID lpCurlEasySetopt = reinterpret_cast<LPVOID>(lpCurlEasySetoptAddress);
    LPVOID lpCurlSetopt = reinterpret_cast<LPVOID>(lpCurlSetoptAddress);

    // TODO: Add checks for hooks.

    //MH_CreateHook(lpCurlEasySetopt, curl_easy_setopt_detour, reinterpret_cast<LPVOID*>(&curl_easy_setopt));
    EnableHook(lpCurlEasySetopt, curl_easy_setopt_detour);

    MH_CreateHook(lpCurlSetopt, Curl_setopt_detour, reinterpret_cast<LPVOID*>(&Curl_setopt));
    //EnableHook(lpCurlSetopt, Curl_setopt_detour_list);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpReserved) {
    if (dwReason == DLL_PROCESS_ATTACH) {
        Main();
    }

    return TRUE;
}
