#include "pch.h"

#include "util.h"
#include "hooks.h"

VOID Main() {
    Util::InitConsole();

    printf("Aurora, made with <3 by Cyuubi and Slushia.\n");
    printf("Discord: https://discord.gg/AuroraFN\n\n");

    InitHooks();
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpReserved) {
    if (dwReason == DLL_PROCESS_ATTACH)
        Main();

    return TRUE;
}
