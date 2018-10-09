using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMIControl.HMIBase
{
    class LZW_HMIHelper
    {
        public static bool GetBitValue(int value, ushort index)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");
            var val = 1 << index;
            return (value & val) == val;
        }

        public static int SetBitValue(int value, ushort index, bool bitValue)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");
            var val = 1 << index;
            return bitValue ? (value | val) : (value & ~val);
        }
    }
}
