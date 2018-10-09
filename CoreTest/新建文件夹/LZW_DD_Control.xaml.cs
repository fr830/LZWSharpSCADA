using System;
using System.Windows;
using System.Windows.Media;

namespace CoreTest
{
    public partial class LZW_DD_Control : Window
    {
        public LZW_DD_Control(string dev, Point p)
        {
            InitializeComponent();
            this.Title = dev + "_Control";

            var CMD = App.Server[dev + "_cmd"];
            var Status = App.Server[dev + "_status"];

            var Open_TP_CD = App.Server[dev + "_TP_CD_Open"];
            var Close_TP_CD = App.Server[dev + "_TP_CD_Close"];
            var TR_CD = App.Server[dev + "_TR_CD"];
            var NHT_O = App.Server[dev + "_NHT_O"];
            var NHT_R = App.Server[dev + "_NHT_R"];


            btnAuto.Click += new RoutedEventHandler((s, e) =>
            {
                if ((CMD != null) & MessageBox.Show("Delete this user?", "Confirm Message", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
               
                {
                    CMD.Write(LZW_WindowHelper.SetBitValue(CMD.Value.Word, 13, false));
                }
            });

            btnMan.Click += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(CMD.Value.Word, 13, true));

            });
            btnAck.Click += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(CMD.Value.Word, 8, true));

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

            btnPEB.Click += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(CMD.Value.Word, 1, !LZW_WindowHelper.GetBitValue(CMD.Value.Word, 1)));

            });

            btnSIM.Click += new RoutedEventHandler((s, e) =>
            {
                if (CMD != null)
                    CMD.Write(LZW_WindowHelper.SetBitValue(CMD.Value.Word, 12, !LZW_WindowHelper.GetBitValue(CMD.Value.Word, 12)));

            });


            this.Left = p.X - this.Width / 2;
            this.Top = p.Y - this.Height / 2 + 40;
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
            if (Close_TP_CD != null)
            {
                this.Close_TP_CD.Text = Close_TP_CD.Value.Word.ToString();
                Close_TP_CD.ValueChanged += (s1, e1) =>
                {
                    Dispatcher.BeginInvoke(new Action(delegate
                    {
                        this.Close_TP_CD.Text = Close_TP_CD.Value.Word.ToString();

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

            Status_INTLK.Fill = LZW_WindowHelper.GetBitValue(TagStauts, 15) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Gray);
            Status_PE.Fill = LZW_WindowHelper.GetBitValue(TagStauts, 7) ? new SolidColorBrush(Colors.DarkGreen) : new SolidColorBrush(Colors.Gray);
            Status_Ready.Fill = LZW_WindowHelper.GetBitValue(TagStauts, 0) ? new SolidColorBrush(Colors.DarkGreen) : new SolidColorBrush(Colors.Gray);
            Status_YI.Fill = LZW_WindowHelper.GetBitValue(TagStauts, 4) ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.Gray);
        }

       
    }
}
