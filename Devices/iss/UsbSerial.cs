using System.IO.Ports;

namespace NeithDevices.iss
{
    public partial class UsbISS : SerialPort
    {

    }

    public enum CommandPrefixSerial : byte
    {
        SERIAL_IO = 0x62,       // 
    };
}
