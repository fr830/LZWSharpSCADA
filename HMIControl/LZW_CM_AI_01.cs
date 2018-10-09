using System;
using System.ComponentModel;
using System.Windows;

namespace HMIControl
{
    [AI]
    public class LZW_CM_AI_01 : HMIControlBase
    {
        static LZW_CM_AI_01()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LZW_CM_AI_01), new FrameworkPropertyMetadata(typeof(LZW_CM_AI_01)));
        }

        public static DependencyProperty STAProperty = DependencyProperty.Register("STA", typeof(short), typeof(LZW_CM_AI_01));
        public static DependencyProperty MDProperty = DependencyProperty.Register("MD", typeof(short), typeof(LZW_CM_AI_01));
        public static DependencyProperty PVProperty = DependencyProperty.Register("PV", typeof(float), typeof(LZW_CM_AI_01));
        public static DependencyProperty ModeVisibleProperty = DependencyProperty.Register("ModeVisible", typeof(bool), typeof(LZW_CM_AI_01));
        public static DependencyProperty BorderVisibleProperty = DependencyProperty.Register("BorderVisible", typeof(bool), typeof(LZW_CM_AI_01));
        public static DependencyProperty AlarmBlinkProperty = DependencyProperty.Register("AlarmBlink", typeof(bool), typeof(LZW_CM_AI_01));

        public static DependencyProperty UnitProperty = DependencyProperty.Register("Unit", typeof(string), typeof(LZW_CM_AI_01));

        public override string[] GetActions()
        {
            return new string[] { TagActions.VISIBLE, TagActions.CAPTION, TagActions.DEVICENAME, TagActions.ALARM,"Unit" };
        }


        [Category("HMI")]
        public short STA
        {
            set
            {
                SetValue(STAProperty, value);
            }
            get
            {
                return (short)GetValue(STAProperty);
            }
        }
        [Category("HMI")]
        public short MD
        {
            set
            {
                SetValue(MDProperty, value);
            }
            get
            {
                return (short)GetValue(MDProperty);
            }
        }
        [Category("HMI")]
        public float PV
        {
            set
            {
                SetValue(PVProperty, value);
            }
            get
            {
                return (float)GetValue(PVProperty);
            }
        }

        [Category("HMI")]
        public bool ModeVisible
        {
            set
            {
                SetValue(ModeVisibleProperty, value);
            }
            get
            {
                return (bool)GetValue(ModeVisibleProperty);
            }
        }
        [Category("HMI")]
        public bool BorderVisible
        {
            set
            {
                SetValue(BorderVisibleProperty, value);
            }
            get
            {
                return (bool)GetValue(BorderVisibleProperty);
            }
        }      

        [Category("HMI")]
        public bool AlarmBlink
        {
            set
            {
                SetValue(AlarmBlinkProperty, value);
            }
            get
            {
                return (bool)GetValue(AlarmBlinkProperty);
            }
        }
        [Category("HMI")]
        public string Unit
        {
            set
            {
                SetValue(UnitProperty, value);
            }
            get
            {
                return (string)GetValue(UnitProperty);
            }
        }

        public override Action SetTagReader(string key, Delegate tagChanged)
        {
            switch (key)
            {
                case "STA":
                    var _funcStatus = tagChanged as Func<int>;
                    if (_funcStatus != null)
                    {
                        return delegate {
                            STA = (short)_funcStatus();
                            AlarmBlink = STA == 3 ? true : false;
                            VisualStateManager.GoToState(this, AlarmBlink ? "Blink" : "Nomal", true);
                        };
                    }
                    else return null;
                case "MD":
                    var _funcMode = tagChanged as Func<int>;
                    if (_funcMode != null)
                    {
                        //int T_Border = (short)_funcMode() >> 8;
                        //int T_Mode = (short)_funcMode() & 0xff;

                        return delegate {
                            MD = (short)_funcMode();
                            BorderVisible = MD >> 8 == 1 ? true : false;
                            ModeVisible = (MD & 0xff) == 1 ? true : false;
                        };
                    }
                    else return null;
                case "PV":
                    var _funcPV = tagChanged as Func<float>;
                    if (_funcPV != null)
                    {
                           return delegate {
                            PV = (float)_funcPV();
                        };
                    }
                    else return null;

                case "Unit":
                    var _funcUnit = tagChanged as Func<string>;
                    if (_funcUnit != null)
                    {
                        
                        return delegate {
                            Unit = (string)_funcUnit();
                        };
                    }
                    else return null;
            }
            return base.SetTagReader(key, tagChanged);
        }
    }

}
