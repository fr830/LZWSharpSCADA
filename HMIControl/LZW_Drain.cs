using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMIControl
{
    public class LZW_Drain : HMIControlBase
    {
        static LZW_Drain()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LZW_Drain), new FrameworkPropertyMetadata(typeof(LZW_Drain)));
        }

        public override LinkPosition[] GetLinkPositions()
        {
            return new LinkPosition[1]
                {
                        new  LinkPosition(new Point(0.5,0.1),ConnectOrientation.Top),  
                };
        }
    }
   

}
