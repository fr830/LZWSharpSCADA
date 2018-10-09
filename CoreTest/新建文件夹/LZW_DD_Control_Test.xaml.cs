using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CoreTest
{
    public partial class LZW_DD_Control_Test : Window
    {

       // List<TagNodeHandle> _valueChangedList;
        public LZW_DD_Control_Test(string dev, Point p)
        {
            //InitializeComponent();
            this.Title = dev + "_Control";

            //var CMD = App.Server[dev + "_cmd"];
            //var Status = App.Server[dev + "_status"];

            //var Open_TP_CD = App.Server[dev + "_Open_TP_CD"];
            //var Close_TP_CD = App.Server[dev + "_Close_TP_CD"];
            //var TR_CD = App.Server[dev + "_TR_CD"];
            //var NHT_O = App.Server[dev + "_NHT_O"];
            //var NHT_R = App.Server[dev + "_NHT_R"];




            this.Left = p.X - this.Width / 2;
            this.Top = p.Y - this.Height / 2 + 40;
            if (this.Left < 0) this.Left = 0;
            if (this.Top < 0) this.Top = 0;


        }

      
    }
}
