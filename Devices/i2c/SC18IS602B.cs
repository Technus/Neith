using NeithDevices.iss;
using NeithDevices.spi;
using System;

namespace NeithDevices.i2c
{
    public class DeviceSC18IS602B:IBusSPI
    {
        public readonly byte address;
        public readonly UsbISS iss;

        public DeviceSC18IS602B(UsbISS parentISS, byte address7bit)
        {
            address = (byte)(address7bit << 1);
            iss = parentISS;
        }

        public bool WriteBuffer(SlaveSelect slaveSelect,byte[] dataSPI)
        {
            iss.AwaitReady(address);
            byte[] bytes = new byte[dataSPI.Length + 1];
            bytes[0] = (byte)((byte)Function.Write | (byte)slaveSelect);
            Buffer.BlockCopy(dataSPI, 0, bytes, 1, dataSPI.Length);
            return iss.WriteI2C(address, bytes);
        }

        public byte[] ReadBuffer(int count)
        {
            iss.AwaitReady(address);
            return iss.ReadI2C(address, count);
        }

        public bool Configure(bool firstLSB=false,Mode mode=Mode.DataOnLeadingEdge_IdleLowCLK,Clock clock=Clock.Freq1843kHz)
        {
            iss.AwaitReady(address);
            return iss.WriteI2C(address, (byte)Function.Configure, (byte)((byte)mode|(byte)clock));
        }

        public bool Configure(Configuration configuration)
        {
            iss.AwaitReady(address);
            return iss.WriteI2C(address,(byte)Function.Configure, (byte)configuration);
        }

        public bool ClearInterrupt()
        {
            iss.AwaitReady(address);
            return iss.WriteOneI2C(address, Function.ClearInterrupt);
        }

        public bool EnterIdleMode()
        {
            iss.AwaitReady(address);
            return iss.WriteOneI2C(address, Function.IdleMode);
        }

        public bool Write(ControlRegister register,byte value)
        {
            iss.AwaitReady(address);
            return iss.WriteI2C(address, (byte)register, value);
        }

        public byte ReadToBufferGPIO()
        {
            iss.AwaitReady(address);
            if(iss.WriteOneI2C(address, Function.ReadGPIO))
            {
                return ReadBuffer(1)[0];
            }
            else
            {
                throw new Exception("Failed to write ReadGPIO command");
            }
        }

        public enum Function : byte
        {
            Write = 0x00,
            Configure = 0xF0,
            ClearInterrupt =0xF1,
            IdleMode =0xF2,
            ReadGPIO=0xF5,
        }

        public enum ControlRegister : byte
        {
            WriteGPIO = 0xF4,
            EnableGPIO = 0xF6,
            ConfigureGPIO = 0xF7,
        }

        [Flags]
        public enum SlaveSelect : byte
        {
            SS0=0b0001,
            SS1=0b0010,
            SS2=0b0100,
            SS3=0b1000,
            ALL=0b1111,
        }

        public enum Mode : byte
        {
            DataOnLeadingEdge_IdleLowCLK=0b0000,
            DataOnTrailingEdge_IdleLowCLK=0b0100,
            DataOnLeadingEdge_IdleHighCLK=0b1000,
            DataOnTrailingEdge_IdleHighCLK = 0b1100,
        }

        public enum Clock : byte
        {
            Freq1843kHz=0b00,
            Freq461kHz=0b01,
            Freq115kHz=0b10,
            Freq58kHz=0b11,
        }

        [Flags]
        public enum Configuration : byte
        {
            F0 = 0b00000001,
            F1 = 0b00000010,
            MODE0 = 0b00000100,
            CPHA = 0b00000100,
            MODE1 = 0b00001000,
            CPOL = 0b00001000,
            ORDER = 0b00100000,
        }
    }
}
