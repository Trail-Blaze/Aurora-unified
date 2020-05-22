#include "pch.h"

CURLcode (*Curl_setopt)(struct Curl_easy*, CURLoption, va_list) = nullptr;

// Thanks to Eldor for helping me!
CURLcode Curl_setopt_detour(struct Curl_easy* data, CURLoption option, va_list param) {
    std::string url;

    switch (option) {
    case CURLOPT_URL:
        url = std::string(reinterpret_cast<char*>(param));

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

        param = reinterpret_cast<va_list>((char*)url.c_str());
        break;
    case CURLOPT_USE_SSL:
        param = reinterpret_cast<va_list>(1L);
        break;
    case CURLOPT_SSL_VERIFYPEER:
        param = reinterpret_cast<va_list>(0L);
        break;
    }

    auto result = Curl_setopt(data, option, param);

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

    // CurlSetopt = 48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 30 33 ED 49 8B F0 48 8B D9

#ifdef FDEV
    MODULEINFO info = { 0 };

    GetModuleInformation(GetCurrentProcess(), GetModuleHandle(0), &info, sizeof(info));
#endif

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

    LPVOID lpCurlSetopt = reinterpret_cast<LPVOID>(lpCurlSetoptAddress);

    // TODO: Add checks for hooks.

    //MH_CreateHook(lpCurlSetopt, Curl_setopt_detour, reinterpret_cast<LPVOID*>(&Curl_setopt));
    Curl_setopt = reinterpret_cast<decltype(Curl_setopt)>(lpCurlSetopt);

    EnableHook(lpCurlSetopt, Curl_setopt_detour);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpReserved) {
    if (dwReason == DLL_PROCESS_ATTACH) {
        Main();
    }

    return TRUE;
}
