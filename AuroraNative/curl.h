#ifndef HOOKS_H
#define HOOKS_H

#include "pch.h"

#include "hook.h"
#include "url.hpp"

LPVOID lpCurlSetopt;
LPVOID lpCurlEasySetopt;

VHook* pCurlEasySetoptHook;

LONG(*CurlSetopt)(LPVOID, INT, va_list) = nullptr;

inline LONG CurlSetoptVa(LPVOID lpContext, INT iOption, ...) {
    va_list arg;
    LONG result;

    va_start(arg, iOption);

    result = CurlSetopt(lpContext, iOption, arg);

    va_end(arg);

    return result;
}

inline LONG CurlEasySetopt(LPVOID lpContext, INT iTag, ...) {
    va_list arg, copy;
    LONG result;

    if (lpContext == nullptr)
        return 43; // CURLE_BAD_FUNCTION_ARGUMENT

    va_start(arg, iTag);

    if (IsDebuggerPresent())
        exit(EXIT_FAILURE);

    if (iTag == 10002) { // CURLOPT_URL
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

        result = CurlSetoptVa(lpContext, iTag, url.c_str());

        va_end(copy);
#ifdef DISABLE_PINNING
    } else if (iTag == 64) { // CURLOPT_SSL_VERIFYPEER
#ifdef VERBOSE
        printf("CurlEasySetopt (va): tag = %i\n", tag);
#endif // VERBOSE

        result = CurlSetoptVa(lpContext, iTag, false);
#endif // DISABLE_PINNING
#ifdef DISABLE_PROXY
    } else if (iTag == 10004) { // CURLOPT_PROXY
#ifdef VERBOSE
        printf("CurlEasySetopt (va): tag = %i\n", tag);
#endif // VERBOSE

        result = CurlSetoptVa(lpContext, iTag, "");
#endif // DISABLE_PROXY
    } else {
#ifdef VERBOSE
        printf("CurlEasySetopt: tag = %i\n", tag);
#endif // VERBOSE

        result = CurlSetopt(lpContext, iTag, arg);
    }

#ifdef VERBOSE
    printf("CurlSetopt: result = %i\n", result);
#endif // VERBOSE

    va_end(arg);

    return result;
}

VOID InitCurl() {
    auto pCurlEasySetoptAddress = Util::FindPattern("\x89\x54\x24\x10\x4C\x89\x44\x24\x18\x4C\x89\x4C\x24\x20\x48\x83\xEC\x28\x48\x85\xC9\x75\x08\x8D\x41\x2B\x48\x83\xC4\x28\xC3\x4C", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!pCurlEasySetoptAddress) {
        printf("Finding pattern for CurlEasySetopt has failed, exiting immediately!\n");
        exit(EXIT_FAILURE);
    }

#ifdef VERBOSE
    printf("lpCurlEasySetoptAddress: %" PRIXPTR "\n", lpCurlEasySetoptAddress);
#endif // VERBOSE

    auto pCurlSetoptAddress = Util::FindPattern("\x48\x89\x5C\x24\x08\x48\x89\x6C\x24\x10\x48\x89\x74\x24\x18\x57\x48\x83\xEC\x30\x33\xED\x49\x8B\xF0\x48\x8B\xD9", "xxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!pCurlSetoptAddress) {
        printf("Finding pattern for CurlSetopt has failed, exiting immediately!\n");
        exit(EXIT_FAILURE);
    }

#ifdef VERBOSE
    printf("lpCurlSetoptAddress: %" PRIXPTR "\n", lpCurlSetoptAddress);
#endif // VERBOSE

    lpCurlSetopt = reinterpret_cast<LPVOID>(pCurlSetoptAddress);
    lpCurlEasySetopt = reinterpret_cast<LPVOID>(pCurlEasySetoptAddress);

    pCurlEasySetoptHook = new VHook();

    CurlSetopt = reinterpret_cast<decltype(CurlSetopt)>(lpCurlSetopt);

    pCurlEasySetoptHook->Hook((uintptr_t)lpCurlEasySetopt, (uintptr_t)CurlEasySetopt);
}

#endif // HOOKS_H
