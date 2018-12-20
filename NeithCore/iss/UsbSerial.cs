using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neith.serial;

namespace NeithCore.iss
{
    partial class UsbISS : SerialPort
    {

    }

    enum CommandPrefixSerial : byte
    {
        SERIAL_IO = 0x62,       // 
    };
}
