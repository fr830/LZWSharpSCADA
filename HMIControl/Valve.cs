using System;
using System.Windows;
using System.ComponentModel;
namespace HMIControl
{
    public class Valve : HMIControlBase
    {
        static Valve()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Valve), new FrameworkPropertyMetadata(typeof(Valve)));
        }
        public static readonly DependencyProperty Actived =
           DependencyProperty.Register("Actived", typeof(bool), typeof(Valve),
           new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnValueChanged)));

        public override string[] GetActions()
        {
            return new string[] { TagActions.VISIBLE, TagActions.CAPTION, TagActions.DEVICENAME, TagActions.ALARM, TagActions.ON };
        }

        [Category("HMI")]
        public bool Active
        {
            get { return (bool)GetValue(Actived); }
            set { SetValue(Actived, value); }
        }

        public override LinkPosition[] GetLinkPositions()
        {
            return new LinkPosition[2]
                {
                    new  LinkPosition(new Point(0,0.5),ConnectOrientation.Left),
                       new  LinkPosition(new Point(1,0.5),ConnectOrientation.Right),
                };
        }

        protected override void UpdateState()
        {
            VisualStateManager.GoToState(this,  Active ? "Actived" : "NoActive", true);
        }

        public override Action SetTagReader(string key, Delegate tagChanged)
        {
            switch (key)
            {
                case TagActions.ON:
                    var _funcRun = tagChanged as Func<bool>;
                    if (_funcRun != null)
                    {
                        return delegate { Active = _funcRun(); };
                    }
                    else return null;
            }
            return base.SetTagReader(key, tagChanged);
        }
    }
}
