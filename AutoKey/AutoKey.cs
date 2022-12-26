using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using static System.Net.Mime.MediaTypeNames;

namespace AutoKey
{
    public partial class AutoKey : Form
    {
        Thread workThread;
        bool isWork = false;
        bool skeyUnPressed = true;

        public bool isPress = false;

        byte skey, wkey;
        int rate;
        long targetTick;

        private int hHookKeyboard;
        private WinApi.HookProc procKeyboard;

        //public event EventHandler<Keys> OnKeyPressed;
        //public event EventHandler<Keys> OnKeyUnpressed;

        public AutoKey()
        {
            InitializeComponent();
            InitialForm();
        }

        private void InitialForm()
        {
           
            FormClosed += Form_Closed;
            //this.OnKeyPressed += kbd_OnKeyPressed;
            //this.OnKeyUnpressed += kbd_OnKeyUnPressed;
            cbStartKey.SelectedIndex = 5;
            cbWorkKey.SelectedIndex = 3;
            
            
            string tmp = cbWorkKey.Items[cbWorkKey.SelectedIndex].ToString();
            wkey = (byte)tmp[0];
            tmp = cbStartKey.Items[cbStartKey.SelectedIndex].ToString();
            skey = (byte)tmp[0];

            rate = int.Parse(txtRate.Text);
            Debug.Print(rate.ToString());


            //keyboard hooking : 해당프로그램이 활성화되지 않아도 키이벤트작동하게 함
            procKeyboard = KeyboardCallBack;
            hHookKeyboard = WinApi.SetWindowsHookEx(HookType.WH_KEYBOARD_LL,
                            procKeyboard, (IntPtr)0, 0);

            //Thread Start
            //SetApartmentState STA를 설정해 주어야 Thread에서 Ui접근오류 막을수 있음
            workThread = new Thread(AutoKeyThead);
            workThread.SetApartmentState(ApartmentState.STA);
            workThread.Start();
        }
        
       
        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            //Thread 루프정지시켜서 쓰레드종료시킴
            isWork = false;
            isPress = false;
            workThread.Interrupt();

            //key unhooking
            WinApi.UnhookWindowsHookEx(hHookKeyboard);

            //폼닫기
            Dispose();
        }

        //작업쓰레드함수
        //별도 쓰레드에서 루프를 돌면서 원하는 작업을 함
        //form에서 루프를 돌면서 키이벤트를 기다라면 폼이 잘 작동하지 않는 문제가 있음
        public void AutoKeyThead()
        {
            isWork = true;
            long time0, time1;
        
            //DateTime.Now.Ticks 값은 1ms 10,000tick임
            //1초(1000ms) 10,000,000tick 
            //1분 600,000,000tick
            targetTick = 600000000/rate;
            time0 = time1 = DateTime.Now.Ticks;
            
            try
            {
                while (isWork)
                {
                    if (isPress)
                    {
                        //targetTick(정해진 틱값)이 되면 key_event 발생시키기
                        if (time1 - time0 >= targetTick)
                        {
                            //timer 초기화
                            time0 = time1;

                            //key event
                            Debug.Print("Key Press: " + targetTick.ToString());
                            WinApi.keybd_event((byte)wkey, 0, 0, 0);  //key down
                            WinApi.keybd_event((byte)wkey, 0, 2, 0);  //key up

                        }
                    }

                    //Sleep함수를 실행시켜 프로그램제어권한을 넘겨줌.
                    //Sleep함수가 없으면 cpu 1개 코어를 100%점유하게 됨
                    Thread.Sleep(1);

                    //MSG msg = new MSG();

                    //if (WinApi.PeekMessage(ref msg, IntPtr.Zero, 0, 0, 1) > 0)
                    //{
                    //    WinApi.TranslateMessage(ref msg);
                    //    WinApi.DispatchMessage(ref msg);
                    //}

                    time1 = DateTime.Now.Ticks;
                }
            }
            catch (Exception e)
            {
                Debug.Print("Thread interupted" + e.Message);            
            }
        }

        private int KeyboardCallBack(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //KeyboardStruct keyStruct = (KeyboardStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardStruct));

            //keydown
            if (nCode >= 0 && wParam == (IntPtr)KeyMessages.WM_KEYDOWN || wParam == (IntPtr)KeyMessages.WM_SYSKEYDOWN)
            {
            
                int vkCode = Marshal.ReadInt32(lParam);
                //Debug.Print(vkCode.ToString());
                //OnKeyPressed.Invoke(this, ((Keys)vkCode));

                if (vkCode == skey && skeyUnPressed)
                {
                    skeyUnPressed = false;
                    isPress = !isPress;
                    //Debug.Print("press2_elseif: " + isPress.ToString());

                    //off키 입력시에는 key_event 발생시키지 않음
                    if(!isPress) return -1;
                    //return -1;
                    
                }
            }
            //keyup
            else if (nCode >= 0 && wParam == (IntPtr)KeyMessages.WM_KEYUP || wParam == (IntPtr)KeyMessages.WM_SYSKEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                //OnKeyUnpressed.Invoke(this, ((Keys)vkCode));
                if (vkCode == skey) 
                {
                    skeyUnPressed = true;
                    //return -1;
                }
            }

            return WinApi.CallNextHookEx(hHookKeyboard, nCode, wParam, lParam);
        }

        //private void kbd_OnKeyPressed(object sender, Keys e)
        //{
           
        //    if (e == (Keys)skey && skeyUnPressed)
        //    {
        //        skeyUnPressed = false;
        //        isPress = !isPress;
        //        Debug.Print("press2_elseif: " + isPress.ToString());
        //    }

        //}

        //private void kbd_OnKeyUnPressed(object sender, Keys e)
        //{
        //    if (e == (Keys)skey)
        //    {
        //        skeyUnPressed = true;
        //        Debug.Print("unpress: " + isPress.ToString());
        //    }
        //}

        private void cbStartKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmp = cbStartKey.Items[cbStartKey.SelectedIndex].ToString();
            skey = (byte)tmp[0];
        }

        private void cbWorkKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmp = cbWorkKey.Items[cbWorkKey.SelectedIndex].ToString();
            wkey = (byte)tmp[0];
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            int newNum = 0;
            if (int.TryParse(txtRate.Text, out newNum))
            {
                if(newNum > 0)
                {
                    rate = newNum;
                    targetTick = 600000000/rate;
                }
            }
            
        }

    }
}
