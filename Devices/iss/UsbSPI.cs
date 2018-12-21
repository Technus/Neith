using RJCP.IO.Ports;
using System.IO.Ports;

namespace NeithDevices.iss
{
    public partial class UsbISS : SerialPortStream
    {

    }

    public enum CommandPrefixSPI : byte
    {
        SPI_IO = 0x61,          // SPI I/O
    };
}
