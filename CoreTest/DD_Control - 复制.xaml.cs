using DataService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CoreTest
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class DD_Control : Window
    {

        const int COLS = 4;
        const int ROWHEIGHT = 30;

       

        //LZW_DD_btnStatus btnStatus  = new LZW_DD_btnStatus();

        public DD_Control(string dev, Point p)
        {
            InitializeComponent();
            LZW_DD_btnStatus btnStatus = new LZW_DD_btnStatus();
            this.DataContext = btnStatus;
            btnStatus.GetStauts(dev);
            var CMD = App.Server[dev + "_cmd"];
            var Status = App.Server[dev + "_status"];
            ushort TagCMD = CMD.Value.Word;
            ushort TagStauts = Status.Value.Word;

            btnAuto.Click  += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(TagCMD, 13,false));
                btnStatus.GetStauts(dev);
            });
            btnMan.Click   += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(TagCMD, 13, true));
                btnStatus.GetStauts(dev);
            });
            btnOpen.Click  += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(TagCMD, 0, true));
                btnStatus.GetStauts(dev);
            });
            btnClose.Click += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(TagCMD, 0, false));
                btnStatus.GetStauts(dev);
            });


            this.Left = p.X - grid.Width / 2;
            this.Top = p.Y - grid.Height / 2 + 40;
            if (this.Left < 0) this.Left = 0;
            if (this.Top < 0) this.Top = 0;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


       


        private void GetStauts( ushort TagStauts, ushort TagCMD)
        {
           //btnAuto_IsEnabled = (LZW_WindowHelper.GetBitValue(TagStauts, 7) & !LZW_WindowHelper.GetBitValue(TagCMD, 1)) | !LZW_WindowHelper.GetBitValue(TagCMD, 13) ? false : true;
          //  btnMan.IsEnabled = (LZW_WindowHelper.GetBitValue(TagStauts, 7) & !LZW_WindowHelper.GetBitValue(TagCMD, 1)) | LZW_WindowHelper.GetBitValue(TagCMD, 13) ? false : true; ;
          //  btnOpen.IsEnabled = LZW_WindowHelper.GetBitValue(TagCMD, 0) | !LZW_WindowHelper.GetBitValue(TagCMD, 13) | LZW_WindowHelper.GetBitValue(TagStauts, 4) | LZW_WindowHelper.GetBitValue(TagStauts, 11) ? false : true; ;
         //   btnClose.IsEnabled = !LZW_WindowHelper.GetBitValue(TagCMD, 0) | !LZW_WindowHelper.GetBitValue(TagCMD, 13) | LZW_WindowHelper.GetBitValue(TagStauts, 4) | LZW_WindowHelper.GetBitValue(TagStauts, 11) ? false : true; ;
        }
    }
}
