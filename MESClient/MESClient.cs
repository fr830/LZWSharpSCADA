using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MESClient
{
    public partial class MESClient : Form
    {

        public DataService.ITag Int1;
        public DataService.ITag Int2;
        public DataService.ITag Int3;
        public DataService.ITag Int4;
        public DataService.ITag Int5;
        public DataService.ITag Int6;
        public DataService.ITag Int7;
        public DataService.ITag Int8;
        public DataService.ITag Int9;
        public DataService.ITag Int10;
        public DataService.ITag Int11;
        public DataService.ITag Int12;
        public DataService.ITag Int13;
        public DataService.ITag Int14;
        public DataService.ITag Int15;

        public DataService.ITag Float1;
        public DataService.ITag Float2;
        public DataService.ITag Float3;
        public DataService.ITag Float4;
        public DataService.ITag Float5;
        public DataService.ITag Float6;
        public DataService.ITag Float7;
        public DataService.ITag Float8;
        public DataService.ITag Float9;
        public DataService.ITag Float10;
        public DataService.ITag Float11;
        public DataService.ITag Float12;
        public DataService.ITag Float13;
        public DataService.ITag Float14;
        public DataService.ITag Float15;

        public DataService.ITag String1;
        public DataService.ITag String2;
        public DataService.ITag String3;
        public DataService.ITag String4;
        public DataService.ITag String5;
        public DataService.ITag String6;
        public DataService.ITag String7;
        public DataService.ITag String8;
        public DataService.ITag String9;
        public DataService.ITag String10;
        public DataService.ITag String11;
        public DataService.ITag String12;
        public DataService.ITag String13;
        public DataService.ITag String14;
        public DataService.ITag String15;

        public DataService.ITag EventType;
        public DataService.ITag TrigConut;
        public DataService.ITag TrigConutMES;


        public DataService.ITag MES_PLC_DT_Y;
        public DataService.ITag MES_PLC_DT_M;
        public DataService.ITag MES_PLC_DT_D;
        public DataService.ITag MES_PLC_DT_hh;
        public DataService.ITag MES_PLC_DT_mm;
        public DataService.ITag MES_PLC_DT_ss;

        public Int16 iMES_PLC_DT_Y;
        public Int16 iMES_PLC_DT_M;
        public Int16 iMES_PLC_DT_D;
        public Int16 iMES_PLC_DT_hh;
        public Int16 iMES_PLC_DT_mm;
        public Int16 iMES_PLC_DT_ss;



        public MESClient()
        {
            InitializeComponent();

            MESClient_Load();
            Control.CheckForIllegalCrossThreadCalls = false;
            Int1.ValueChanged += (s, e) =>
            {
                this.tbInt1.Text = Int1.Value.Int32.ToString();
            };
            Int2.ValueChanged += (s, e) =>
            {
                this.tbInt2.Text = Int2.Value.Int32.ToString();
            };
            Int3.ValueChanged += (s, e) =>
            {
                this.tbInt3.Text = Int3.Value.Int32.ToString();
            };
            Int4.ValueChanged += (s, e) =>
            {
                this.tbInt4.Text = Int4.Value.Int32.ToString();
            };
            Int5.ValueChanged += (s, e) =>
            {
                this.tbInt5.Text = Int5.Value.Int32.ToString();
            };
            Int6.ValueChanged += (s, e) =>
            {
                this.tbInt6.Text = Int6.Value.Int32.ToString();
            };
            Int7.ValueChanged += (s, e) =>
            {
                this.tbInt7.Text = Int8.Value.Int32.ToString();
            };
            Int8.ValueChanged += (s, e) =>
            {
                this.tbInt8.Text = Int8.Value.Int32.ToString();
            };
            Int9.ValueChanged += (s, e) =>
            {
                this.tbInt9.Text = Int9.Value.Int32.ToString();
            };
            Int10.ValueChanged += (s, e) =>
            {
                this.tbInt10.Text = Int10.Value.Int32.ToString();
            };


            MES_PLC_DT_Y.ValueChanged += (s, e) =>
            {
                iMES_PLC_DT_Y = App.FromBCD(MES_PLC_DT_Y.Value.Byte);
                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            };
            MES_PLC_DT_M.ValueChanged += (s, e) =>
            {
                iMES_PLC_DT_M = App.FromBCD(MES_PLC_DT_M.Value.Byte);
                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            };
            MES_PLC_DT_D.ValueChanged += (s, e) =>
            {
                iMES_PLC_DT_D = App.FromBCD(MES_PLC_DT_D.Value.Byte);
                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            };
            MES_PLC_DT_hh.ValueChanged += (s, e) =>
            {
                iMES_PLC_DT_hh = App.FromBCD(MES_PLC_DT_hh.Value.Byte);
                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            };
            MES_PLC_DT_mm.ValueChanged += (s, e) =>
            {
                iMES_PLC_DT_mm = App.FromBCD(MES_PLC_DT_mm.Value.Byte);
                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            };
            MES_PLC_DT_ss.ValueChanged += (s, e) =>
            {
                iMES_PLC_DT_ss = App.FromBCD(MES_PLC_DT_ss.Value.Byte);
                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            };




            Float1.ValueChanged += (s, e) =>
            {
                this.tbFloat1.Text = Float1.Value.Single.ToString();
            };
            Float2.ValueChanged += (s, e) =>
            {
                this.tbFloat2.Text = Float2.Value.Single.ToString();
            };
            Float3.ValueChanged += (s, e) =>
            {
                this.tbFloat3.Text = Float3.Value.Single.ToString();
            };
            Float4.ValueChanged += (s, e) =>
            {
                this.tbFloat4.Text = Float4.Value.Single.ToString();
            };
            Float5.ValueChanged += (s, e) =>
            {
                this.tbFloat5.Text = Float5.Value.Single.ToString();
            };

            TrigConut.ValueChanged += (s, e) =>
            {
                this.tbTrigCount.Text = TrigConut.Value.Int16.ToString();
                if(TrigConut.Value.Int16 != TrigConutMES.Value.Int16)
                {
                    //MessageQueueSend();
                   

                }



            };


            EventType.ValueChanged += (s, e) =>
            {
                this.tbEventType.Text = EventType.Value.Int16.ToString();
            };
            TrigConutMES.ValueChanged += (s, e) =>
            {
                this.tbTrigCountMES.Text = TrigConutMES.Value.Int16.ToString();
            };

            this.tbInt1.TextChanged += (s, e) =>
            {
                Int1.Write(Convert.ToInt32(this.tbInt1.Text));
            };

            this.tbInt2.TextChanged += (s, e) =>
            {
                Int2.Write(Convert.ToInt32(this.tbInt2.Text));
            };

        }

        private void MessageQueueSend()
        {
            LZWMessageQueue.MessageQueue oMQ = new LZWMessageQueue.MessageQueue();

            String myDateString;
            String EventDateString;
            DateTime myDate;
            String myYear;
            String myMonth;
            String myDay;
            String myHour;
            String myMinute;
            String mySecond;


            myDate = DateTime.Now;
            myDateString = myDate.ToString("yyyy-MM-dd HH:mm:ss") ;

            myYear = ( (iMES_PLC_DT_Y >=90 && iMES_PLC_DT_Y <= 99) ? "19"+ iMES_PLC_DT_Y.ToString(): "20"+ iMES_PLC_DT_Y.ToString());
            myMonth = iMES_PLC_DT_M.ToString();
            myDay = iMES_PLC_DT_D.ToString();
            
            myHour = iMES_PLC_DT_hh.ToString();
            myMinute = iMES_PLC_DT_mm.ToString();
            mySecond = iMES_PLC_DT_ss.ToString();

            myMonth = (myMonth.Length == 1 ? "0" + myMonth : myMonth);
            myDay = (myDay.Length == 1 ? "0" + myDay : myDay);
            myHour = (myHour.Length == 1 ? "0" + myHour : myHour);
            myMinute = (myMinute.Length == 1 ? "0" + myMinute : myMinute);
            mySecond = (mySecond.Length == 1 ? "0" + mySecond : mySecond);

            EventDateString = myYear + "-" + myMonth + "-" + myDay + " " + myHour + ":" + myMinute + ":" + mySecond;


            oMQ.QueueName = this.tbQueueName.Text; //"FormatName:DIRECT=OS:" + this.tbDestinationHost.Text + "\\private$\\" + this.tbQueueName.Text;

            oMQ.EventHost = this.tbEventHost.Text;
            oMQ.EventQueue = this.tbEventQueue.Text;
            oMQ.EventType = Convert.ToInt32(this.tbEventType.Text);
            oMQ.EventCounter = Convert.ToInt32(this.tbTrigCount.Text);

            oMQ.EventTime = EventDateString;
            oMQ.DataQuality = 1;
            oMQ.WaitedFor = 0;
            oMQ.EventDetected = myDateString;
            oMQ.TriggerTime = myDateString;

            oMQ.DefineProdMsg(10, 10, 5);
            oMQ.SetInteger(1, Int1.Value.Int32);
            oMQ.SetInteger(2, Int2.Value.Int32);
            oMQ.SetInteger(3, Int3.Value.Int32);
            oMQ.SetInteger(4, Int4.Value.Int32);
            oMQ.SetInteger(5, Int5.Value.Int32);
            oMQ.SetInteger(6, Int6.Value.Int32);
            oMQ.SetInteger(7, Int7.Value.Int32);
            oMQ.SetInteger(8, Int8.Value.Int32);
            oMQ.SetInteger(9, Int9.Value.Int32);
            oMQ.SetInteger(10, Int10.Value.Int32);

            oMQ.SetFloat(1, Float1.Value.Single);
            oMQ.SetFloat(2, Float2.Value.Single);
            oMQ.SetFloat(3, Float3.Value.Single);
            oMQ.SetFloat(4, Float4.Value.Single);
            oMQ.SetFloat(5, Float5.Value.Single);
            oMQ.SetFloat(6, 0.0);
            oMQ.SetFloat(7, 0.0);
            oMQ.SetFloat(8, 0.0);
            oMQ.SetFloat(9, 0.0);
            oMQ.SetFloat(10, 0.0);

            oMQ.SetString(1, "");
            oMQ.SetString(2, "");
            oMQ.SetString(3, "");
            oMQ.SetString(4, "");
            oMQ.SetString(5, "");

            if (oMQ.SendProdMsg())
            {
                TrigConutMES.Write(TrigConut.Value.Int16);
               
            }
            else
            {
                App.AddErrorLog(oMQ.LastErrorMsg);
            }

            
        }

      

        private void MESClient_Load()
        {
            Int1 = App.Server["Int1"];
            Int2 = App.Server["Int2"];
            Int3 = App.Server["Int3"];
            Int4 = App.Server["Int4"];
            Int5 = App.Server["Int5"];
            Int6 = App.Server["Int6"];
            Int7 = App.Server["Int7"];
            Int8 = App.Server["Int8"];
            Int9 = App.Server["Int9"];
            Int10 = App.Server["Int10"];
            Int11 = App.Server["Int11"];
            Int12 = App.Server["Int12"];
            Int13 = App.Server["Int13"];
            Int14 = App.Server["Int14"];
            Int15 = App.Server["Int14"];

            Float1 = App.Server["Float1"];
            Float2 = App.Server["Float2"];
            Float3 = App.Server["Float3"];
            Float4 = App.Server["Float4"];
            Float5 = App.Server["Float5"];
            Float6 = App.Server["Float6"];
            Float7 = App.Server["Float7"];
            Float8 = App.Server["Float8"];
            Float9 = App.Server["Float9"];
            Float10 = App.Server["Float10"];
            Float11 = App.Server["Float11"];
            Float12 = App.Server["Float12"];
            Float13 = App.Server["Float13"];
            Float14 = App.Server["Float14"];
            Float15 = App.Server["Float15"];

            String1 = App.Server["String1"];
            String2 = App.Server["String2"];
            String3 = App.Server["String3"];
            String4 = App.Server["String4"];
            String5 = App.Server["String5"];
            String6 = App.Server["String6"];
            String7 = App.Server["String7"];
            String8 = App.Server["String8"];
            String9 = App.Server["String9"];
            String10 = App.Server["String10"];
            String11 = App.Server["String11"];
            String12 = App.Server["String12"];
            String13 = App.Server["String13"];
            String14 = App.Server["String14"];
            String15 = App.Server["String15"];

            EventType = App.Server["EventType"];
            TrigConut = App.Server["TrigConut"];
            TrigConutMES = App.Server["TrigConutMES"];

            MES_PLC_DT_Y = App.Server["MES_PLC_DT_Y"];
            MES_PLC_DT_M = App.Server["MES_PLC_DT_M"];
            MES_PLC_DT_D = App.Server["MES_PLC_DT_D"];
            MES_PLC_DT_hh = App.Server["MES_PLC_DT_hh"];
            MES_PLC_DT_mm = App.Server["MES_PLC_DT_mm"];
            MES_PLC_DT_ss = App.Server["MES_PLC_DT_ss"];
        }

        private void MESClient_Load(object sender, EventArgs e)
        {
         
            this.tbQueueName.Text = ConfigCache.QueueName;
            this.tbEventHost.Text = "EventHost";
            this.tbEventQueue.Text = "EventQueue";
            this.tbEventType.Text = "1";
            this.tbEventCounter.Text = "1";
            if(Int1 != null)
            {
                this.tbInt1.Text = Int1.Value.Int32.ToString();
            }
            if (Int2 != null)
            {
                this.tbInt2.Text = Int2.Value.Int32.ToString();
            }
            if (Int3 != null)
            {
                this.tbInt3.Text = Int3.Value.Int32.ToString();
            }
            if (Int4 != null)
            {
                this.tbInt4.Text = Int4.Value.Int32.ToString();
            }
            if (Int5 != null)
            {
                this.tbInt5.Text = Int5.Value.Int32.ToString();
            }
            if (Int6 != null)
            {
                this.tbInt6.Text = Int6.Value.Int32.ToString();
            }
            if (Int7 != null)
            {
                this.tbInt7.Text = Int7.Value.Int32.ToString();
            }
            if (Int8 != null)
            {
                this.tbInt8.Text = Int8.Value.Int32.ToString();
            }
            if (Int9 != null)
            {
                this.tbInt9.Text = Int9.Value.Int32.ToString();
            }
            if (Int10 != null)
            {
                this.tbInt10.Text = Int10.Value.Int32.ToString();
            }


            if (Float1 != null)
            {
                this.tbFloat1.Text = Float1.Value.Single.ToString();
            }
            if (Float2 != null)
            {
                this.tbFloat2.Text = Float2.Value.Single.ToString();
            }
            if (Float3 != null)
            {
                this.tbFloat3.Text = Float3.Value.Single.ToString();
            }
            if (Float4 != null)
            {
                this.tbFloat4.Text = Float4.Value.Single.ToString();
            }
            if (Float5 != null)
            {
                this.tbFloat5.Text = Float5.Value.Single.ToString();
            }

            if (EventType != null)
            {
                this.tbEventType.Text = EventType.Value.Int32.ToString();
            }
            if (TrigConut != null)
            {
                this.tbTrigCount.Text = TrigConut.Value.Int32.ToString();
            }
            if (TrigConutMES != null)
            {
                this.tbTrigCountMES.Text = TrigConutMES.Value.Int32.ToString();
            }

            if (MES_PLC_DT_Y != null)
            {
                  iMES_PLC_DT_Y = App.FromBCD( MES_PLC_DT_Y.Value.Byte);
                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            }
            if (MES_PLC_DT_M != null)
            {
                iMES_PLC_DT_M = App.FromBCD(MES_PLC_DT_M.Value.Byte);
                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            }
            if (MES_PLC_DT_D != null)
            {
                iMES_PLC_DT_D = App.FromBCD(MES_PLC_DT_D.Value.Byte);

                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            }
            if (MES_PLC_DT_hh != null)
            {
                iMES_PLC_DT_hh = App.FromBCD(MES_PLC_DT_hh.Value.Byte);
                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            }
            if (MES_PLC_DT_mm != null)
            {
                iMES_PLC_DT_mm = App.FromBCD(MES_PLC_DT_mm.Value.Byte);
                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            }
            if (MES_PLC_DT_ss != null)
            {
                iMES_PLC_DT_ss = App.FromBCD(MES_PLC_DT_ss.Value.Byte);
                this.tbMESPLCDT.Text = iMES_PLC_DT_Y.ToString() + "-" + iMES_PLC_DT_M.ToString() + "-" + iMES_PLC_DT_D.ToString() + " " + iMES_PLC_DT_hh.ToString() + ":" + iMES_PLC_DT_mm.ToString() + ":" + iMES_PLC_DT_ss.ToString();
            }

           


        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)

            {

                //还原窗体显示    

                WindowState = FormWindowState.Normal;

                //激活窗体并给予它焦点

                this.Activate();

                //任务栏区显示图标

                this.ShowInTaskbar = true;

                //托盘区图标隐藏

                notifyIcon1.Visible = false;

            }

        }

        /// <summary>

        /// 判断是否最小化,然后显示托盘

        /// </summary>

        /// <param name="sender"></param>

        /// <param name="e"></param>

        private void F_Main_SizeChanged(object sender, EventArgs e)

        {

            //判断是否选择的是最小化按钮

            if (WindowState == FormWindowState.Minimized)

            {

                //隐藏任务栏区图标

                this.ShowInTaskbar = false;

                //图标显示在托盘区

                notifyIcon1.Visible = true;

            }

        }

        /// <summary>

        /// 确认是否退出

        /// </summary>

        /// <param name="sender"></param>

        /// <param name="e"></param>

        private void F_Main_FormClosing(object sender, FormClosingEventArgs e)

        {

            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                // 关闭所有的线程

                this.Dispose();

                this.Close();

            }

            else

            {

                e.Cancel = true;

            }

        }
    }
}
