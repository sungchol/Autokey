using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Interop;

namespace AutoKey
{
    public enum HookType
    {
        WH_JOURNALRECORD = 0,
        WH_JOURNALPLAYBACK = 1,
        WH_KEYBOARD = 2,
        WH_GETMESSAGE = 3,
        WH_CALLWNDPROC = 4,
        WH_CBT = 5,
        WH_SYSMSGFILTER = 6,
        WH_MOUSE = 7,
        WH_HARDWARE = 8,
        WH_DEBUG = 9,
        WH_SHELL = 10,
        WH_FOREGROUNDIDLE = 11,
        WH_CALLWNDPROCRET = 12,
        WH_KEYBOARD_LL = 13,
        WH_MOUSE_LL = 14
    }

    public struct CwpStruct
    {
        public IntPtr lparam;
        public IntPtr wparam;
        public int message;
        public IntPtr hwnd;
    }

    public enum MouseMessages
    {
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_MOUSEMOVE = 0x0200,
        WM_MOUSEWHEEL = 0x020A,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205
    }

    public enum KeyMessages
    {
        WM_KEYDOWN = 0x0100,
        WM_SYSKEYDOWN = 0x0104,
        WM_KEYUP = 0x101,
        WM_SYSKEYUP = 0x105
    }

    public struct POINT
    {
        public int x;
        public int y;
    }

    public struct MouseLLStruct
    {
        public POINT pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    //typedef struct tagMOUSEHOOKSTRUCT
    //{
    //    POINT pt;
    //    HWND hwnd;
    //    UINT wHitTestCode;
    //    ULONG_PTR dwExtraInfo;
    //} MOUSEHOOKSTRUCT

    public struct MouseStruct
    {
        public POINT pt;
        public IntPtr hwnd;
        public int wHitTestCode;
        public IntPtr dwExtraInof;
    }

    public struct KeyboardStruct
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }
    
    static class WinApi
    {
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public const int WM_MOUSEACTIVATE = 0x0021;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(HookType idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        //[DllImport("user32.dll")]
        //public static extern IntPtr SetFocus(IntPtr hWnd);

        //[DllImport("kernel32.dll")]
        //public static extern int GetCurrentThreadId();

        //[DllImport("user32.dll")]
        //public static extern int GetClassNameW(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder buf, int nMaxCount);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern sbyte GetMessage(ref MSG lpMsg, IntPtr hWnd, uint mMsgFilterInMain, uint mMsgFilterMax);

        [DllImport("user32.dll")]
        public static extern sbyte PeekMessage(ref MSG lpMsg, IntPtr hWnd,
                            uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);
        
        [DllImport("user32.dll")]
        public static extern byte GetAsyncKeyState([In] int vKey);
    }
}
