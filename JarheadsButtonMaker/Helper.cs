using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class Helper
{
    public static byte FloatToByte(float f)
    {
        return Convert.ToByte(Math.Floor(f*255));
    }

    public static float ByteToFloat(byte b)
    {
        return (1f/255f) * (float)b;
    }


}

