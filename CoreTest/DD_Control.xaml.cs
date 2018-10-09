using System;
using System.Windows;

namespace CoreTest
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class DD_Control : Window
    {

       
        public DD_Control(string dev, Point p)
        {
            InitializeComponent();
   
     
            var CMD = App.Server[dev + "_cmd"];
            var Status = App.Server[dev + "_status"];
          
           
            btnAuto.Click += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(CMD.Value.Word, 13, false));
                
            });
            btnMan.Click += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(CMD.Value.Word, 13, true));
                
            });
            btnOpen.Click += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(CMD.Value.Word, 0, true));
               
            });
            btnClose.Click += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(CMD.Value.Word, 0, false));
               
            });


            this.Left = p.X - grid.Width / 2;
            this.Top = p.Y - grid.Height / 2 + 40;
            if (this.Left < 0) this.Left = 0;
            if (this.Top < 0) this.Top = 0;

            if (Status != null)
            {
                GetStauts(Status.Value.Word, CMD.Value.Word);

                Status.ValueChanged += (s1, e1) =>
                {
                     Dispatcher.BeginInvoke(new Action(delegate
                    {
                      GetStauts(Status.Value.Word, CMD.Value.Word);
                    }));

                };
            }


        }

        private void GetStauts(ushort TagStauts, ushort TagCMD)
        {
            btnAuto.IsEnabled = (LZW_WindowHelper.GetBitValue(TagStauts, 7) & !LZW_WindowHelper.GetBitValue(TagCMD, 1)) | !LZW_WindowHelper.GetBitValue(TagCMD, 13) ? false : true;
            btnMan.IsEnabled = (LZW_WindowHelper.GetBitValue(TagStauts, 7) & !LZW_WindowHelper.GetBitValue(TagCMD, 1)) | LZW_WindowHelper.GetBitValue(TagCMD, 13) ? false : true; ;
            btnOpen.IsEnabled = LZW_WindowHelper.GetBitValue(TagCMD, 0) | !LZW_WindowHelper.GetBitValue(TagCMD, 13) | LZW_WindowHelper.GetBitValue(TagStauts, 4) | LZW_WindowHelper.GetBitValue(TagStauts, 11) ? false : true; ;
            btnClose.IsEnabled = !LZW_WindowHelper.GetBitValue(TagCMD, 0) | !LZW_WindowHelper.GetBitValue(TagCMD, 13) | LZW_WindowHelper.GetBitValue(TagStauts, 4) | LZW_WindowHelper.GetBitValue(TagStauts, 11) ? false : true; ;
        }
    }
}
