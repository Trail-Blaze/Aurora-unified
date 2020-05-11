#pragma once

#include "pch.h"

class VehHook {
public:
	static bool Run(uintptr_t lpTarget, uintptr_t lpDetour);
	static bool Unhook();

private:
	static uintptr_t lpTarget;
	static uintptr_t lpDetour;

	static PVOID hHandle;
	static DWORD dwProtect;

	static LONG WINAPI Handler(EXCEPTION_POINTERS *pExceptionInfo);

	static bool IsSamePage(const uint8_t* pAddressFirst, const uint8_t* pAddressSecond);
};
