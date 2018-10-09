using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

using System.Windows.Threading;

namespace MESClient
{

    public partial class App : System.Windows.Application
    {
        const string MYCOSLOGSOURCE = "MyWEM Application";
        const string MYCOSLOGNAME = "MyWEM";

        static EventLog Log = new EventLog(MYCOSLOGNAME);
        static readonly string machine = Environment.MachineName;
        public static readonly DAServer Server = new DAServer();

        static IPrincipal _princ;
        public static IPrincipal Principal
        {
            get
            {
                return _princ;
            }
            set
            {
                _princ = value;
            }
        }

        public static string LogSource
        {
            get
            {
                return machine + "\\" + (_princ == null ? null : _princ.Identity.Name);
            }
        }

        static ReverseObservableQueue<string> eventLog = new ReverseObservableQueue<string>(300);
        public static ReverseObservableQueue<string> Events
        {
            get { return eventLog; }
        }



        public static void AddErrorLog(Exception e)
        {
            string err = "";
            Exception exp = e;
            while (exp != null)
            {
                err += string.Format("\n {0}", exp.Message);
                exp = exp.InnerException;
            }
            err += string.Format("\n {0}", e.StackTrace);
            Log.Source = MYCOSLOGSOURCE;
            Log.WriteEntry(err, EventLogEntryType.Error);
        }

        public static void AddErrorLog(string str)
        {
            string err = "";
           
            err = str;
            Log.Source = MYCOSLOGSOURCE;
            Log.WriteEntry(err, EventLogEntryType.Error);
        }

        private static byte ConvertBCD(byte b)//byte转换为BCD码
        {
            //高四位  
            byte b1 = (byte)(b / 10);
            //低四位  
            byte b2 = (byte)(b % 10);
            return (byte)((b1 << 4) | b2);
        }

        /// <summary>  
        /// 将BCD一字节数据转换到byte 十进制数据  
        /// </summary>  
        /// <param name="b" />字节数  
        /// <returns>返回转换后的BCD码</returns>  
        public static byte ConvertBCDToInt(byte b)
        {
            //高四位  
            byte b1 = (byte)((b >> 4) & 0xF);
            //低四位  
            byte b2 = (byte)(b & 0xF);

            return (byte)(b1 * 10 + b2);
        }


        public static int ToBCD(byte val)
        {
            int res = 0;
            int bit = 0;
            while (val >= 10)
            {
                res |= (val % 10 << bit);
                val /= 10;
                bit += 4;
            }
            res |= val << bit;
            return res;
        }
        public static byte FromBCD(int vals)
        {
            int c = 1;
            byte b = 0;
            while (vals > 0)
            {
                b += (byte)((vals & 0xf) * c);
                c *= 10;
                vals >>= 4;
            }
            return b;
        }
    }
}
