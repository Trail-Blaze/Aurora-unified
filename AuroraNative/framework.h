#pragma once

//#define WIN32_LEAN_AND_MEAN
#include <Windows.h>

#include <iostream>
#include <sstream>
#include <string>

#include <inttypes.h>
#include <psapi.h>

#include "curl.h" // TODO: Should be cURL 7.55.1, I'll fix this sometime. (Or, never.)

#include <MinHook.h>
#pragma comment(lib, "minhook.lib")

#include "veh.h"