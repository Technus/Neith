using NeithDevices.serial;

namespace NeithDevices.iss
{
    public partial class UsbISS : CSSSC
    {

    }

    public enum CommandPrefixSPI : byte
    {
        SPI_IO = 0x61,          // SPI I/O
    };
}
