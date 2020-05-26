#include "pch.h"

CURLcode (*CurlSetopt)(struct Curl_easy*, CURLoption, va_list) = nullptr;

CURLcode CurlSetoptVa(struct Curl_easy* data, CURLoption option, ...) {
    va_list arg;

    CURLcode result;

    va_start(arg, option);

    result = CurlSetopt(data, option, arg);

    va_end(arg);

    return result;
}

CURLcode CurlEasySetopt(struct Curl_easy* data, CURLoption tag, ...) {
    va_list arg, copy;

    CURLcode result;

    if (!data)
        return CURLE_BAD_FUNCTION_ARGUMENT;

    va_start(arg, tag);

    if (tag == CURLOPT_URL) {
        va_copy(copy, arg);

        std::string url(va_arg(copy, char*));

        if (url.find(".epicgames.com") != std::string::npos) {
            Url redirect(url);

#ifdef LOCALHOST
            redirect.scheme("http").host("localhost");
#endif // LOCALHOST
#ifdef FDEV
            std::string sDomain(FDEV_DOMAIN);

            char* pDomain = (char*)malloc(sDomain.length());

            rc4((char*)std::string(DOMAIN_KEY).c_str(), (char*)sDomain.c_str(), (unsigned char*)pDomain);

            redirect.scheme("http").host(pDomain);

            free(pDomain);
#endif // FDEV

            url = redirect.str();
        }

#ifdef VERBOSE
        printf("CurlEasySetopt (va): tag = %i, url = %s\n", tag, url.c_str());
#endif

        result = CurlSetoptVa(data, tag, url.c_str());

        va_end(copy);
#ifdef DISABLE_PINNING
    } else if (tag == CURLOPT_SSL_VERIFYPEER) {
#ifdef VERBOSE
        printf("CurlEasySetopt (va): tag = %i\n", tag);
#endif

        result = CurlSetoptVa(data, tag, false);
#endif // DISABLE_PINNING
    } else {
#ifdef VERBOSE
        printf("CurlEasySetopt: tag = %i\n", tag);
#endif // VERBOSE

        result = CurlSetopt(data, tag, arg);
    }

#ifdef VERBOSE
    printf("CurlSetopt: result = %i\n", result);
#endif // VERBOSE

    va_end(arg);

	return result;
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

    // Native Author
    std::string sAuthor(NATIVE_AUTHOR);

    char* pAuthor = (char*)malloc(sAuthor.length());

    rc4((char*)std::string(COMMON_KEY).c_str(), (char*)sAuthor.c_str(), (unsigned char*)pAuthor);

    printf((std::string(pAuthor) + "\n\n").c_str());

    free(pAuthor);

#ifdef FDEV
    // FDev Credits
    std::string sCredits(FDEV_CREDITS);

    char* pCredits = (char*)malloc(sCredits.length());

    rc4((char*)std::string(COMMON_KEY).c_str(), (char*)sCredits.c_str(), (unsigned char*)pCredits);

    printf((std::string(pCredits) + "\n").c_str());

    free(pCredits);

    // FDev Discord
    std::string sDiscord(FDEV_DISCORD);

    char* pDiscord = (char*)malloc(sDiscord.length());

    rc4((char*)std::string(COMMON_KEY).c_str(), (char*)sDiscord.c_str(), (unsigned char*)pDiscord);

    printf((std::string(pDiscord) + "\n\n").c_str());

    free(pDiscord);
#endif // FDEV

    // CurlEasySetopt = 89 54 24 10 4C 89 44 24 18 4C 89 4C 24 20 48 83 EC 28 48 85 C9 75 08 8D 41 2B 48 83 C4 28 C3 4C
    // CurlSetopt = 48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 30 33 ED 49 8B F0 48 8B D9

    auto lpCurlEasySetoptAddress = FindPattern("\x89\x54\x24\x10\x4C\x89\x44\x24\x18\x4C\x89\x4C\x24\x20\x48\x83\xEC\x28\x48\x85\xC9\x75\x08\x8D\x41\x2B\x48\x83\xC4\x28\xC3\x4C", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!lpCurlEasySetoptAddress) {
        printf("Finding pattern for CurlEasySetopt has failed, bailing-out immediately!");
        return;
    }

#ifdef VERBOSE
    printf("lpCurlEasySetoptAddress: %" PRIXPTR "\n", lpCurlEasySetoptAddress);
#endif // VERBOSE

    auto lpCurlSetoptAddress = FindPattern("\x48\x89\x5C\x24\x08\x48\x89\x6C\x24\x10\x48\x89\x74\x24\x18\x57\x48\x83\xEC\x30\x33\xED\x49\x8B\xF0\x48\x8B\xD9", "xxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!lpCurlSetoptAddress) {
        printf("Finding pattern for CurlSetopt has failed, bailing-out immediately!");
        return;
    }

#ifdef VERBOSE
    printf("lpCurlSetoptAddress: %" PRIXPTR "\n\n", lpCurlSetoptAddress);
#endif // VERBOSE

    LPVOID lpCurlEasySetopt = reinterpret_cast<LPVOID>(lpCurlEasySetoptAddress);
    LPVOID lpCurlSetopt = reinterpret_cast<LPVOID>(lpCurlSetoptAddress);

    (new VehHook())->Run((uintptr_t)lpCurlEasySetopt, (uintptr_t)CurlEasySetopt);

    CurlSetopt = reinterpret_cast<decltype(CurlSetopt)>(lpCurlSetopt);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpReserved) {
    if (dwReason == DLL_PROCESS_ATTACH) {
        Main();
    }

    return TRUE;
}
