// CodeForCppDll.cpp: 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"

double TestMethod_Array(double _array[], int _size)
{
	double sumArray = 0;

	for (int i = 0; i < _size; i++)
	{
		sumArray += _array[i];
	}
	return sumArray;
}

int TryCatchTest(int _choice)
{
	switch (_choice)
	{
	case 1:
		return -1;
		break;
	case 2:
		return -2;
		break;
	default:
		return 0;
		break;
	}
}