using System;
using NeithCore.utility;
using System.Collections.Generic;
using System.Threading;
using NeithDevices.i2c;

namespace NeithDevices.iss
{
    public partial class UsbISS : CSSSC.CSSSC,IBusI2C
    {
        private bool TestPresenceCapable;

        public bool TestPresenceI2C(IAddressI2C address)
        {
            if (address.Is7Bit() && TestPresenceCapable)
            {
                try
                {
                    DiscardInBuffer();
                    Write(CommandPrefixI2C.I2C_TEST, address.ToAddress7BitI2C().GetAddress8Bit(false));
                    return ReadByte() != 0x00;
                }
                catch (TimeoutException e)
                {
                    DiscardOutBuffer();
                    DiscardInBuffer();
                    throw e;
                }
            }
            else
            {
                try
                {
                    SendCustomPacketI2C(new PacketI2C(ActionI2C.Start, ActionI2C.SendAddress(address,false), ActionI2C.Stop));
                    return true;
                }
                catch (OperationNoDeviceACK)
                {
                    return false;
                }
            }
        }

        public void AwaitReady(IAddressI2C address, int timeout = 2000)
        {
            if (!SpinWait.SpinUntil(() => TestPresenceI2C(address), timeout))
            {
                throw new TimeoutException("TimedOut waiting for device ready!");
            }
        }

        public HashSet<IAddressI2C> PresentValidAddressesI2C(bool check7Bit=true,bool check10Bit=true)
        {
            HashSet<IAddressI2C> states = new HashSet<IAddressI2C>();
            if (check7Bit)
            {
                for (int i = Address7BitI2C.MIN_ADDRESS_VALUE; i <= Address7BitI2C.MAX_ADDRESS_VALUE; i++)
                {
                    Address7BitI2C address = i.ToAddress7BitI2C();
                    if (TestPresenceI2C(address))
                    {
                        states.Add(address);
                    }
                }
            }
            if (check10Bit)
            {
                for (int i = Address10BitI2C.MIN_ADDRESS_VALUE; i < Address10BitI2C.MAX_ADDRESS_VALUE; i++)
                {
                    Address10BitI2C address = i.ToAddress10BitI2C();
                    if (TestPresenceI2C(address))
                    {
                        states.Add(address);
                    }
                }
            }
            return states;
        }

        /*
        public byte ReadOneI2C(IAddressI2C address) 
        {
            if (address.Is7Bit())
            {
                try
                {
                    DiscardInBuffer();
                    Write(CommandPrefixI2C.I2C_SGL, address.ToAddress7BitI2C().GetAddress8Bit(true));
                    return ReadByte();
                }
                catch (TimeoutException e)
                {
                    DiscardInBuffer();
                    DiscardOutBuffer();
                    throw e;
                }
            }
            else
            {
                PacketI2C packet = new PacketI2C(ActionI2C.Start, ActionI2C.SendAddress(address,true), ActionI2C.Read(1), ActionI2C.Stop);
                SendCustomPacketI2C(packet);
                return packet[2][0].ToByte();
            }
        }

        public void WriteOneI2C(IAddressI2C address,IConvertible value)
        {
            if (address.Is7Bit())
            {
                try
                {
                    Write(CommandPrefixI2C.I2C_SGL, address.ToAddress7BitI2C().GetAddress8Bit(false), value);
                    if (ReadByte() == 0)
                    {
                        throw new OperationFailedI2C();
                    }
                }
                catch (TimeoutException e)
                {
                    DiscardInBuffer();
                    DiscardOutBuffer();
                    throw e;
                }
            }
            else
            {
                SendCustomPacketI2C(new PacketI2C(ActionI2C.Start, ActionI2C.SendAddress(address,false), ActionI2C.Write(value),ActionI2C.Stop));
            }
        }

        public byte[] ReadSimpleI2C(IAddressI2C address, int count)
        {
            byte[] data = new byte[count];
            ReadSimpleI2C(address, data);
            return data;
        }

        public void ReadSimpleI2C(IAddressI2C address, byte[] bytes)
        {
            if (address.Is7Bit())
            {
                try
                {
                    DiscardInBuffer();
                    Write(CommandPrefixI2C.I2C_AD0, address.ToAddress7BitI2C().GetAddress8Bit(true), (byte)bytes.Length);
                    Read(bytes);
                }
                catch (TimeoutException e)
                {
                    DiscardInBuffer();
                    DiscardOutBuffer();
                    throw e;
                }
            }
            else
            {
                PacketI2C packet = new PacketI2C(ActionI2C.Start, ActionI2C.SendAddress(address, true), ActionI2C.Read(bytes.Length), ActionI2C.Stop);
                SendCustomPacketI2C(packet);
                packet[2].Data.CopyToByteArray(bytes);
            }
        }

        public void WriteSimpleI2C(IAddressI2C address, params IConvertible[] value)
        {
            if (address.Is7Bit())
            {
                try
                {
                    IConvertible[] bytes = new IConvertible[3 + value.Length];
                    bytes[0] = CommandPrefixI2C.I2C_AD0;
                    bytes[1] = address.ToAddress7BitI2C().GetAddress8Bit(false);
                    bytes[2] = value.Length;
                    for (int i = 3, j = 0; j < value.Length; i++, j++)
                    {
                        bytes[i] = value[j];
                    }
                    Write(bytes);
                    if (ReadByte() == 0)
                    {
                        throw new OperationFailedI2C();
                    }
                }
                catch (TimeoutException e)
                {
                    DiscardInBuffer();
                    DiscardOutBuffer();
                    throw e;
                }
            }
            else
            {
                SendCustomPacketI2C(new PacketI2C(ActionI2C.Start, ActionI2C.SendAddress(address, false), ActionI2C.Write(value), ActionI2C.Stop));
            }
        }

        public byte[] ReadAddressedI2C(IAddressI2C address, IConvertible internalAddressByte, int count)
        {
            byte[] data = new byte[count];
            ReadAddressedI2C(address,internalAddressByte, data);
            return data;
        }

        public void ReadAddressedI2C(IAddressI2C address,IConvertible internalAddress, byte[] bytes)
        {
            if (address.Is7Bit())
            {
                try
                {
                    DiscardInBuffer();
                    Write(CommandPrefixI2C.I2C_AD1, address.ToAddress7BitI2C().GetAddress8Bit(true), internalAddress, (byte)bytes.Length);
                    Read(bytes);
                }
                catch (TimeoutException e)
                {
                    DiscardInBuffer();
                    DiscardOutBuffer();
                    throw e;
                }
            }
            else
            {
                PacketI2C packet = new PacketI2C(ActionI2C.Start,ActionI2C.SendAddress(address,false),ActionI2C.Write(internalAddress),
                                                ActionI2C.Restart,ActionI2C.SendAddress(address,true),ActionI2C.Read(bytes.Length));

            }
        }

        public bool WriteI2C(byte address, IConvertible internalAddress, params byte[] value)
        {
            try
            {
                IConvertible[] bytes = new IConvertible[4 + value.Length];
                bytes[0] = CommandPrefixI2C.I2C_AD1;
                bytes[1] = address & (byte)DirectionI2C.WriteMask;
                bytes[2] = internalAddress;
                bytes[3] = value.Length;
                for (int i = 4, j = 0; j < value.Length; i++, j++)
                {
                    bytes[i] = value[j];
                }
                Write(bytes);
                return ReadByte() == 0 ? false : true;
            }
            catch (TimeoutException)
            {
                DiscardInBuffer();
                DiscardOutBuffer();
                return false;
            }
        }

        public byte[] ReadI2C(byte address, IConvertible internalAddressH, IConvertible internalAddressL, int count)
        {
            byte[] data = new byte[count];
            return ReadI2C(address,internalAddressH,internalAddressL, data) ? data : null;
        }

        public bool ReadI2C(byte address, IConvertible internalAddressH, IConvertible internalAddressL, byte[] bytes)
        {
            try
            {
                DiscardInBuffer();
                Write(CommandPrefixI2C.I2C_AD2, address | (byte)DirectionI2C.ReadBit, internalAddressH,internalAddressL, (byte)bytes.Length);
                Read(bytes);
                return true;
            }
            catch (TimeoutException)
            {
                DiscardInBuffer();
                DiscardOutBuffer();
                return false;
            }
        }

        public bool WriteI2C(byte address, IConvertible internalAddressH, IConvertible internalAddressL, params byte[] value)
        {
            try
            {
                IConvertible[] bytes = new IConvertible[5 + value.Length];
                bytes[0] = CommandPrefixI2C.I2C_AD2;
                bytes[1] = address & (byte)DirectionI2C.WriteMask;
                bytes[2] = internalAddressH;
                bytes[3] = internalAddressL;
                bytes[4] = value.Length;
                for (int i = 5, j = 0; j < value.Length; i++, j++)
                {
                    bytes[i] = value[j];
                }
                Write(bytes);
                return ReadByte() == 0 ? false : true;
            }
            catch (TimeoutException)
            {
                DiscardInBuffer();
                DiscardOutBuffer();
                return false;
            }
        }

        private void SendCustomPacketI2C(PacketImplementationI2C packet)
        {
            List<byte> bytesToSend = new List<byte>();
            int readInstances = 0, writeInstances = 0, readCount=0;
            foreach (IConvertible obj in packet.list)
            {
                if ((byte)CommandDirectI2C.I2CREAD==obj.ToByte(Thread.CurrentThread.CurrentCulture))
                {
                    byte[] read = packet.readList[readInstances];
                    int count = read.Length;
                    readCount += count;
                    while (count >= 16)
                    {
                        bytesToSend.Add((byte)CommandDirectI2C.I2CREAD|0x0F);
                        count -= 16;
                    }
                    if (count > 0)
                    {
                        bytesToSend.Add((byte)((byte)CommandDirectI2C.I2CREAD | (byte)(count-1)));
                    }
                    readInstances++;
                }
                else if ((byte)CommandDirectI2C.I2CWRITE == obj.ToByte(Thread.CurrentThread.CurrentCulture))
                {
                    IConvertible[] write = packet.writeList[writeInstances];
                    int count = write.Length;
                    int writeCount = 0;
                    while (count >= 16)
                    {
                        bytesToSend.Add((byte)CommandDirectI2C.I2CWRITE | 0x0F);
                        for (int i=0; i<16; writeCount++,i++)
                        {
                            bytesToSend.Add(write[writeCount].ToByte(Thread.CurrentThread.CurrentCulture));
                        }
                        count -= 16;
                    }
                    if (count > 0)
                    { 
                        bytesToSend.Add((byte)((byte)CommandDirectI2C.I2CWRITE | (byte)(count - 1)));
                        for (; writeCount<write.Length; writeCount++)
                        {
                            bytesToSend.Add(write[writeCount].ToByte(Thread.CurrentThread.CurrentCulture));
                        }
                    }
                    writeInstances++;
                }
                else
                {
                    bytesToSend.Add(obj.ToByte(Thread.CurrentThread.CurrentCulture));
                }
            }

            try
            {
                Write(bytesToSend.ToArray());
                byte b;
                if ((b=ReadByte()) == (byte)ResultDirectI2C.OK)
                {
                    if (readCount != (b=ReadByte()))
                    {
                        return ResultDirectI2C.RD_LENGTH;
                    }
                    if (readCount > 0)
                    {
                        byte[] read = Read(readCount);
                        int currentPointer = 0;
                        foreach (byte[] arr in packet.readList)
                        {
                            for (int i = 0; i < arr.Length; i++, currentPointer++)
                            {
                                arr[i] = read[currentPointer];
                            }
                        }
                    }
                    return ResultDirectI2C.OK;
                }
                else
                {
                    return (ResultDirectI2C)ReadByte();
                }
            }
            catch (TimeoutException)
            {
                DiscardInBuffer();
                DiscardOutBuffer();
                return ResultDirectI2C.TIMEOUT;
            }
        }
        */

        public void SendCustomPacketI2C(PacketI2C packet)
        {
            var bytesToSend=
            foreach (var action in packet)
            {
                switch (action.Type)
                {

                }
            }

            List<byte> bytesToSend = new List<byte>();
            int readInstances = 0, writeInstances = 0, readCount = 0;
            foreach (IConvertible obj in packet.list)
            {
                if ((byte)CommandDirectI2C.I2CREAD == obj.ToByte(Thread.CurrentThread.CurrentCulture))
                {
                    byte[] read = packet.readList[readInstances];
                    int count = read.Length;
                    readCount += count;
                    while (count >= 16)
                    {
                        bytesToSend.Add((byte)CommandDirectI2C.I2CREAD | 0x0F);
                        count -= 16;
                    }
                    if (count > 0)
                    {
                        bytesToSend.Add((byte)((byte)CommandDirectI2C.I2CREAD | (byte)(count - 1)));
                    }
                    readInstances++;
                }
                else if ((byte)CommandDirectI2C.I2CWRITE == obj.ToByte(Thread.CurrentThread.CurrentCulture))
                {
                    IConvertible[] write = packet.writeList[writeInstances];
                    int count = write.Length;
                    int writeCount = 0;
                    while (count >= 16)
                    {
                        bytesToSend.Add((byte)CommandDirectI2C.I2CWRITE | 0x0F);
                        for (int i = 0; i < 16; writeCount++, i++)
                        {
                            bytesToSend.Add(write[writeCount].ToByte(Thread.CurrentThread.CurrentCulture));
                        }
                        count -= 16;
                    }
                    if (count > 0)
                    {
                        bytesToSend.Add((byte)((byte)CommandDirectI2C.I2CWRITE | (byte)(count - 1)));
                        for (; writeCount < write.Length; writeCount++)
                        {
                            bytesToSend.Add(write[writeCount].ToByte(Thread.CurrentThread.CurrentCulture));
                        }
                    }
                    writeInstances++;
                }
                else
                {
                    bytesToSend.Add(obj.ToByte(Thread.CurrentThread.CurrentCulture));
                }
            }

            try
            {
                Write(bytesToSend.ToArray());
                byte b;
                if ((b = ReadByte()) == (byte)ResultDirectI2C.OK)
                {
                    if (readCount != (b = ReadByte()))
                    {
                        return ResultDirectI2C.RD_LENGTH;
                    }
                    if (readCount > 0)
                    {
                        byte[] read = Read(readCount);
                        int currentPointer = 0;
                        foreach (byte[] arr in packet.readList)
                        {
                            for (int i = 0; i < arr.Length; i++, currentPointer++)
                            {
                                arr[i] = read[currentPointer];
                            }
                        }
                    }
                    return ResultDirectI2C.OK;
                }
                else
                {
                    return (ResultDirectI2C)ReadByte();
                }
            }
            catch (TimeoutException)
            {
                DiscardInBuffer();
                DiscardOutBuffer();
                return ResultDirectI2C.TIMEOUT;
            }
        }

        class PacketImplementationI2C : PacketI2C
        {
            public readonly List<IConvertible> list = new List<IConvertible>();
            public readonly List<byte[]> readList = new List<byte[]>();
            public readonly List<IConvertible[]> writeList = new List<IConvertible[]>();

            PacketImplementationI2C()
            {
                list.Add(CommandPrefixI2C.I2C_DIRECT);
            }

            PacketImplementationI2C AppendStart()
            {
                list.Add(CommandDirectI2C.I2CSTART);
                return this;
            }

            PacketImplementationI2C AppendRestart()
            {
                list.Add(CommandDirectI2C.I2CRESTART);
                return this;
            }

            PacketImplementationI2C AppendStop()
            {
                list.Add(CommandDirectI2C.I2CSTOP);
                return this;
            }

            PacketImplementationI2C AppendNACK()
            {
                throw new NotImplementedException();
            }

            PacketImplementationI2C AppendToNextReadNACK()
            {
                list.Add(CommandDirectI2C.I2CNACK);
                return this;
            }

            public PacketImplementationI2C AppendRead(int count)
            {
                if (count <= 0)
                {
                    AppendRead(null);
                }
                else
                {
                    AppendRead(new byte[count]);
                }
                return this;
            }

            public PacketImplementationI2C AppendRead(byte[] bytes)
            {
                list.Add(CommandDirectI2C.I2CREAD);
                readList.Add(bytes);
                return this;
            }

            public PacketImplementationI2C AppendWrite(IConvertible[] values)
            {
                list.Add(CommandDirectI2C.I2CWRITE);
                writeList.Add(values);
                return this;
            }

            public List<byte[]> GetReadList()
            {
                return readList;
            }

            public byte[] GetReadBytes()
            {
                int i = 0;
                foreach (var item in readList)
                {
                    i += item.Length;
                }


            }
        }
    }


    public enum CommandPrefixI2C : byte
    {
        I2C_SGL=0x53,       // Read/Write single byte for non-registered devices
        I2C_AD0=0x54,       // Read/Write multiple bytes for devices without internal address or where address does not require resetting
        I2C_AD1 =0x55,      // Read/Write 1 byte addressed devices (the majority of devices will use this one)
        I2C_AD2 =0x56,      // Read/Write 2 byte addressed devices
        I2C_DIRECT =0x57,   // Used to build your own custom I2C sequenc
        I2C_TEST =0x58,     // Used to check for the existence of an I2C device on the bus. (V5 or later firmware only)
    }

    // I2C DIRECT commands
    public enum CommandDirectI2C : byte
    {
        I2CSRP = 0x00,          // Start/Stop Codes - 0x01=start, 0x02=restart, 0x03=stop, 0x04=nack
        I2CSTART=0x01,               // send start sequence
        I2CRESTART=0x02,             // send restart sequence
        I2CSTOP=0x03,                // send stop sequence
        I2CNACK=0x04,                // send NACK after next read
        I2CREAD = 0x20,         // 0x20-0x2f, reads 1-16 bytes
        I2CWRITE = 0x30,        // 0x30-0x3f, writes next 1-16 bytes
    };
    // return from I2C_DIRECT is:
    // [(ACK] [Read Cnt] [Data1] Data2] ... [DataN]
    // or
    // [(NACK] [Reason]
    public enum ResultDirectI2C : byte
    {           		
        DEVICE = 0x01,              // no ack from device			
        BUF_OVRFLOW=0x02,                // buffer overflow (>60)
        RD_OVERFLOW=0x03,                // no room in buffer to read data
        WR_UNDERFLOW=0x04,               // not enough data provided
        TIMEOUT = 0xFD,
        RD_LENGTH =0xFE,
        OK=0xFF,
    };

    enum DirectionI2C : byte
    {
        WriteMask=0xFE,
        ReadBit=0x01,
    }

    enum ReservedAdressI2C8BIT : byte
    {
        GeneralCall = 0b00000000,
        CBUSAddress = 0b00000010,
        ReservedForDifferentBusFormat = 0b00000100,
        ReservedForFuturePurposes = 0b00000110,
        HsModeMasterCode = 0b00001000,
        HsModeMasterMask = 0b00000110,
        SlaveAdressing10BitCode = 0b11110000,
        SlaveAdressing10BitMask = 0b00000110,
        DeviceIDCode = 0b11111000,
        DeviceIDMask = 0b00000110,
        ReservedForFuturePurposesCode = 0b11111000,
        ReservedForFuturePurposesMask = 0b00000110,
    }

    enum ReservedAdressI2C7BIT : byte
    {
        GeneralCall=0b0000000,
        CBUSAddress=0b0000001,
        ReservedForDifferentBusFormat=0b0000010,
        ReservedForFuturePurposes=0b0000011,
        HsModeMasterCode=0b0000100,
        HsModeMasterMask=0b0000011,
        SlaveAdressing10BitCode=0b1111000,
        SlaveAdressing10BitMask=0b0000011,
        DeviceIDCode=0b1111100,
        DeviceIDMask=0b0000011,
        ReservedForFuturePurposesCode=0b1111100,
        ReservedForFuturePurposesMask=0b0000011,
    }
}
