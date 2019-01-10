using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;

namespace NeithDevices.serial
{
    public class SerialPort : PInvokeSerialPort.SerialPort
    {
        private readonly MemoryTributary inBuffer =new MemoryTributary();
        public int ReadTimeout
        {
            get
            {
                return inBuffer.ReadTimeout;
            }
            set
            {
                inBuffer.ReadTimeout = value;
            }
        }
        public int WriteTimeout
        {
            get
            {
                return SendTimeoutConstant;
            }
            set
            {
                SendTimeoutConstant = value;
            }
        }
        public bool IsOpen
        {
            get
            {
                return Online;
            }
        }

        public SerialPort(string name) : base(name)
        {
            DataReceived += x => { lock (inBuffer) { inBuffer.AppendByte(x); } }; 
        }

        ~SerialPort()
        {
            Dispose();
        }


        public void DiscardInBuffer()
        {
            lock (inBuffer)
            {
                try
                {
                    inBuffer.Flush();
                }
                catch (Exception) { }
                inBuffer.SetLength(0);
                inBuffer.Position = 0;
            }
        }

        public void DiscardOutBuffer()
        {
            try
            {
                base.Flush();
            }
            catch (Exception) { }
        }

        public void Read(byte[] bytes)
        {
            Read(bytes,0,bytes.Length);
        }

        public byte[] Read(int count)
        {
            byte[] bytes = new byte[count];
            Read(bytes,0,count);
            return bytes;
        }

        public void Read(byte[] bytes, int offset, int count)
        {
            //lock (inBuffer)
            //{
                inBuffer.Read(bytes, offset, count);
            //}
        }

        public byte ReadByte()
        {
            //lock (inBuffer)
            //{
                return (byte)inBuffer.ReadByte();
            //}
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
            base.Write(bytes);
        }

        public new void Write(byte[] bytes)
        {
            base.Write(bytes);
        }

        public void WriteByte(byte data)
        {
            base.Write(data);
        }

        public new void Flush()
        {
            try
            {
                base.Flush();
            }
            catch (Exception) { }
            lock (inBuffer)
            {
                try
                {
                    inBuffer.Flush();
                }
                catch (Exception) { }
            }
        }

        public new void Close()
        {
            try
            {
                base.Flush();
            }
            catch (Exception) { }
            finally
            {
                base.Close();
            }
            lock (inBuffer)
            {
                try
                {
                    inBuffer.Flush();
                }
                catch (Exception) { }
                finally
                {
                    inBuffer.Close();
                }
            }
        }

        public new void Dispose()
        {
            if (IsOpen)
            {
                Close();
            }
            base.Dispose();
            lock (inBuffer)
            {
                inBuffer.Dispose();
            }
        }
    }
}
