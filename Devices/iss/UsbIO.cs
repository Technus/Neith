using RJCP.IO.Ports;
using System.IO.Ports;

namespace NeithDevices.iss
{
    public partial class UsbISS : SerialPortStream
    {

    }

    public enum CommandPrefixIO : byte
    {
        SETPINS = 0x63,         // [SETPINS] [pin states]
        GETPINS = 0x64,         // 

        GETAD = 0x65,           // [GETAD] [pin to convert]
    };
}
