#pragma once

#include "pch.h"

class VehHook {
public:
	static bool Run(uintptr_t pTarget, uintptr_t pDetour);
	static bool Unhook();

private:
	static uintptr_t pTarget;
	static uintptr_t pDetour;

	static PVOID hHandle;
	static DWORD dwProtect;

	static LONG WINAPI Handler(EXCEPTION_POINTERS *pExceptionInfo);

	static bool IsSamePage(const uint8_t* pAddressFirst, const uint8_t* pAddressSecond);
};
