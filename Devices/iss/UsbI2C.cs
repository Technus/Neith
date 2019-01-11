using System;
using NeithDevices.serial;
using NeithCore.utility;
using System.Collections.Generic;
using System.Threading;

namespace NeithDevices.iss
{
    public partial class UsbISS : SerialPort
    {
        private bool TestPresenceCapable;

        public TriState TestPresenceI2C(byte address)
        {
            if (TestPresenceCapable)
            {
                try
                {
                    this.DiscardInBuffer();
                    this.Write(CommandPrefixI2C.I2C_TEST, address);
                    return this.ReadByte() != 0x00 ? TriState.True : TriState.False;
                }
                catch (TimeoutException)
                {
                    this.DiscardOutBuffer();
                    this.DiscardInBuffer();
                }
            }
            else
            {
                throw new NotSupportedException("Testing presence is not supported");
            }
            return TriState.UnknownOrNull;
        }

        public HashSet<byte> PresentAddresses8BitI2C()
        {
            HashSet<byte> states = new HashSet<byte>();
            for (int i = 0; i <= 255; i++)
            {
                if (TestPresenceI2C((byte)i) == TriState.True)
                {
                    states.Add((byte)i);
                }
            }
            return states;
        }

        public HashSet<byte> PresentValidAddresses8BitI2C()
        {
            HashSet<byte> states = new HashSet<byte>();
            for (int i = (0x08<<1); i <=(0x77<<1); i+=2)
            {
                if (TestPresenceI2C((byte)i) == TriState.True)
                {
                    states.Add((byte)i);
                }
            }
            return states;
        }

        public HashSet<byte> PresentValidAddresses7BitI2C()
        {
            HashSet<byte> states = new HashSet<byte>();
            for (int i = (0x08 << 1); i <= (0x77 << 1); i += 2)
            {
                if (TestPresenceI2C((byte)i) == TriState.True)
                {
                    states.Add((byte)(i>>1));
                }
            }
            return states;
        }

        public byte? ReadI2C(byte address) 
        {
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixI2C.I2C_SGL, address|(byte)DirectionI2C.ReadBit);
                return (byte)this.ReadByte();
            }
            catch(TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return null;
            }
        }

        public bool WriteI2C(byte address,byte value)
        {
            try
            {
                this.Write(CommandPrefixI2C.I2C_SGL, address & (byte)DirectionI2C.WriteMask, value);
                return this.ReadByte() == 0?false:true;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public byte[] ReadI2C(byte address, byte count)
        {
            byte[] data = new byte[count];
            return ReadI2C(address, data)?data:null;
        }

        public bool ReadI2C(byte address, byte[] bytes)
        {
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixI2C.I2C_AD0, address | (byte)DirectionI2C.ReadBit, (byte)bytes.Length);
                this.Read(bytes);
                return true;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public bool WriteI2C(byte address, params byte[] value)
        {
            try
            {
                IConvertible[] bytes = new IConvertible[3 + value.Length];
                bytes[0] = CommandPrefixI2C.I2C_AD0;
                bytes[1] = address & (byte)DirectionI2C.WriteMask;
                bytes[2] = value.Length;
                for (int i = 3, j = 0; j < value.Length; i++, j++)
                {
                    bytes[i] = value[j];
                }
                this.Write(bytes);
                return this.ReadByte() == 0 ? false : true;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public byte[] ReadI2C(byte address, byte internalAddress, byte count)
        {
            byte[] data = new byte[count];
            return ReadI2C(address,internalAddress, data) ? data : null;
        }

        public bool ReadI2C(byte address,byte internalAddress, byte[] bytes)
        {
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixI2C.I2C_AD1, address | (byte)DirectionI2C.ReadBit,internalAddress, (byte)bytes.Length);
                this.Read(bytes);
                return true;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public bool WriteI2C(byte address, byte internalAddress, params byte[] value)
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
                this.Write(bytes);
                return this.ReadByte() == 0 ? false : true;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public byte[] ReadI2C(byte address, byte internalAddressH, byte internalAddressL, byte count)
        {
            byte[] data = new byte[count];
            return ReadI2C(address,internalAddressH,internalAddressL, data) ? data : null;
        }

        public bool ReadI2C(byte address, byte internalAddressH, byte internalAddressL, byte[] bytes)
        {
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixI2C.I2C_AD2, address | (byte)DirectionI2C.ReadBit, internalAddressH,internalAddressL, (byte)bytes.Length);
                this.Read(bytes);
                return true;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public bool WriteI2C(byte address, byte internalAddressH, byte internalAddressL, params byte[] value)
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
                this.Write(bytes);
                return this.ReadByte() == 0 ? false : true;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public FailureDirectI2C? SendCustomPacketI2C(PacketI2C packet)
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
                        bytesToSend.Add((byte)((byte)CommandDirectI2C.I2CREAD | (byte)count));
                    }
                    readInstances++;
                }
                else if ((byte)CommandDirectI2C.I2CWRITE == obj.ToByte(Thread.CurrentThread.CurrentCulture))
                {
                    IConvertible[] write = packet.writeList[writeInstances];
                    int count = write.Length;
                    int currentPointer = 0;
                    while (count >= 16)
                    {
                        bytesToSend.Add((byte)CommandDirectI2C.I2CWRITE | 0x0F);
                        for (int i=0; i<16; currentPointer++,i++)
                        {
                            bytesToSend.Add(write[currentPointer].ToByte(Thread.CurrentThread.CurrentCulture));
                        }
                        count -= 16;
                    }
                    if (count > 0)
                    { 
                        bytesToSend.Add((byte)((byte)CommandDirectI2C.I2CWRITE | (byte)count));
                        for (; currentPointer<write.Length; currentPointer++)
                        {
                            bytesToSend.Add(write[currentPointer].ToByte(Thread.CurrentThread.CurrentCulture));
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
                this.Write(bytesToSend.ToArray());

                if (this.ReadByte() == 0xFF)
                {
                    if (readCount != this.ReadByte())
                    {
                        return FailureDirectI2C.RD_LENGTH;
                    }
                    byte[] read = this.Read(readCount);
                    int currentPointer = 0;
                    foreach (byte[] arr in packet.readList)
                    {
                        for (int i = 0; i < arr.Length; i++,currentPointer++)
                        {
                            arr[i] = read[currentPointer];
                        }
                    }
                    return null;
                }
                else
                {
                    return (FailureDirectI2C)this.ReadByte();
                }
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return FailureDirectI2C.TIMEOUT;
            }
        }
    }

    public class PacketI2C
    {
        public readonly List<IConvertible> list = new List<IConvertible>();
        public readonly List<byte[]> readList = new List<byte[]>();
        public readonly List<IConvertible[]> writeList = new List<IConvertible[]>();

        public PacketI2C()
        {
            list.Add(CommandPrefixI2C.I2C_DIRECT);
        }

        public PacketI2C AppendStart()
        {
            list.Add(CommandDirectI2C.I2CSTART);
            return this;
        }

        public PacketI2C AppendRestart()
        {
            list.Add(CommandDirectI2C.I2CRESTART);
            return this;
        }

        public PacketI2C AppendStop()
        {
            list.Add(CommandDirectI2C.I2CSTOP);
            return this;
        }

        public PacketI2C AppendNACK()
        {
            list.Add(CommandDirectI2C.I2CNACK);
            return this;
        }

        public PacketI2C AppendRead(int count)
        {
            AppendRead(new byte[count]);
            return this;
        }

        public PacketI2C AppendRead(byte[] bytes)
        {
            list.Add(CommandDirectI2C.I2CREAD);
            readList.Add(bytes);
            return this;
        }

        public PacketI2C AppendWrite(IConvertible[] values)
        {
            list.Add(CommandDirectI2C.I2CWRITE);
            writeList.Add(values);
            return this;
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
    public enum FailureDirectI2C : byte
    {
        TIMEOUT = 0x00,              		
        DEVICE = 0x01,              // no ack from device			
        BUF_OVRFLOW=0x02,                // buffer overflow (>60)
        RD_OVERFLOW=0x03,                // no room in buffer to read data
        WR_UNDERFLOW=0x04,               // not enough data provided
        RD_LENGTH=0xFF,
    };

    enum DirectionI2C : byte
    {
        WriteMask=0xFE,
        ReadBit=0x01,
    }
}
