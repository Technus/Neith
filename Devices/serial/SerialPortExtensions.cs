using System;
using System.Globalization;
using System.IO.Ports;

namespace NeithDevices.serial
{
    public static class SerialPortExtensions
    {
        private static readonly CultureInfo cultureInfo = new CultureInfo("en-US");

        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The buffer passed is null.
        //
        //   T:System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The offset or count parameters are outside a valid region of the buffer being
        //     passed. Either offset or count is less than zero.
        //
        //   T:System.ArgumentException:
        //     offset plus count is greater than the length of the buffer.
        //
        //   T:System.TimeoutException:
        //     No bytes were available to read.
        public static void Read(this SerialPort port, byte[] bytes)
        {
            port.Read(bytes, 0, bytes.Length);
        }

        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The buffer passed is null.
        //
        //   T:System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The offset or count parameters are outside a valid region of the buffer being
        //     passed. Either offset or count is less than zero.
        //
        //   T:System.ArgumentException:
        //     offset plus count is greater than the length of the buffer.
        //
        //   T:System.TimeoutException:
        //     No bytes were available to read.
        public static byte[] Read(this SerialPort port, int count)
        {
            byte[] bytes = new byte[count];
            port.Read(bytes);
            return bytes;
        }

        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The buffer passed is null.
        //
        //   T:System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The offset or count parameters are outside a valid region of the buffer being
        //     passed. Either offset or count is less than zero.
        //
        //   T:System.ArgumentException:
        //     offset plus count is greater than the length of the buffer.
        //
        //   T:System.ServiceProcess.TimeoutException:
        //     The operation did not complete before the time-out period ended.
        public static void Write<T>(this SerialPort port, params T[] enums) where T : IConvertible
        {
            byte[] bytes = new byte[enums.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = enums[i].ToByte(cultureInfo);
            }
            port.Write(bytes, 0, bytes.Length);
        }

        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The buffer passed is null.
        //
        //   T:System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The offset or count parameters are outside a valid region of the buffer being
        //     passed. Either offset or count is less than zero.
        //
        //   T:System.ArgumentException:
        //     offset plus count is greater than the length of the buffer.
        //
        //   T:System.ServiceProcess.TimeoutException:
        //     The operation did not complete before the time-out period ended.
        public static void Write(this SerialPort port, params IConvertible[] enums)
        {
            port.Write<IConvertible>(enums);
        }
    }
}
