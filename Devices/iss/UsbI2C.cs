using System;
using System.IO.Ports;
using NeithDevices.serial;
using NeithCore.utility;
using System.Collections.Generic;
using System.Diagnostics;
using RJCP.IO.Ports;

namespace NeithDevices.iss
{
    public partial class UsbISS : SerialPortStream
    {
        private static readonly PacketI2C testPacket = new PacketI2C().AppendStart().AppendStop();

        private QuadState TestPresenceCapable = QuadState.Unknown;

        public QuadState TestPresenceI2C(byte address)
        {
            switch (TestPresenceCapable)
            {
                case QuadState.True:
                    {
                        try
                        {
                            DiscardInBuffer();
                            this.Write(CommandPrefixI2C.I2C_TEST, address);
                            return ReadByte() != 0x00? QuadState.True: QuadState.False;
                        }
                        catch(TimeoutException)
                        {
                            DiscardInBuffer();
                            DiscardOutBuffer();
                            return QuadState.Null;
                        }
                    }
                case QuadState.Unknown:
                    {
                        try
                        {
                            Version version = ReadVersion();
                            TestPresenceCapable = version!=null && version.FirmwareVersion >= 5 ? QuadState.True : QuadState.False;
                        }
                        catch(TimeoutException)
                        {
                            DiscardInBuffer();
                            DiscardOutBuffer();
                            TestPresenceCapable = QuadState.Null;
                        }
                        return TestPresenceI2C(address);
                    }
                default:
                    {
                        return SendCustomPacketI2C(testPacket) == null?QuadState.True:QuadState.Unknown;
                    }
            }
        }

        public QuadState[] TestPresenceI2C()
        {
            QuadState[] states = new QuadState[128];
            for(int i = 0,j=1; i < states.Length; i++,j+=2)
            {
                states[i] = TestPresenceI2C((byte)j);
            }
            return states;
        }
        public List<byte> PresentAddressesI2C()
        {
            List<byte> states = new List<byte>();
            for (int i = 0, j = 1; i < 128; i++, j += 2)
            {
                if(TestPresenceI2C((byte)j) == QuadState.True)
                {
                    states.Add((byte)j);
                }
            }
            return states;
        }

        public byte? ReadI2C(byte address) 
        {
            try
            {
                DiscardInBuffer();
                this.Write(CommandPrefixI2C.I2C_SGL, address|(byte)DirectionI2C.ReadBit);
                return (byte)ReadByte();
            }
            catch(TimeoutException)
            {
                DiscardInBuffer();
                DiscardOutBuffer();
                return null;
            }
        }

        public bool WriteI2C(byte address,byte value)
        {
            try
            {
                this.Write(CommandPrefixI2C.I2C_SGL, address & (byte)DirectionI2C.WriteMask, value);
                return ReadByte() == 0?false:true;
            }
            catch
            {
                DiscardInBuffer();
                DiscardOutBuffer();
                return false;
            }
        }

        public byte[] ReadI2C(byte address, byte count)
        {
            byte[] data = new byte[count];
            return ReadI2C(address, data)?data:null;
        }

        public bool ReadI2C(byte address, params byte[] bytes)
        {
            try
            {
                DiscardInBuffer();
                this.Write(CommandPrefixI2C.I2C_AD0, address | (byte)DirectionI2C.ReadBit, (byte)bytes.Length);
                this.Read(bytes);
                return true;
            }
            catch (TimeoutException)
            {
                DiscardInBuffer();
                DiscardOutBuffer();
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
                return ReadByte() == 0 ? false : true;
            }
            catch
            {
                DiscardInBuffer();
                DiscardOutBuffer();
                return false;
            }
        }

        public byte[] ReadI2C(byte address, byte internalAddress, byte count)
        {
            byte[] data = new byte[count];
            return ReadI2C(address,internalAddress, data) ? data : null;
        }

        public bool ReadI2C(byte address,byte internalAddress, params byte[] bytes)
        {
            try
            {
                DiscardInBuffer();
                this.Write(CommandPrefixI2C.I2C_AD1, address | (byte)DirectionI2C.ReadBit,internalAddress, (byte)bytes.Length);
                this.Read(bytes);
                return true;
            }
            catch (TimeoutException)
            {
                DiscardInBuffer();
                DiscardOutBuffer();
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
                return ReadByte() == 0 ? false : true;
            }
            catch
            {
                DiscardInBuffer();
                DiscardOutBuffer();
                return false;
            }
        }

        public byte[] ReadI2C(byte address, byte internalAddressH, byte internalAddressL, byte count)
        {
            byte[] data = new byte[count];
            return ReadI2C(address,internalAddressH,internalAddressL, data) ? data : null;
        }

        public bool ReadI2C(byte address, byte internalAddressH, byte internalAddressL, params byte[] bytes)
        {
            try
            {
                DiscardInBuffer();
                this.Write(CommandPrefixI2C.I2C_AD2, address | (byte)DirectionI2C.ReadBit, internalAddressH,internalAddressL, (byte)bytes.Length);
                this.Read(bytes);
                return true;
            }
            catch (TimeoutException)
            {
                DiscardInBuffer();
                DiscardOutBuffer();
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
                return ReadByte() == 0 ? false : true;
            }
            catch
            {
                DiscardInBuffer();
                DiscardOutBuffer();
                return false;
            }
        }

        public FailureDirectI2C? SendCustomPacketI2C(PacketI2C packet)
        {
            List<byte> bytesToSend = new List<byte>();
            int readInstances = 0, writeInstances = 0, readCount=0;
            foreach (IConvertible obj in packet.list)
            {
                if ((byte)CommandDirectI2C.I2CREAD==obj.ToByte(SerialPortExtensions.cultureInfo))
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
                else if ((byte)CommandDirectI2C.I2CWRITE == obj.ToByte(SerialPortExtensions.cultureInfo))
                {
                    IConvertible[] write = packet.writeList[writeInstances];
                    int count = write.Length;
                    int currentPointer = 0;
                    while (count >= 16)
                    {
                        bytesToSend.Add((byte)CommandDirectI2C.I2CWRITE | 0x0F);
                        for (int i=0; i<16; currentPointer++,i++)
                        {
                            bytesToSend.Add(write[currentPointer].ToByte(SerialPortExtensions.cultureInfo));
                        }
                        count -= 16;
                    }
                    if (count > 0)
                    { 
                        bytesToSend.Add((byte)((byte)CommandDirectI2C.I2CWRITE | (byte)count));
                        for (; currentPointer<write.Length; currentPointer++)
                        {
                            bytesToSend.Add(write[currentPointer].ToByte(SerialPortExtensions.cultureInfo));
                        }
                    }
                    writeInstances++;
                }
                else
                {
                    bytesToSend.Add(obj.ToByte(SerialPortExtensions.cultureInfo));
                }
            }

            try
            {
                this.Write(bytesToSend.ToArray());

                if (ReadByte() == 0xFF)
                {
                    if (readCount != ReadByte())
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
                    return (FailureDirectI2C)ReadByte();
                }
            }
            catch
            {
                DiscardInBuffer();
                DiscardOutBuffer();
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
