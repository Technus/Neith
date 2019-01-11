using System;
using System.Threading;

namespace NeithDevices.serial
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class SerialPort : System.IO.Ports.SerialPort
    {
        public SerialPort(string name) : base(name)
        {

            ReadTimeout = 1000;
            WriteTimeout = 1000;
        }

        ~SerialPort()
        {
            try
            {
                Close();
            }
            catch (Exception) { }
            Dispose();
        }

        public new void Open()
        {
            base.Open();
        }

        public void Read(byte[] bytes)
        {
            Read(bytes, 0, bytes.Length);
        }

        public byte[] Read(int count)
        {
            byte[] bytes = new byte[count];
            Read(bytes, 0, count);
            return bytes;
        }

        public void Write(params IConvertible[] enums)
        {
            Write<IConvertible>(enums);
        }

        public void Write<T>(params T[] enums) where T : IConvertible
        {
            byte[] bytes = new byte[enums.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = enums[i].ToByte(Thread.CurrentThread.CurrentCulture);
            }
            Write(bytes);
        }

        public void Write(byte[] bytes)
        {
            base.Write(bytes, 0, bytes.Length);
        }
    }
}
