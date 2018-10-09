using System;
using System.ComponentModel;
using System.Windows;

namespace HMIControl
{
    public class LZW_DD_Control : HMIControlBase
    {
        static LZW_DD_Control()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LZW_DD_Control), new FrameworkPropertyMetadata(typeof(LZW_DD_Control)));
        }
        public static DependencyProperty StautsProperty = DependencyProperty.Register("Status", typeof(short), typeof(LZW_DD_Control), new PropertyMetadata(new PropertyChangedCallback(OnValueChanged)));
        public static DependencyProperty CMDProperty = DependencyProperty.Register("CMD", typeof(short), typeof(LZW_DD_Control), new PropertyMetadata(new PropertyChangedCallback(OnValueChanged)));

        public static DependencyProperty TP_CD_OpenProperty = DependencyProperty.Register("TP_CD_Open", typeof(int), typeof(LZW_DD_Control));
        public static DependencyProperty TP_CD_CloseProperty = DependencyProperty.Register("TP_CD_Close", typeof(int), typeof(LZW_DD_Control));
        public static DependencyProperty TR_CDProperty = DependencyProperty.Register("TR_CD", typeof(int), typeof(LZW_DD_Control));

        public static DependencyProperty NHT_OProperty = DependencyProperty.Register("NHT_O", typeof(float), typeof(LZW_DD_Control));
        public static DependencyProperty NHT_RProperty = DependencyProperty.Register("NHT_R", typeof(float), typeof(LZW_DD_Control));


        protected override void UpdateState()
        {

        }

        public override string[] GetActions()
        {
            return new string[] { TagActions.VISIBLE, TagActions.CAPTION, TagActions.DEVICENAME, TagActions.ALARM, "ValveType" };
        }

        [Category("HMI")]
        public short Status
        {
            set
            {
                SetValue(StautsProperty, value);
            }
            get
            {
                return (short)GetValue(StautsProperty);
            }
        }

        [Category("HMI")]
        public short CMD
        {
            set
            {
                SetValue(CMDProperty, value);
            }
            get
            {
                return (short)GetValue(CMDProperty);
            }
        }

        [Category("HMI")]
        public int TP_CD_Open
        {
            set
            {
                SetValue(TP_CD_OpenProperty, value);
            }
            get
            {
                return (int)GetValue(TP_CD_OpenProperty);
            }
        }

        [Category("HMI")]
        public int TP_CD_Close
        {
            set
            {
                SetValue(TP_CD_CloseProperty, value);
            }
            get
            {
                return (int)GetValue(TP_CD_CloseProperty);
            }
        }

        [Category("HMI")]
        public int TR_CD
        {
            set
            {
                SetValue(TR_CDProperty, value);
            }
            get
            {
                return (int)GetValue(TR_CDProperty);
            }
        }

        [Category("HMI")]
        public float NHT_O
        {
            set
            {
                SetValue(NHT_OProperty, value);
            }
            get
            {
                return (float)GetValue(NHT_OProperty);
            }
        }

        [Category("HMI")]
        public float NHT_R
        {
            set
            {
                SetValue(NHT_RProperty, value);
            }
            get
            {
                return (float)GetValue(NHT_RProperty);
            }
        }

        public override Action SetTagReader(string key, Delegate tagChanged)
        {
            switch (key)
            {
                case "Status":
                    var _funcStatus = tagChanged as Func<int>;
                    if (_funcStatus != null)
                    {
                        return delegate {
                            Status = (short)_funcStatus();

                            };
                    }
                    else return null;
                case "CMD":
                    var _funcCMD = tagChanged as Func<int>;
                    if (_funcCMD != null)
                    {
                      
                        return delegate {
                            CMD = (short)_funcCMD();
                          
                        };
                    }
                    else return null;

                case "TP_CD_Open":
                    var _funcTP_CD_Open = tagChanged as Func<int>;
                    if (_funcTP_CD_Open != null)
                    {
                        return delegate
                        {
                            TP_CD_Open = _funcTP_CD_Open();
                        };
                    }
                    else return null;

                case "TP_CD_Close":
                    var _funcTP_CD_Close = tagChanged as Func<int>;
                    if (_funcTP_CD_Close != null)
                    {
                        return delegate
                        {
                            TP_CD_Close = _funcTP_CD_Close();
                        };
                    }
                    else return null;

                case "TR_CD":
                    var _funcTR_CD = tagChanged as Func<int>;
                    if (_funcTR_CD != null)
                    {
                        return delegate
                        {
                            TR_CD = _funcTR_CD();
                        };
                    }
                    else return null;

                case "NHT_O":
                    var _funcNHT_O = tagChanged as Func<float>;
                    if (_funcNHT_O != null)
                    {
                        return delegate
                        {
                            NHT_O = _funcNHT_O();
                        };
                    }
                    else return null;

                case "NHT_R":
                    var _funcNHT_R = tagChanged as Func<float>;
                    if (_funcNHT_R != null)
                    {
                        return delegate
                        {
                            NHT_R = _funcNHT_R();
                        };
                    }
                    else return null;
            }
            return base.SetTagReader(key, tagChanged);
        }
    }
}
