using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CoreTest
{
    public partial class LZW_DD_Control : Window
    {

        List<TagNodeHandle> _valueChangedList;
        string _dev;
        public LZW_DD_Control(string dev, Point p)
        {
            double x1 = SystemParameters.PrimaryScreenWidth;//得到屏幕整体宽度
            double y1 = SystemParameters.PrimaryScreenHeight;//得到屏幕整体高度

            InitializeComponent();
            this.Title = dev + "_Control";

            var CMD = App.Server[dev + "_cmd"];
            var Status = App.Server[dev + "_status"];

            var Open_TP_CD = App.Server[dev + "_TP_CD_Open"];
            var Close_TP_CD = App.Server[dev + "_TP_CD_Close"];
            var TR_CD = App.Server[dev + "_TR_CD"];
            var NHT_O = App.Server[dev + "_NHT_O"];
            var NHT_R = App.Server[dev + "_NHT_R"];

            this.Left = p.X +20; //- this.Width;
            this.Top = p.Y +20;//- this.Height;
            if (p.X > x1-this.Width)
            {
                this.Left = p.X - this.Width-20;
            }
            if (p.Y > y1 - this.Height)
            {
                this.Top = p.Y -this.Height-20;
            }
            if (this.Left < 0) this.Left = 0;
            if (this.Top < 0) this.Top = 0;

            _dev = dev;

        }

        private void HMI_Loaded(object sender, RoutedEventArgs e)
        {
            btnAuto.TagReadText = string.Format("失效:({0}_Status.7&{0}_CMD.1~)|({0}_CMD.13~)\\", _dev);
            btnMan.TagReadText = string.Format("失效:({0}_Status.7&{0}_CMD.1~)|({0}_CMD.13)\\", _dev);
            btnOpen.TagReadText = string.Format("失效:{0}_CMD.0|{0}_CMD.13~|{0}_Status.4|{0}_Status.11\\", _dev);
            btnClose.TagReadText = string.Format("失效:{0}_CMD.00~|{0}_CMD.13~|{0}_Status.04|{0}_Status.11\\", _dev);
           // btnPEB.TagReadText = string.Format("失效:{0}_CMD.00~|{0}_CMD.13~|{0}_Status.04|{0}_Status.11\\", _dev);
          //  btnSIM.TagReadText = string.Format("失效:{0}_CMD.00~|{0}_CMD.13~|{0}_Status.04|{0}_Status.11\\", _dev);

            btnAuto.TagWriteText = string.Format("{0}_CMD.13:False\\", _dev);
            btnMan.TagWriteText = string.Format("{0}_CMD.13:True\\", _dev);
            btnAck.TagWriteText = string.Format("{0}_CMD.8:True\\", _dev);
            btnOpen.TagWriteText = string.Format("{0}_CMD.0:True\\", _dev);
            btnClose.TagWriteText = string.Format("{0}_CMD.0:False\\", _dev);
            btnPEB.TagWriteText = string.Format("{0}_CMD.1:{0}_CMD.1~\\", _dev);
            btnSIM.TagWriteText = string.Format("{0}_CMD.12:{0}_CMD.12~\\", _dev);


            lock (this)
            {
                _valueChangedList = cvs1.BindingToServer(App.Server);
            }
        }

        private void HMI_Unloaded(object sender, RoutedEventArgs e)
        {
            lock (this)
            {
                App.Server.RemoveHandles(_valueChangedList);
            }
        }

       
    }
}
