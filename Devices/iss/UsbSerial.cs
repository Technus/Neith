using NeithDevices.serial;

namespace NeithDevices.iss
{
    public partial class UsbISS : CSSSC
    {

    }

    public enum CommandPrefixSerial : byte
    {
        SERIAL_IO = 0x62,       // 
    };
}
