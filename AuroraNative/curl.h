#ifndef CURL_H
#define CURL_H

#include <Windows.h>
#include <iostream>

#include "util.h"
#include "hook.h"

LPVOID lpCurlSetopt;

LPVOID lpCurlEasySetopt;
VHook* pCurlEasySetoptHook;

LONG(*CurlSetopt)(LPVOID, INT, va_list) = nullptr;
LONG CurlSetoptVa(LPVOID lpContext, INT iOption, ...) {
    va_list pArg;

    LONG lResult;

    va_start(pArg, iOption);

    lResult = CurlSetopt(lpContext, iOption, pArg);

    va_end(pArg);

    return lResult;
}

LONG CurlEasySetopt(LPVOID lpContext, INT iTag, ...) {
    va_list pArg;

    LONG lResult;

    if (lpContext == nullptr)
        return 43; // CURLE_BAD_FUNCTION_ARGUMENT

    va_start(pArg, iTag);

    switch (iTag) {
    case 64: // CURLOPT_SSL_VERIFYPEER
        lResult = CurlSetoptVa(lpContext, 10004, "http://127.0.0.1:6000/"); // TODO (Cyuubi): Probably should de-hardcode this, eventually. (aka, never)
        lResult = CurlSetoptVa(lpContext, 64, false);
        break;

    default:
        lResult = CurlSetopt(lpContext, iTag, pArg);
        break;
    }

    va_end(pArg);

    return lResult;
}

VOID InitCurl() {
    auto pCurlSetoptAddress = Util::FindPattern("\x48\x89\x5C\x24\x08\x48\x89\x6C\x24\x10\x48\x89\x74\x24\x18\x57\x48\x83\xEC\x30\x33\xED\x49\x8B\xF0\x48\x8B\xD9", "xxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!pCurlSetoptAddress) {
        MessageBox(NULL, (LPCWSTR)L"Finding pattern for CurlSetopt has failed, please re-open Fortnite and try again!", (LPCWSTR)L"Error", MB_ICONERROR);
        exit(EXIT_FAILURE);
    }

#ifdef VERBOSE
    printf("pCurlSetoptAddress: %" PRIXPTR "\n", pCurlSetoptAddress);
#endif // VERBOSE

    auto pCurlEasySetoptAddress = Util::FindPattern("\x89\x54\x24\x10\x4C\x89\x44\x24\x18\x4C\x89\x4C\x24\x20\x48\x83\xEC\x28\x48\x85\xC9\x75\x08\x8D\x41\x2B\x48\x83\xC4\x28\xC3\x4C", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    if (!pCurlEasySetoptAddress) {
        MessageBox(NULL, (LPCWSTR)L"Finding pattern for CurlEasySetopt has failed, please re-open Fortnite and try again!", (LPCWSTR)L"Error", MB_ICONERROR);
        exit(EXIT_FAILURE);
    }

#ifdef VERBOSE
    printf("pCurlEasySetoptAddress: %" PRIXPTR "\n", pCurlEasySetoptAddress);
#endif // VERBOSE

    lpCurlSetopt = reinterpret_cast<LPVOID>(pCurlSetoptAddress);
    CurlSetopt = reinterpret_cast<decltype(CurlSetopt)>(lpCurlSetopt);

    lpCurlEasySetopt = reinterpret_cast<LPVOID>(pCurlEasySetoptAddress);

    pCurlEasySetoptHook = new VHook();
    pCurlEasySetoptHook->Hook((uintptr_t)lpCurlEasySetopt, (uintptr_t)CurlEasySetopt);
}

#endif // CURL_H
