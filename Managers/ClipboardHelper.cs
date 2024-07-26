using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public static class ClipboardHelper
{
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool CloseClipboard();

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool IsClipboardFormatAvailable(uint format);

    [DllImport("user32.dll")]
    public static extern IntPtr GetClipboardData(uint uFormat);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);

    [DllImport("user32.dll")]
    public static extern bool EmptyClipboard();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GlobalLock(IntPtr hMem);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GlobalUnlock(IntPtr hMem);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GlobalFree(IntPtr hMem);

    [DllImport("kernel32.dll")]
    public static extern UIntPtr GlobalSize(IntPtr hMem);

    public const uint CF_TEXT = 1;
    public const uint GMEM_MOVEABLE = 0x0002;

    public static string GetText()
    {
        if (!OpenClipboard(IntPtr.Zero))
            return null;

        string data = null;
        if (IsClipboardFormatAvailable(CF_TEXT))
        {
            IntPtr handle = GetClipboardData(CF_TEXT);
            if (handle != IntPtr.Zero)
            {
                IntPtr pointer = GlobalLock(handle);
                if (pointer != IntPtr.Zero)
                {
                    data = Marshal.PtrToStringAnsi(pointer);
                    GlobalUnlock(handle);
                }
            }
        }
        CloseClipboard();
        return data;
    }

    public static bool SetText(string text)
    {
        if (!OpenClipboard(IntPtr.Zero))
            return false;

        EmptyClipboard();
        IntPtr hGlobal = Marshal.StringToHGlobalAnsi(text);
        if (SetClipboardData(CF_TEXT, hGlobal) == IntPtr.Zero)
        {
            Marshal.FreeHGlobal(hGlobal);
            CloseClipboard();
            return false;
        }

        CloseClipboard();
        return true;
    }
}
