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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMIControl
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:HMIControl"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:HMIControl;assembly=HMIControl"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:LZW_HMIInputOutput/>
    ///
    /// </summary>
    public class LZW_HMIInputOutput : HMIControlBase , ITagWriter
    {

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(LZW_HMIInputOutput),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty UnitProperty = DependencyProperty.Register("Unit", typeof(string), typeof(LZW_HMIInputOutput),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

        public static DependencyProperty StringFormatProperty = DependencyProperty.Register("StringFormat", typeof(string), typeof(LZW_HMIInputOutput));

        public static readonly DependencyProperty TagWriteTextProperty = DependencyProperty.Register("TagWriteText", typeof(string), typeof(LZW_HMIInputOutput));

        public static readonly DependencyProperty IsPulseProperty = DependencyProperty.Register("IsPulse", typeof(bool), typeof(LZW_HMIInputOutput), new FrameworkPropertyMetadata(false));

        public override string[] GetActions()
        {
            return new string[] { TagActions.VISIBLE, TagActions.CAPTION, TagActions.DEVICENAME, TagActions.TEXT };
        }

        static LZW_HMIInputOutput()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LZW_HMIInputOutput), new FrameworkPropertyMetadata(typeof(LZW_HMIInputOutput)));
        }

       

        [Category("HMI")]
        public string Text
        {
            get
            {
                return (string)base.GetValue(TextProperty);
            }
            set
            {
                base.SetValue(TextProperty, value);
            }
        }

        [Category("HMI")]
        public string Unit
        {
            get
            {
                return (string)base.GetValue(UnitProperty);
            }
            set
            {
                base.SetValue(UnitProperty, value);
            }
        }

        [Category("HMI")]
        public string StringFormat
        {
            get
            {
                return (string)base.GetValue(StringFormatProperty);
            }
            set
            {
                base.SetValue(StringFormatProperty, value);
            }
        }

        public override Action SetTagReader(string key, Delegate tagChanged)
        {
            var unit = " " + Unit;
            switch (key)
            {
                case TagActions.TEXT:
                    var _funcText = tagChanged as Func<string>;
                    if (_funcText != null)
                    {
                        return delegate { this.Text = _funcText() + unit; };
                    }
                    else
                    {
                        var _funcFloat = tagChanged as Func<float>;
                        if (_funcFloat != null)
                        {
                            return delegate
                            {
                                var format = this.StringFormat;
                                this.Text = string.IsNullOrEmpty(format) ? _funcFloat().ToString() : _funcFloat().ToString(format) + unit;
                            };
                        }
                        else
                        {
                            var _funcint = tagChanged as Func<int>;
                            {
                                if (_funcint != null)
                                    return delegate { this.Text = _funcint().ToString() + unit; };
                                else
                                {
                                    var _funcbool = tagChanged as Func<bool>;
                                    if (_funcbool != null)
                                    {
                                        return delegate { this.Text = _funcbool() ? "1" : "0" + unit; };
                                    }
                                }
                            }

                        }
                    }
                    return null;
            }
            return base.SetTagReader(key, tagChanged);
        }


        [Category("Common")]
        public string TagWriteText
        {
            get
            {
                return (string)base.GetValue(TagWriteTextProperty);
            }
            set
            {
                base.SetValue(TagWriteTextProperty, value);
            }
        }

        [Category("HMI")]
        public bool IsPulse
        {
            get
            {
                return (bool)base.GetValue(IsPulseProperty);
            }
            set
            {
                base.SetValue(IsPulseProperty, value);
            }
        }

        protected List<Func<object, int>> _funcWrites = new List<Func<object, int>>();
        public bool SetTagWriter(IEnumerable<Delegate> tagWriter)
        {
            bool ret = true;
            _funcWrites.Clear();

            foreach (var item in tagWriter)
            {
                Func<object, int> _funcWrite = item as Func<object, int>;

                if (_funcWrite != null)
                    _funcWrites.Add(_funcWrite);
                else
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Return && _funcWrites.Count > 0 && !string.IsNullOrEmpty(Text))
            {
                foreach (var func in _funcWrites)
                {
                    func(Text);
                }
            }
            base.OnKeyDown(e);
        }
    }

}



