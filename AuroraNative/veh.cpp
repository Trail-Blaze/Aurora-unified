#include "pch.h"

#define XIP Rip

uintptr_t VehHook::lpTarget = 0;
uintptr_t VehHook::lpDetour = 0;

PVOID VehHook::hHandle = nullptr;
DWORD VehHook::dwProtect = 0;

bool VehHook::Run(uintptr_t lpTarget, uintptr_t lpDetour)
{
	VehHook::lpTarget = lpTarget;
	VehHook::lpDetour = lpDetour;

	if (IsSamePage((const uint8_t*)lpTarget, (const uint8_t*)lpDetour))
		return false;

	hHandle = AddVectoredExceptionHandler(true, (PVECTORED_EXCEPTION_HANDLER)Handler);

	if (hHandle && VirtualProtect((LPVOID)lpTarget, 1, PAGE_EXECUTE_READ | PAGE_GUARD, &dwProtect))
		return true;

	return false;
}

bool VehHook::Unhook()
{
	DWORD dwOldProtect;

	if (hHandle && VirtualProtect((LPVOID)lpTarget, 1, dwProtect, &dwOldProtect) && RemoveVectoredExceptionHandler(hHandle))
		return true;

	return false;
}

LONG WINAPI VehHook::Handler(EXCEPTION_POINTERS* pExceptionInfo)
{
	if (pExceptionInfo->ExceptionRecord->ExceptionCode == STATUS_GUARD_PAGE_VIOLATION)
	{
		if (pExceptionInfo->ContextRecord->XIP == (uintptr_t)lpTarget)
			pExceptionInfo->ContextRecord->XIP = (uintptr_t)lpDetour;

		pExceptionInfo->ContextRecord->EFlags |= 0x100;

		return EXCEPTION_CONTINUE_EXECUTION;
	}

	if (pExceptionInfo->ExceptionRecord->ExceptionCode == STATUS_SINGLE_STEP)
	{
		DWORD dwOldProtect;

		VirtualProtect((LPVOID)lpTarget, 1, PAGE_EXECUTE_READ | PAGE_GUARD, &dwOldProtect);

		return EXCEPTION_CONTINUE_EXECUTION;
	}

	return EXCEPTION_CONTINUE_SEARCH;
}

bool VehHook::IsSamePage(const uint8_t* pAddressFirst, const uint8_t* pAddressSecond)
{
	MEMORY_BASIC_INFORMATION mbiFirst, mbiSecond;

	if (!VirtualQuery(pAddressFirst, &mbiFirst, sizeof(mbiFirst)))
		return true;

	if (!VirtualQuery(pAddressSecond, &mbiSecond, sizeof(mbiSecond)))
		return true;

	if (mbiFirst.BaseAddress == mbiSecond.BaseAddress)
		return true;

	return false;
}
