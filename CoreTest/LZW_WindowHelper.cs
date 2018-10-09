using System;
using System.Windows;
using System.Windows.Input;
using HMIControl;
using System.Security.Permissions;
using System.Windows.Threading;
using System.ComponentModel;

namespace CoreTest
{
    class LZW_WindowHelper
    {
        static LZW_DD_Control frm2 = null;
        public static void DD_element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            HMIControlBase ui = sender as HMIControlBase;
            if (ui != null)
            {
                if (frm2 != null)
                    frm2.Close();
                frm2 = new LZW_DD_Control(ui.DeviceName, ui.PointToScreen(new Point(ui.ActualWidth / 2, ui.ActualHeight / 2)));
                frm2.Topmost = true;
                frm2.Show();
            }
            e.Handled = true;


        }

        public static bool GetBitValue(int value, ushort index)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");
            var val = 1 << index;
            return (value & val) == val;
        }

        public static int SetBitValue(int value, ushort index, bool bitValue)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");
            var val = 1 << index;
            return bitValue ? (value | val) : (value & ~val);
        }


        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrames), frame);
            try { Dispatcher.PushFrame(frame); }
            catch (InvalidOperationException) { }
        }
        private static object ExitFrames(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }

}