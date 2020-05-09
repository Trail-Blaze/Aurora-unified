#include "pch.h"
#include <vector>

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

        //printf("curl_easy_setopt: tag = %i, url = %s\n", tag, url.c_str());

        result = Curl_setopt_va(data, tag, url.c_str());

        va_end(arg_copy);
    } else if (tag == CURLOPT_SSL_VERIFYPEER) {
        //printf("curl_easy_setopt: tag = %i\n", tag);

        result = Curl_setopt_va(data, tag, false);
    } else {
        //printf("curl_easy_setopt: tag = %i\n", tag);

        result = Curl_setopt_detour(data, tag, arg);
    }

    //printf("Curl_setopt: result = %i\n", result);

    va_end(arg);

	return result;
}

BOOL bVehHook = false;

VOID AN_EnableHook(LPVOID pTarget, LPVOID pDetour) {
    if (!bVehHook)
        MH_EnableHook(pTarget);
    else {
        if (pDetour != NULL)
            (new VehHook())->Run((uintptr_t)pTarget, (uintptr_t)pDetour);
    }
}

VOID Main() {
    AllocConsole();

    FILE* pFile;
    freopen_s(&pFile, "CONOUT$", "w", stdout);

    if (/* TODO: Re-add configuration, in the future. */ true)
        bVehHook = true;

    printf("bVehHook: %d\n", bVehHook);

    uintptr_t pBaseAddress = (uintptr_t)GetModuleHandle(TEXT("FortniteClient-Win64-Shipping.exe"));
    printf("pBaseAddress: %" PRIXPTR "\n", pBaseAddress);

    // CurlEasySetopt = 89 54 24 10 4C 89 44 24 18 4C 89 4C 24 20 48 83 EC 28 48 85 C9 75 08 8D 41 2B 48 83 C4 28 C3 4C
    // CurlSetopt = 48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 30 33 ED 49 8B F0 48 8B D9

    // 0x3863D40 - CurlEasySetopt (1.8)
    // 0x386FB80 - CurlSetopt (1.8)

    LPVOID lpCurlEasySetopt = reinterpret_cast<LPVOID>(pBaseAddress + 0x545E340);
    LPVOID lpCurlSetopt = reinterpret_cast<LPVOID>(pBaseAddress + 0x546D680);

    //MH_CreateHook(lpCurlEasySetopt, curl_easy_setopt_detour, reinterpret_cast<LPVOID*>(&curl_easy_setopt));
    AN_EnableHook(lpCurlEasySetopt, curl_easy_setopt_detour);

    MH_CreateHook(lpCurlSetopt, Curl_setopt_detour, reinterpret_cast<LPVOID*>(&Curl_setopt));
    //AN_EnableHook(lpCurlSetopt, Curl_setopt_detour_list);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpReserved) {
    if (dwReason == DLL_PROCESS_ATTACH) {
        Main();
    }

    return TRUE;
}
