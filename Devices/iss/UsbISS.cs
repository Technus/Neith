using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace NeithDevices.iss
{
    [System.ComponentModel.DesignerCategory("Code")]
    public partial class UsbISS : CSSSC.CSSSC
    {
        private static Dictionary<string, UsbISS> AttachedDevices = new Dictionary<string, UsbISS>();
        public static ReadOnlyDictionary<string, UsbISS> GetAttachedISS()
        {
            foreach (KeyValuePair<string,UsbISS> entry in AttachedDevices)
            {
                entry.Value.Dispose();
            }
            AttachedDevices.Clear();

            foreach(string name in System.IO.Ports.SerialPort.GetPortNames())
            {
                UsbISS port=null;
                try
                {
                    port = new UsbISS(name);
                    Debug.Print(name);
                    port.Open();
                }
                catch(Exception ex) when (ex is InvalidOperationException || ex is UnauthorizedAccessException)
                {
                    port.Dispose();
                    continue;
                }
                if (port != null && port.IsOpen)
                {
                    try
                    {
                        string serial = port.ReadSerialNumber();
                        if (serial != null)
                        {
                            Version version = port.ReadVersion();
                            if (version != null)
                            {
                                port.TestPresenceCapable = version.FirmwareVersion >= 5;
                                if (!port.TestPresenceCapable)
                                {
                                    throw new Exception(version.ToString());
                                }
                                AttachedDevices.Add(serial, port);
                                continue;
                            }
                        }
                        port.Dispose();
                    }
                    catch (TimeoutException)
                    {
                        port.Dispose();
                    }
                }
            }
            return new ReadOnlyDictionary<string, UsbISS>(AttachedDevices);
        }

        private UsbISS(string name) : base(name)
        {
            ReadTimeout = 2000;
        }

        ~UsbISS()
        {
            if (IsOpen)
            {
                Close();
            }
            Dispose();
        }

        public Version ReadVersion()
        {
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixISS.USB_ISS, CommandISS.ISS_VER);
                byte[] x = this.Read(3);
                return new Version(x);
            }
            catch(TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return null;
            }
        }

        public string ReadSerialNumber()
        {
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixISS.USB_ISS, CommandISS.GET_SER_NUM);
                string serial = Encoding.UTF8.GetString(this.Read(8));
                foreach (char c in serial)
                {
                    if (c > '9' || c < '0')
                    {
                        return null;
                    }
                }
                return serial;
            }
            catch(TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return null;
            }
        }

        public Mode? ReadMode()
        {
            Version version = ReadVersion();
            return version!=null?version.OperatingMode:(Mode?)null;
        }

        public Mode? WriteMode(Mode mode, params byte[] paramaters)
        {
            IConvertible[] bytes = new IConvertible[2 + paramaters.Length];
            bytes[0]=CommandPrefixISS.USB_ISS;
            bytes[1]=CommandISS.ISS_MODE;
            for(int i = 2, j=0; j < paramaters.Length; i++,j++)
            {
                bytes[i] = paramaters[j];
            }
            try
            {
                this.Write(bytes);
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
            }
            return this.ReadMode();
        }

        public bool WriteChangeIO(
            TypeIO io1 = TypeIO.DIGITAL_INPUT, TypeIO io2 = TypeIO.DIGITAL_INPUT,
            TypeIO io3 = TypeIO.DIGITAL_INPUT, TypeIO io4 = TypeIO.DIGITAL_INPUT)
        {
            byte val = (byte)((byte)io1 | ((byte)io2 << 2) | ((byte)io3 << 4) | ((byte)io4 << 6));
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixISS.USB_ISS, CommandISS.ISS_MODE, Mode.IO_CHANGE, val);
                return this.ReadByte() == 0xFF && this.ReadByte() == 0x00;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public bool WriteModeIO(TypeIO io1 = TypeIO.DIGITAL_INPUT, TypeIO io2 = TypeIO.DIGITAL_INPUT, 
            TypeIO io3 = TypeIO.DIGITAL_INPUT, TypeIO io4 = TypeIO.DIGITAL_INPUT)
        {
            byte val = (byte)((byte)io1 | ((byte)io2 << 2) | ((byte)io3 << 4) | ((byte)io4 << 6));
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixISS.USB_ISS, CommandISS.ISS_MODE, Mode.IO, val);
                return this.ReadByte() == 0xFF && this.ReadByte() == 0x00;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public bool WriteModeSerial(int baudRate=9600, TypeIO io3 = TypeIO.DIGITAL_INPUT, TypeIO io4 = TypeIO.DIGITAL_INPUT)
        {
            byte val = (byte)(((byte)io3 << 4) | ((byte)io4 << 6));
            baudRate = (48000000 / (16 + baudRate)) - 1;
            if (baudRate > short.MaxValue)
            {
                return false;
            }
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixISS.USB_ISS, CommandISS.ISS_MODE, Mode.SERIAL, (byte)((baudRate >> 8) & 0xff), (byte)(baudRate & 0xFF), val);
                return this.ReadByte() == 0xFF && this.ReadByte() == 0x00;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public bool WriteModeI2C(ModeI2C i2c=ModeI2C.I2C_S_100KHZ, TypeIO io1 = TypeIO.DIGITAL_INPUT, TypeIO io2 = TypeIO.DIGITAL_INPUT)
        {
            byte val = (byte)(((byte)io1) | ((byte)io2 << 2));
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixISS.USB_ISS, CommandISS.ISS_MODE, i2c, val);
                return this.ReadByte() == 0xFF && this.ReadByte() == 0x00;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public bool WriteModeSerialI2C(int baudRate=9600, ModeI2C i2c=ModeI2C.I2C_H_100KHZ)
        {
            baudRate = (48000000 / (16 + baudRate)) - 1;
            if (baudRate > short.MaxValue)
            {
                return false;
            }
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixISS.USB_ISS, CommandISS.ISS_MODE, (byte)i2c|(byte)Mode.SERIAL, (byte)((baudRate >> 8) & 0xff), (byte)(baudRate & 0xFF));
                return this.ReadByte() == 0xFF && this.ReadByte() == 0x00;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }

        public bool WriteModeSPI(ModeSPI spi=ModeSPI.SPI_MODE_TX_ACTIVE_TO_IDLE_LOW,int sckFrequency=500000)
        {
            sckFrequency = (6000000 / sckFrequency) - 1;
            try
            {
                this.DiscardInBuffer();
                this.Write(CommandPrefixISS.USB_ISS, CommandISS.ISS_MODE, spi,(byte)sckFrequency);
                return this.ReadByte() == 0xFF && this.ReadByte() == 0x00;
            }
            catch (TimeoutException)
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                return false;
            }
        }
    }

    public class Version
    {
        public readonly byte ID;
        public readonly byte FirmwareVersion;
        public readonly Mode OperatingMode;

        internal Version(byte[] bytes)
        {
            ID = bytes[0];
            FirmwareVersion = bytes[1];
            OperatingMode = (Mode)bytes[2];
        }

        override
        public string ToString()
        {
            return BitConverter.ToString(new byte[] {ID,FirmwareVersion});
        }
    }

    public enum CommandPrefixISS : byte
    {
        USB_ISS = 0x5A,         // 
    };

    public enum CommandISS : byte
    {
        ISS_VER = 0x01,         // returns version num, 1 byte
        ISS_MODE = 0x02,        // returns ACK, NACK, 1 byte
        GET_SER_NUM = 0x03,
    }

    public enum Mode : byte
    {
        IO = 0x00,             //4x GPIO
        SERIAL = 0x01,              //Rx Tx 2xGPIO
        IO_CHANGE = 0x10,           // between AI, DI, DO
        I2C_S_20KHZ = 0x20,         // Software I2C (bit-bashed) modes //ALL I2C SDA SCL 2xGPIO
        I2C_S_50KHZ = 0x30,
        I2C_S_100KHZ = 0x40,
        I2C_S_400KHZ = 0x50,
        I2C_H_100KHZ = 0x60,        // Hardware I2C peripheral modes
        I2C_H_400KHZ = 0x70,
        I2C_H_1000KHZ = 0x80,
        SPI_MODE_TX_ACTIVE_TO_IDLE_LOW = 0x90,
        SPI_MODE_TX_ACTIVE_TO_IDLE_HIGH = 0x91,
        SPI_MODE_TX_IDLE_LOW_TO_ACTIVE = 0x92,
        SPI_MODE_TX_IDLE_HIGH_TO_ACTIVE = 0x93,
        SERIAL_I2C_S_20KHZ = 0x21,         // Software I2C (bit-bashed) modes //ALL I2C SDA SCL Tx Rx
        SERIAL_I2C_S_50KHZ = 0x31,
        SERIAL_I2C_S_100KHZ = 0x41,
        SERIAL_I2C_S_400KHZ = 0x51,
        SERIAL_I2C_H_100KHZ = 0x61,        // Hardware I2C peripheral modes
        SERIAL_I2C_H_400KHZ = 0x71,
        SERIAL_I2C_H_1000KHZ = 0x81,
    };

    public enum ModeI2C : byte
    {
        I2C_S_20KHZ = Mode.I2C_S_20KHZ,      // Software I2C (bit-bashed) modes //ALL I2C SDA SCL 2xGPIO
        I2C_S_50KHZ = Mode.I2C_S_50KHZ,
        I2C_S_100KHZ = Mode.I2C_S_100KHZ,
        I2C_H_100KHZ = Mode.I2C_H_100KHZ,        // Hardware I2C peripheral modes
        I2C_S_400KHZ = Mode.I2C_S_400KHZ,
        I2C_H_400KHZ = Mode.I2C_H_400KHZ,
        I2C_H_1000KHZ = Mode.I2C_H_1000KHZ,
    }

    public enum ModeSPI : byte
    {
        SPI_MODE_TX_ACTIVE_TO_IDLE_LOW = 0x90,
        SPI_MODE_TX_ACTIVE_TO_IDLE_HIGH = 0x91,
        SPI_MODE_TX_IDLE_LOW_TO_ACTIVE = 0x92,
        SPI_MODE_TX_IDLE_HIGH_TO_ACTIVE = 0x93,
    }

    public enum TypeIO : byte
    {
        OUTPUT_LOW=0x00,
        OUTPUT_HIGH=0x01,
        DIGITAL_INPUT=0x02,
        ANALOG_INPUT=0x03,
    }
}
