using NeithDevices.serial;
using System;

namespace NeithDevices.iss
{
    public partial class UsbISS : SerialPort
    {
        public bool WritePinsIO(bool io1 = false, bool io2 = false,
            bool io3 = false, bool io4 = false)
        {
            int value = 0;
            value |= io1 ? 0x01:0;
            value |= io2 ? 0x02:0;
            value |= io3 ? 0x04:0;
            value |= io4 ? 0x08:0;
            return WritePinsIO((byte)value);
        }

        public bool WritePinsIO(byte io = 0)
        {
            try
            {
                this.Write(CommandPrefixIO.SETPINS, io);
                return this.ReadByte() == 0 ? false : true;
            }
            catch(TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public byte? ReadBytePinsIO()
        {
            try
            {
                this.Write(CommandPrefixIO.GETPINS);
                return (byte)this.ReadByte();
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return null;
            }
        }

        public bool[] ReadPinsIO()
        {
            byte? read = ReadBytePinsIO();
            if (read == null)
            {
                return null;
            }
            else
            {
                return new bool[]
                {
                    (read.Value&0x01)!=0,
                    (read.Value&0x02)!=0,
                    (read.Value&0x04)!=0,
                    (read.Value&0x08)!=0,
                };
            }
        }

        public int? ReadPinAnalog(byte pinNumber)
        {
            try
            {
                this.Write(CommandPrefixIO.GETAD,pinNumber);
                return (this.ReadByte()<<8)| this.ReadByte();
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return null;
            }
        }
    }

    public enum CommandPrefixIO : byte
    {
        SETPINS = 0x63,         // [SETPINS] [pin states]
        GETPINS = 0x64,         // 

        GETAD = 0x65,           // [GETAD] [pin to convert]
    };
}
