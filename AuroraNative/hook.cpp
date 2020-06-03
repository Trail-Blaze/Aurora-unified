#include "pch.h"

#define XIP Rip

uintptr_t VHook::lpTarget = 0;
uintptr_t VHook::lpDetour = 0;

PVOID VHook::hHandle = nullptr;
DWORD VHook::dwProtect = 0;

BOOL VHook::Hook(uintptr_t lpTarget = 0, uintptr_t lpDetour = 0)
{
	if (lpTarget != 0)
		VHook::lpTarget = lpTarget;
	if (lpDetour != 0)
		VHook::lpDetour = lpDetour;

	if (IsSamePage((const uint8_t*)lpTarget, (const uint8_t*)lpDetour))
		return false;

	hHandle = AddVectoredExceptionHandler(true, (PVECTORED_EXCEPTION_HANDLER)Handler);

	if (hHandle && VirtualProtect((LPVOID)lpTarget, 1, PAGE_EXECUTE_READ | PAGE_GUARD, &dwProtect))
		return true;

	return false;
}

BOOL VHook::Unhook()
{
	DWORD dwOldProtect;

	if (hHandle && VirtualProtect((LPVOID)lpTarget, 1, dwProtect, &dwOldProtect) && RemoveVectoredExceptionHandler(hHandle))
		return true;

	return false;
}

LONG WINAPI VHook::Handler(EXCEPTION_POINTERS* pExceptionInfo)
{
	DWORD dwOldProtect;

	switch (pExceptionInfo->ExceptionRecord->ExceptionCode) {
		case STATUS_GUARD_PAGE_VIOLATION:
			if (pExceptionInfo->ContextRecord->XIP == (uintptr_t)lpTarget)
				pExceptionInfo->ContextRecord->XIP = (uintptr_t)lpDetour;

			pExceptionInfo->ContextRecord->EFlags |= 0x100;
			return EXCEPTION_CONTINUE_EXECUTION;

		case STATUS_SINGLE_STEP:
			VirtualProtect((LPVOID)lpTarget, 1, PAGE_EXECUTE_READ | PAGE_GUARD, &dwOldProtect);
			return EXCEPTION_CONTINUE_EXECUTION;

		default:
			break;
	}

	return EXCEPTION_CONTINUE_SEARCH;
}

BOOL VHook::IsSamePage(const uint8_t* pAddressFirst, const uint8_t* pAddressSecond)
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
