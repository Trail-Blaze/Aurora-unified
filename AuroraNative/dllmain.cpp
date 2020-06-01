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
#ifdef ONLINE
            redirect.host("aurorafn.dev");
#endif // ONLINE

            url = redirect.str();
        }

#ifdef VERBOSE
        printf("CurlEasySetopt (va): tag = %i, url = %s\n", tag, url.c_str());
#endif // VERBOSE

        result = CurlSetoptVa(data, tag, url.c_str());

        va_end(copy);
#ifdef DISABLE_PINNING
    } else if (tag == CURLOPT_SSL_VERIFYPEER) {
#ifdef VERBOSE
        printf("CurlEasySetopt (va): tag = %i\n", tag);
#endif // VERBOSE

        result = CurlSetoptVa(data, tag, false);
#endif // DISABLE_PINNING
#ifdef DISABLE_PROXY
    } else if (tag == CURLOPT_PROXY) {
#ifdef VERBOSE
        printf("CurlEasySetopt (va): tag = %i\n", tag);
#endif // VERBOSE

        result = CurlSetoptVa(data, tag, "");
#endif // DISABLE_PROXY
    } else if (tag == 1337) {
#ifdef VERBOSE
        printf("CurlEasySetopt: tag = %i\n", tag);
#endif // VERBOSE

        printf("\nYou usually will never see this message whilest running Aurora, but I've seen that you're reverse engineering my hard work... Why?\n");
        printf("It took me a month to create AuroraNative, I don't have any money to upgrade my computer.\n");
        printf("I guess this is my only way to communicate to you \"skids/hackers\", but can you please stop?\n");
        printf("I understand, you want to check out my code. But, you're just going to cause more trouble than good.\n");
        printf("I don't want to be credited in your skidded version of my work, all it does is cause more confusion.\n");
        printf("Please don't distribute modified versions of my hard work, it's fine if you want to do it in private.\n");
        printf("But, don't tell anybody how to do so. You're just spoonfeeding them, it doesn't help anybody.\n");
        printf("Kemo if you remove this message, then you're just as scummy as the others.\n");
        printf("Thank you, for complying. I hope we can talk, soon. -Cyuubi\n\n");

        result = CurlSetopt(data, tag, arg);
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

VOID Main() {
    AllocConsole();

    FILE* pFile;
    freopen_s(&pFile, "CONOUT$", "w", stdout);

    printf("Aurora, made with <3 by Cyuubi and Slushia.\n");
    printf("Discord: https://discord.gg/aurorafn\n\n");

    // CurlEasySetopt = 89 54 24 10 4C 89 44 24 18 4C 89 4C 24 20 48 83 EC 28 48 85 C9 75 08 8D 41 2B 48 83 C4 28 C3 4C
    // CurlSetopt = 48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 30 33 ED 49 8B F0 48 8B D9

    auto lpCurlEasySetoptAddress = Util::FindPattern("\x89\x54\x24\x10\x4C\x89\x44\x24\x18\x4C\x89\x4C\x24\x20\x48\x83\xEC\x28\x48\x85\xC9\x75\x08\x8D\x41\x2B\x48\x83\xC4\x28\xC3\x4C", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!lpCurlEasySetoptAddress) {
        printf("Finding pattern for CurlEasySetopt has failed, bailing-out immediately!");
        return;
    }

#ifdef VERBOSE
    printf("lpCurlEasySetoptAddress: %" PRIXPTR "\n", lpCurlEasySetoptAddress);
#endif // VERBOSE

    auto lpCurlSetoptAddress = Util::FindPattern("\x48\x89\x5C\x24\x08\x48\x89\x6C\x24\x10\x48\x89\x74\x24\x18\x57\x48\x83\xEC\x30\x33\xED\x49\x8B\xF0\x48\x8B\xD9", "xxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!lpCurlSetoptAddress) {
        printf("Finding pattern for CurlSetopt has failed, bailing-out immediately!");
        return;
    }

#ifdef VERBOSE
    printf("lpCurlSetoptAddress: %" PRIXPTR "\n\n", lpCurlSetoptAddress);
#endif // VERBOSE

    LPVOID lpCurlEasySetopt = reinterpret_cast<LPVOID>(lpCurlEasySetoptAddress);
    LPVOID lpCurlSetopt = reinterpret_cast<LPVOID>(lpCurlSetoptAddress);

    (new VHook())->Hook((uintptr_t)lpCurlEasySetopt, (uintptr_t)CurlEasySetopt);

    CurlSetopt = reinterpret_cast<decltype(CurlSetopt)>(lpCurlSetopt);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpReserved) {
    if (dwReason == DLL_PROCESS_ATTACH) {
        Main();
    }

    return TRUE;
}
