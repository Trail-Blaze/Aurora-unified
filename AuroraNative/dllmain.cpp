#include "pch.h"

CURLcode (*Curl_setopt)(struct Curl_easy*, CURLoption, va_list) = nullptr;

CURLcode Curl_setopt_detour(struct Curl_easy* data, CURLoption option, va_list param) {
    switch (option) {
    case CURLOPT_URL:
        break;
    }

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

CURLcode (*curl_easy_setopt)(struct Curl_easy*, CURLoption, ...) = nullptr;

CURLcode curl_easy_setopt_detour(struct Curl_easy* data, CURLoption tag, ...) {
    va_list arg, arg_copy;

    CURLcode result;

    if (!data)
        return CURLE_BAD_FUNCTION_ARGUMENT;

    va_start(arg, tag);

    /*HKEY hKey = NULL;
    LONG lRes = RegOpenKeyExW(HKEY_CURRENT_USER, L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet", 0, KEY_READ, &hKey);

    if (lRes == ERROR_SUCCESS) {
        bool dwProxyEnable = false;

        GetBoolRegKey(hKey, L"ProxyEnable", dwProxyEnable, false);

        if (dwProxyEnable)
            exit(ERROR_INVALID_ADDRESS);
    }

    RegCloseKey(hKey);*/

    if (tag == CURLOPT_URL) {
        va_copy(arg_copy, arg);

        std::string url(va_arg(arg_copy, char*));
        size_t length = url.length();

        if (url.find(".epicgames.com") != std::string::npos) {
            Url redirect(url);

#ifdef LOCALHOST
            redirect.scheme("http").host("localhost");
#endif
#ifdef FDEV
            redirect.scheme("http").host("fortnite.dev");
#endif

            url = redirect.str();
        }

        // Add padding.
        for (size_t index = url.length(); index < length; index++)
            url = url + " ";

        //printf("curl_easy_setopt (va): tag = %i, url = %s\n", tag, url.c_str());

        result = Curl_setopt_va(data, tag, url.c_str());

        va_end(arg_copy);
#ifdef DISABLE_PINNING
    } else if (tag == CURLOPT_SSL_VERIFYPEER) {
        //printf("curl_easy_setopt (va): tag = %i\n", tag);

        result = Curl_setopt_va(data, tag, false);
#endif // DISABLE_PINNING
    } else {
        //printf("curl_easy_setopt (detour): tag = %i\n", tag);

        result = Curl_setopt_detour(data, tag, arg);
    }

    //printf("Curl_setopt: result = %i\n", result);

    va_end(arg);

	return result;
}

BOOL bVehHook = true;

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

    printf((base64_decode(NATIVE_AUTHOR) + "\n\n").c_str());

#ifdef FDEV
    printf((base64_decode(FDEV_CREDITS) + "\n").c_str());
    printf((base64_decode(FDEV_DISCORD) + "\n\n").c_str());
#endif // FDEV

    printf("Initializing MinHook... ");

    if (MH_Initialize() == MH_OK)
        printf("Success!\n\n");
    else {
        printf("Failed, bailing-out immediately!\n\n");
        return;
    }

    // CurlEasySetopt = 89 54 24 10 4C 89 44 24 18 4C 89 4C 24 20 48 83 EC 28 48 85 C9 75 08 8D 41 2B 48 83 C4 28 C3 4C
    // CurlSetopt = 48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 30 33 ED 49 8B F0 48 8B D9

#ifdef FDEV
    MODULEINFO info = { 0 };

    GetModuleInformation(GetCurrentProcess(), GetModuleHandle(0), &info, sizeof(info));
#endif

#ifdef FDEV
    auto lpCurlEasySetoptAddress = reinterpret_cast<PBYTE>(info.lpBaseOfDll) + 0x54C9BD0;
#else
    auto lpCurlEasySetoptAddress = FindPattern("\x89\x54\x24\x10\x4C\x89\x44\x24\x18\x4C\x89\x4C\x24\x20\x48\x83\xEC\x28\x48\x85\xC9\x75\x08\x8D\x41\x2B\x48\x83\xC4\x28\xC3\x4C", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!lpCurlEasySetoptAddress) {
        printf("Finding pattern for CurlEasySetopt has failed, bailing-out immediately!");
        return;
    }
#endif

#ifdef VERBOSE
    printf("lpCurlEasySetoptAddress: %" PRIXPTR "\n", lpCurlEasySetoptAddress);
#endif // VERBOSE

#ifdef FDEV
    auto lpCurlSetoptAddress = reinterpret_cast<PBYTE>(info.lpBaseOfDll) + 0x54D8F10;
#else
    auto lpCurlSetoptAddress = FindPattern("\x48\x89\x5C\x24\x08\x48\x89\x6C\x24\x10\x48\x89\x74\x24\x18\x57\x48\x83\xEC\x30\x33\xED\x49\x8B\xF0\x48\x8B\xD9", "xxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!lpCurlSetoptAddress) {
        printf("Finding pattern for CurlSetopt has failed, bailing-out immediately!");
        return;
    }
#endif

#ifdef VERBOSE
    printf("lpCurlSetoptAddress: %" PRIXPTR "\n\n", lpCurlSetoptAddress);
#endif // VERBOSE

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
