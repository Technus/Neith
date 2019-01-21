using NeithDevices.iss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeithDevices.i2c
{
    public class DeviceMCP23017
    {
        public readonly byte address;
        public readonly UsbISS iss;

        public DeviceMCP23017(UsbISS parentISS,byte address7bit)
        {
            address = (byte)(address7bit<<1);
            iss= parentISS;
        }

        private byte ReadInternal(ControlRegister register, bool bank=false)
        {
            return iss.ReadI2C(address, register.GetAddress(bank), 1)[0];
        }

        private bool WriteInternal(ControlRegister register,byte data, bool bank = false)
        {
            return iss.WriteI2C(address, register.GetAddress(bank), data);
        }

        private ushort ReadInternal2(ControlRegister2 register)
        {
            return (ushort)(iss.ReadI2C(address, register, 1)[0] | (iss.ReadI2C(address, register + 1, 1)[0] << 8));
        }

        private bool WriteInternal2(ControlRegister2 register, ushort data)
        {
            return iss.WriteI2C(address, register+1, (byte)((data >> 8) & 0xff)) && iss.WriteI2C(address, register, (byte)(data & 0xff));
        }

        public enum ControlRegister : ushort
        {
            DEFVALA = 0x0306,
            DEFVALB = 0x1307,
            GPINTENA = 0x0204,
            GPINTENB = 0x1205,
            GPIOA = 0x0912,
            GPIOB = 0x1913,
            GPPUA = 0x060C,
            GPPUB = 0x160D,
            INTCAPA = 0x0810,
            INTCAPB = 0x1811,
            INTCONA = 0x0408,
            INTCONB = 0x1409,
            INTFA = 0x070E,
            INTFB = 0x170F,
            IOCON = 0x050A,
            IOCONA = 0x050A,
            IOCONB = 0x150B,
            IODIRA = 0x0000,
            IODIRB = 0x1001,
            IPOLA = 0x0102,
            IPOLB = 0x1103,
            OLATA = 0x0A14,
            OLATB = 0x1A15,
        }

        public enum ControlRegister2 : byte
        {
            IODIR = 0x00,
            IPOL = 0x02,
            GPINTEN = 0x04,
            DEFVAL = 0x06,
            INTCON = 0x08,
            IOCON = 0x0A,
            GPPU = 0x0C,
            INTF = 0x0E,
            INTCAP = 0x10,
            GPIO = 0x12,
            OLAT = 0x14,
        }
    }

    public  static partial class Extensions
    {
        public static byte GetAddress(this DeviceMCP23017.ControlRegister register, bool bank=false)
        {
            return (byte)(bank ? ((byte)register >> 8) & 0xFF : (byte)register & 0xFF);
        }
    }
}
