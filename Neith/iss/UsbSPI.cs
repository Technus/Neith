using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neith.serial;

namespace Neith.iss
{
    partial class UsbISS : SerialPort
    {

    }

    enum CommandPrefixSPI : byte
    {
        SPI_IO = 0x61,          // SPI I/O
    };
}
