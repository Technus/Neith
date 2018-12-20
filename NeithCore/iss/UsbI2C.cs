using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeithDevices.serial;
using NeithCore.utility;

namespace NeithDevices.iss
{
    public partial class UsbISS : SerialPort
    {
        private QuadState TestPresenceCapable = QuadState.Unknown;

        public QuadState TestPresenceI2C(byte address)
        {
            switch (TestPresenceCapable)
            {
                case QuadState.True:
                    {
                        DiscardInBuffer();
                        this.Write(CommandPrefixI2C.I2C_TEST, address);
                        try
                        {
                            return ReadByte() != 0x00? QuadState.True: QuadState.False;
                        }
                        catch(TimeoutException)
                        {
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
                            TestPresenceCapable = QuadState.Null;
                        }
                        return TestPresenceI2C(address);
                    }
                default:return QuadState.Unknown;
            }
        }

        public byte? ReadOneI2C(byte address) 
        {
            DiscardInBuffer();
            this.Write(CommandPrefixI2C.I2C_SGL, address|(byte)DirectionI2C.ReadBit);
            try
            {
                return (byte)ReadByte();
            }
            catch
            {
                return null;
            }
        }

        public bool WriteOneI2C(byte address,byte value)
        {
            this.Write(CommandPrefixI2C.I2C_SGL, address & (byte)DirectionI2C.WriteMask, value);
            try
            {
                return ReadByte() == 0?false:true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class DirectI2C
    {

    }

    public class PacketI2C
    {

    }

    public class ResponseI2C
    {

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
        I2CSTART,               // send start sequence
        I2CRESTART,             // send restart sequence
        I2CSTOP,                // send stop sequence
        I2CNACK,                // send NACK after next read
        I2CREAD = 0x20,         // 0x20-0x2f, reads 1-16 bytes
        I2CWRITE = 0x30,        // 0x30-0x3f, writes next 1-16 bytes
    };
    // return from I2C_DIRECT is:
    // [(ACK] [Read Cnt] [Data1] Data2] ... [DataN]
    // or
    // [(NACK] [Reason]
    enum FailureReasonI2C : byte
    {
        DEVICE = 0x01,              // no ack from device			
        BUF_OVRFLOW,                // buffer overflow (>60)
        RD_OVERFLOW,                // no room in buffer to read data
        WR_UNDERFLOW,               // not enough data provided
    };

    enum DirectionI2C : byte
    {
        WriteMask=0xFE,
        ReadBit=0x01,
    }
}
