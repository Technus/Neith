
using NeithDevices.spi;

namespace NeithDevices.iss
{
    public partial class UsbISS : CSSSC.CSSSC,IBusSPI
    {

    }

    public enum CommandPrefixSPI : byte
    {
        SPI_IO = 0x61,          // SPI I/O
    };
}
