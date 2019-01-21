using NeithDevices.iss;
using System;
using System.Threading;

namespace NeithDevices.i2c
{
    public class Device24LC64
    {
        public readonly byte address;
        public readonly UsbISS iss;

        public Device24LC64(UsbISS parentISS, byte address7bit)
        {
            address = (byte)(address7bit << 1);
            iss = parentISS;
        }

        public bool Write(ushort pointer,byte data)
        {
            iss.AwaitReady(address);
            return iss.WriteI2C(address, (byte)((pointer >> 8) & 0xff), (byte)pointer & 0xff,data);
        }

        public byte Read(ushort pointer)
        {
            iss.AwaitReady(address);
            return iss.ReadI2C(address, (byte)((pointer >> 8) & 0xff), (byte)pointer & 0xff, 1)[0];
        }

        public bool Write(ushort pointer, byte[] data)
        {
            iss.AwaitReady(address);
            return iss.WriteI2C(address, (byte)((pointer >> 8) & 0xff), (byte)pointer & 0xff,data);
        }

        public byte[] Read(ushort pointer,int count)
        {
            iss.AwaitReady(address);
            return iss.ReadI2C(address, (byte)((pointer >> 8) & 0xff), (byte)pointer & 0xff, count);
        }
    }
}
