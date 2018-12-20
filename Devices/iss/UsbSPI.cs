using System.IO.Ports;

namespace NeithDevices.iss
{
    public partial class UsbISS : SerialPort
    {

    }

    public enum CommandPrefixSPI : byte
    {
        SPI_IO = 0x61,          // SPI I/O
    };
}
