using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeithDevices.serial;

namespace NeithDevices.iss
{
    public partial class UsbISS : SerialPort
    {

    }

    public enum CommandPrefixIO : byte
    {
        SETPINS = 0x63,         // [SETPINS] [pin states]
        GETPINS = 0x64,         // 

        GETAD = 0x65,           // [GETAD] [pin to convert]
    };
}
