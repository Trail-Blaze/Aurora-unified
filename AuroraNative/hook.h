#pragma once

#include "pch.h"

class VHook {
private:
	static uintptr_t lpTarget;
	static uintptr_t lpDetour;

	static PVOID hHandle;
	static DWORD dwProtect;

public:
	static BOOL Hook(uintptr_t lpTarget, uintptr_t lpDetour);
	static BOOL Unhook();

private:
	static LONG WINAPI Handler(EXCEPTION_POINTERS *pExceptionInfo);

	static BOOL IsSamePage(const uint8_t* pAddressFirst, const uint8_t* pAddressSecond);
};
