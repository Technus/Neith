using System;
using System.Threading;

namespace NeithCore.utility
{
    public enum QuadState : byte { False, True, Unknown, Null }
    public enum TriState : byte { False, True, UnknownOrNull }

    public static class Extensions
    {
        public static T GetOrDefault<T>(this T? nullable,T defaultT=default) where T : struct
        {
            if (nullable.HasValue)
            {
                return nullable.Value;
            }
            else
            {
                return defaultT;
            }
        }

        public static IConvertible[] AsIConvertibleArray(this byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            var arr = new IConvertible[data.Length];
            for (int i = 0,length=data.Length; i < length; i++)
            {
                arr[i] = data[i];
            }
            return arr;
        }

        public static IConvertible[] AsIConvertibleArray(this byte data)
        {
            return new IConvertible[] { data };
        }

        public static byte[] AsByteArray(this IConvertible[] data)
        {
            if (data == null)
            {
                return null;
            }
            var arr = new byte[data.Length];
            for (int i = 0, length = data.Length; i < length; i++)
            {
                arr[i] = data[i].ToByte();
            }
            return arr;
        }

        public static void CopyToByteArray(this IConvertible[] data,byte[] target)
        {
            if (data == null || target==null)
            {
                return;
            }
            for (int i = 0, max = Math.Min(data.Length, target.Length); i < max; i++){
                target[i] = data[i].ToByte();
            }
        }

        public static byte ToByte(this IConvertible data)
        {
            return data.ToByte(Thread.CurrentThread.CurrentCulture);
        }

        public static ushort ToUInt16(this IConvertible data)
        {
            return data.ToUInt16(Thread.CurrentThread.CurrentCulture);
        }

        public static uint ToUInt32(this IConvertible data)
        {
            return data.ToUInt32(Thread.CurrentThread.CurrentCulture);
        }

        public static ulong ToUInt64(this IConvertible data)
        {
            return data.ToUInt64(Thread.CurrentThread.CurrentCulture);
        }


        public static sbyte ToSByte(this IConvertible data)
        {
            return data.ToSByte(Thread.CurrentThread.CurrentCulture);
        }

        public static short ToInt16(this IConvertible data)
        {
            return data.ToInt16(Thread.CurrentThread.CurrentCulture);
        }

        public static int ToInt32(this IConvertible data)
        {
            return data.ToInt32(Thread.CurrentThread.CurrentCulture);
        }

        public static long ToInt64(this IConvertible data)
        {
            return data.ToInt64(Thread.CurrentThread.CurrentCulture);
        }
    }
}
