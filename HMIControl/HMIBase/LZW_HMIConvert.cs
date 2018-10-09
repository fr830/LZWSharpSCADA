using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
namespace HMIControl
{
    public class LZW_BoolToVisibleOrHidden : IValueConverter
    {

        public LZW_BoolToVisibleOrHidden() { }

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible;
        }
        #endregion
    }

    public class LZW_Int16_CM_DD_STA : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);
            switch (colorValue)
            {
                case 1:
                    return new SolidColorBrush(Colors.White);
                case 2:
                    return new SolidColorBrush(Colors.Lime);
                case 3:
                    return new SolidColorBrush(Colors.DarkGreen);
                case 4:
                    return new SolidColorBrush(Colors.DarkGreen);
                case 5:
                    return new SolidColorBrush(Colors.Green);
                case 6:
                    return new SolidColorBrush(Colors.Green);

            }
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LZW_Int16_CM_DD_Mode : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);

            //  int Border = colorValue >> 8;
            int Mode = colorValue & 0xff;


            if (Mode >= 5 || Mode <= 1)
            {
                return new SolidColorBrush(Colors.White);
            }
            if (Mode == 3 || Mode == 4)
            {
                return new SolidColorBrush(Colors.Red);
            }
            if (Mode == 2)
            {
                return new SolidColorBrush(Colors.Blue);
            }
            else
                return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LZW_Int16_CM_DD_Border : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);

            int Border = colorValue >> 8;
            // int Mode = colorValue & 0xff;


            if (Border == 3)
            {
                return new SolidColorBrush(Colors.Yellow);
            }
            if (Border != 3 && Border != 1)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
                return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LZW_Int16_CM_DI_STA : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);
            switch (colorValue)
            {
                case 1:
                    return new SolidColorBrush(Colors.White);
                case 2:
                    return new SolidColorBrush(Colors.Lime);
                case 3:
                    return new SolidColorBrush(Colors.DarkGreen);
                case 4:
                    return new SolidColorBrush(Colors.DarkGreen);
                case 5:
                    return new SolidColorBrush(Colors.Green);
                case 6:
                    return new SolidColorBrush(Colors.Green);

            }
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LZW_Int16_CM_DI_Mode : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);

            //  int Border = colorValue >> 8;
            int Mode = colorValue & 0xff;


            if (Mode >= 5 || Mode <= 1)
            {
                return new SolidColorBrush(Colors.White);
            }
            if (Mode == 3 || Mode == 4)
            {
                return new SolidColorBrush(Colors.Red);
            }
            if (Mode == 2)
            {
                return new SolidColorBrush(Colors.Blue);
            }
            else
                return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LZW_Int16_CM_DI_Border : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);

            int Border = colorValue >> 8;
            // int Mode = colorValue & 0xff;


            if (Border == 3)
            {
                return new SolidColorBrush(Colors.Yellow);
            }
            if (Border != 3 && Border != 1)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
                return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LZW_Int16_CM_AO_STA : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);
            switch (colorValue)
            {
                case 1:
                    return new SolidColorBrush(Colors.White);
                case 2:
                    return new SolidColorBrush(Colors.Lime);
                case 3:
                    return new SolidColorBrush(Colors.DarkGreen);
                case 4:
                    return new SolidColorBrush(Colors.DarkGreen);
                case 5:
                    return new SolidColorBrush(Colors.Green);
                case 6:
                    return new SolidColorBrush(Colors.Green);

            }
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LZW_Int16_CM_AO_Mode : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);

            //  int Border = colorValue >> 8;
            int Mode = colorValue & 0xff;


            if (Mode >= 5 || Mode <= 1)
            {
                return new SolidColorBrush(Colors.White);
            }
            if (Mode == 3 || Mode == 4)
            {
                return new SolidColorBrush(Colors.Red);
            }
            if (Mode == 2)
            {
                return new SolidColorBrush(Colors.Blue);
            }
            else
                return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LZW_Int16_CM_AO_Border : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);

            int Border = colorValue >> 8;
            // int Mode = colorValue & 0xff;


            if (Border == 3)
            {
                return new SolidColorBrush(Colors.Yellow);
            }
            if (Border != 3 && Border != 1)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
                return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LZW_Int16_CM_AI_STA : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);
            switch (colorValue)
            {
                case 1:
                    return new SolidColorBrush(Colors.White);
                case 2:
                    return new SolidColorBrush(Colors.Yellow);
                case 3:
                    return new SolidColorBrush(Colors.Yellow);
                case 4:
                    return new SolidColorBrush(Colors.White);
                case 5:
                    return new SolidColorBrush(Colors.White);
                case 6:
                    return new SolidColorBrush(Colors.White);

            }
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LZW_Int16_CM_AI_Mode : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);

            //  int Border = colorValue >> 8;
            int Mode = colorValue & 0xff;


            if (Mode >= 5 || Mode <= 1)
            {
                return new SolidColorBrush(Colors.White);
            }
            if (Mode == 3 || Mode == 4)
            {
                return new SolidColorBrush(Colors.Red);
            }
            if (Mode == 2)
            {
                return new SolidColorBrush(Colors.Blue);
            }
            else
                return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LZW_Int16_CM_AI_Border : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colorValue = System.Convert.ToInt32(value);

            int Border = colorValue >> 8;
            // int Mode = colorValue & 0xff;


            if (Border == 3)
            {
                return new SolidColorBrush(Colors.Yellow);
            }
            if (Border != 3 && Border != 1)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
                return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
