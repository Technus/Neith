using NeithDevices.iss;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeithDevices.i2c
{
    public class DevicePCA9670
    {
        //Input function when they are HIGH outputs (default!). (it has internal pullup, and driving it low can act as input)

        public readonly byte address;
        public readonly UsbISS iss;

        public readonly byte manufacturer;
        public readonly short partID;
        public readonly byte revision;

        public DevicePCA9670(UsbISS parentISS,byte address7bit)
        {
            address = (byte)(address7bit<<1);
            iss= parentISS;

            byte[] id = ReadStandardID(iss,3, address);
            if (id != null)
            {
                manufacturer = id[0];
                partID = (short)((id[1]<<5) | (id[2]>>3));
                revision = (byte)(id[2] & 0b11);
                Debug.WriteLine(address+" PASS "+manufacturer+" "+partID+" "+revision);
            }
            else
            {
                Debug.WriteLine(address+" FAIL");
                //throw new Exception("Unable to read ID");
            }
        }

        public bool Read(int position)
        {
            return (Read() & (0x1 << position)) != 0;
        }

        public byte Read()
        {
            return iss.ReadOneI2C(address).Value;
        }

        public bool Write(bool value,int position)
        {
            byte states = iss.ReadOneI2C(address).Value;
            if (value)
            {
                states |= (byte)(0x1 << position);
            }
            else
            {
                states &= (byte)~(0x1 << position);
            }
            return iss.WriteI2C(address, states);
        }

        public bool Write(byte states)
        {
            return iss.WriteI2C(address, states);
        }

        public static byte[] ReadStandardID(UsbISS iss, int count, byte address8Bit)
        {
            PacketI2C packetI2C = new PacketI2C()
                .AppendStart()
                .AppendWrite(new IConvertible[] { ReservedAdressI2C8BIT.DeviceIDCode, address8Bit })
                .AppendRestart()
                .AppendWrite(new IConvertible[] { (byte)ReservedAdressI2C8BIT.DeviceIDCode | (byte)DirectionI2C.ReadBit });
            if (count > 1)
            {
                packetI2C
                .AppendRead(count - 1)
                .AppendToNextReadNACK()//IMPORTANT NACK BEFORE 
                .AppendRead(1);
            }
            else
            {
                packetI2C
                .AppendToNextReadNACK()//IMPORTANT NACK BEFORE 
                .AppendRead(1);
            }
            packetI2C.AppendStop();

            if (iss.SendCustomPacketI2C(packetI2C) == ResultDirectI2C.OK)
            {
                byte[] result = new byte[count];
                int pointer = 0;
                foreach (var item in packetI2C.readList)
                {
                    foreach (var data in item)
                    {
                        result[pointer++] = data;
                    }
                }
                return result;
            }
            return null;
        }
    }
}
