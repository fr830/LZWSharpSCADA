﻿using System;
using System.ComponentModel;
using System.Windows;

namespace HMIControl
{
    [DD]
    public class CM_DD : HMIControlBase
    {
        static CM_DD()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CM_DD), new FrameworkPropertyMetadata(typeof(CM_DD)));
        }
        public static DependencyProperty STAProperty = DependencyProperty.Register("STA", typeof(short), typeof(CM_DD));
        public static DependencyProperty MDProperty = DependencyProperty.Register("MD", typeof(short), typeof(CM_DD));
        public static DependencyProperty ModeVisibleProperty = DependencyProperty.Register("ModeVisible", typeof(bool), typeof(CM_DD));
        public static DependencyProperty BorderVisibleProperty = DependencyProperty.Register("BorderVisible", typeof(bool), typeof(CM_DD));
        public static DependencyProperty ActivedProperty = DependencyProperty.Register("CM_DD_Actived", typeof(bool), typeof(CM_DD));
        public static DependencyProperty TypeProperty = DependencyProperty.Register("std_Type_ValveNC", typeof(bool), typeof(CM_DD));
        public static DependencyProperty AlarmBlinkProperty = DependencyProperty.Register("AlarmBlink", typeof(bool), typeof(CM_DD));

        public override string[] GetActions()
        {
            return new string[] { TagActions.VISIBLE, TagActions.CAPTION, TagActions.DEVICENAME, TagActions.ALARM, "ValveType"};
        }

        public override LinkPosition[] GetLinkPositions()
        {
            return new LinkPosition[2]
                {
                    new  LinkPosition(new Point(0.2,0.5),ConnectOrientation.Left),
                    new  LinkPosition(new Point(0.8,0.5),ConnectOrientation.Right),
                };
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
        public bool CM_DD_Actived
        {
            set
            {
                SetValue(ActivedProperty, value);
            }
            get
            {
                return (bool)GetValue(ActivedProperty);
            }
        }

        [Category("HMI")]
        public bool std_Type_ValveNC
        {
            set
            {
                SetValue(TypeProperty, value);
            }
            get
            {
                return (bool)GetValue(TypeProperty);
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
                            CM_DD_Actived = (!std_Type_ValveNC && (STA == 2 || STA == 4) || std_Type_ValveNC && (STA == 1 || STA == 3)) && (STA >= 1 && STA <= 4 )? false : true;
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
                            BorderVisible = MD >> 8 == 1? true:false;
                            ModeVisible  = (MD & 0xff) == 1 ? true : false;
                            AlarmBlink = (MD & 0xff) == 3 ? true : false;
                            VisualStateManager.GoToState(this, AlarmBlink ? "Blink" : "Nomal", true);
                        };
                    }
                    else return null;

                case "ValveType":
                    var _funcType = tagChanged as Func<bool>;
                    if (_funcType != null)
                    {
                        return delegate
                        {
                            std_Type_ValveNC = _funcType();
                        };
                    }
                    else return null;
            }
            return base.SetTagReader(key, tagChanged);
        }
    }
}
