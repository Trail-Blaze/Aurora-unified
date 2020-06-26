#include <Windows.h>

#include "util.h"
#include "curl.h"

VOID Main() {
//#ifdef VERBOSE
    Util::InitConsole();
//#endif

    InitCurl();
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpReserved) {
    if (dwReason == DLL_PROCESS_ATTACH)
        Main();

    return TRUE;
}
