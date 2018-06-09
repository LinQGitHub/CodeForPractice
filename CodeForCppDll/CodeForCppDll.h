#pragma once
//if exports "#ifdef N8241A_EXPORTS",
#define CodeForCppDll_EXPORTS

#ifdef CodeForCppDll_EXPORTS
#define CodeForCppDll_API __declspec(dllexport)   
#else  
#define CodeForCppDll_API __declspec(dllimport)   
#endif

extern "C"
{
	CodeForCppDll_API double TestMethod_Array(double _array[], int _size);
	CodeForCppDll_API int TryCatchTest(int _choice);
}

