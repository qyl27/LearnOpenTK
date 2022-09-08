using System;
using System.Runtime.InteropServices;

namespace LearnOpenTK.Utilities;

public static class UnmanagedArray
{
    public static IntPtr ToUnmanagedFloatArray(int count, float[] arr)
    {
        var ptr = Marshal.AllocHGlobal(count * sizeof(float));
        Marshal.Copy(ptr, arr, 0, count);
        return ptr;
    }
}