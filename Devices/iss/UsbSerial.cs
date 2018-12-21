using RJCP.IO.Ports;
using System.IO.Ports;

namespace NeithDevices.iss
{
    public partial class UsbISS : SerialPortStream
    {

    }

    public enum CommandPrefixSerial : byte
    {
        SERIAL_IO = 0x62,       // 
    };
}
