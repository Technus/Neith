using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Threading;

namespace NeithDevices.serial
{
    public static class SerialPortExtensions
    {
        public static readonly CultureInfo cultureInfo = new CultureInfo("en-US");

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
        public static void Read(this SerialPortStream port, byte[] bytes)
        {
            Thread t = new Thread(()=>
            {
                try
                {
                    while (port.BytesToRead < bytes.Length)
                    {
                        Thread.Sleep(10);
                    }
                }
                catch { }
            });
            t.Start();
            t.Join(port.ReadTimeout);
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
        public static byte[] Read(this SerialPortStream port, int count)
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
        public static void Write<T>(this SerialPortStream port, params T[] enums) where T : IConvertible
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
        public static void Write(this SerialPortStream port, params IConvertible[] enums)
        {
            port.Write<IConvertible>(enums);
        }
    }
}
