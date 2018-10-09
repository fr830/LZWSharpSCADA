using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace HMIControl
{
    public class LZW_Line_Pin1 : HMIControlBase
    {
        public static DependencyProperty PinStyleProperty = DependencyProperty.Register("PinStyle", typeof(PinStyle), typeof(LZW_Line_Pin1),
            new FrameworkPropertyMetadata(PinStyle.Left, FrameworkPropertyMetadataOptions.AffectsRender));

        static LZW_Line_Pin1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LZW_Line_Pin1), new FrameworkPropertyMetadata(typeof(LZW_Line_Pin1)));
        }

        [Category("HMI")]
        public PinStyle PinStyle
        {
            get
            {
                return (PinStyle)base.GetValue(PinStyleProperty);
            }
            set
            {
                base.SetValue(PinStyleProperty, value);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            //base.OnRender(drawingContext);
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            double bevel = height * 0.1;
            Color color = Colors.Black;
            if (this.Background is SolidColorBrush)
                color = (this.Background as SolidColorBrush).Color;
            Pen pen = new Pen(base.BorderBrush, base.BorderThickness.Left);

            drawingContext.DrawRectangle(this.Background, pen, new Rect(0, 0, width, height));

            GetLinkPositions();


        }

        public override LinkPosition[] GetLinkPositions()
        {
            switch (this.PinStyle)
            {
                case PinStyle.Left:
                    return new LinkPosition[1]
                    {
                        new  LinkPosition(new Point(0.5,0.5),ConnectOrientation.Left),
                     };
                case PinStyle.Right:
                    return new LinkPosition[1]
                    {
                        new  LinkPosition(new Point(0.5,0.5),ConnectOrientation.Right),
                     };
                case PinStyle.Top:
                    return new LinkPosition[1]
                    {
                        new  LinkPosition(new Point(0.5,0.5),ConnectOrientation.Top),
                     };
                case PinStyle.Bottom:
                    return new LinkPosition[1]
                    {
                        new  LinkPosition(new Point(0.5,0.5),ConnectOrientation.Bottom),
                     };
                default:
                       
                    return new LinkPosition[1]
                    {
                            new  LinkPosition(new Point(0.5,0.5),ConnectOrientation.Left),
                     };
                   
            }
            
        }

       


    }

    public class LZW_Line_Pin4 : HMIControlBase
    {
      
        static LZW_Line_Pin4()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LZW_Line_Pin4), new FrameworkPropertyMetadata(typeof(LZW_Line_Pin4)));
        }

        public override LinkPosition[] GetLinkPositions()
        {
            return new LinkPosition[4]
                   {
                        new  LinkPosition(new Point(0.5,0.5),ConnectOrientation.Left),
                        new  LinkPosition(new Point(0.5,0.5),ConnectOrientation.Right),
                        new  LinkPosition(new Point(0.5,0.5),ConnectOrientation.Top),
                        new  LinkPosition(new Point(0.5,0.5),ConnectOrientation.Bottom),
                    };
        }
    }

    public enum PinStyle
    {
        Left,
        Right,
        Top,
        Bottom
    }

    public enum CMDDPinStyle
    {
        Nomal,
        PinStyle1,
        PinStyle2,
        PinStyle3,
        PinStyle4,
        PinStyle5,
        PinStyle6,
        PinStyle7,
        PinStyle8,
        PinStyle9,
    }
}
