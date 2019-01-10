using System.Text;
using HidLibrary;

namespace NeithDevices.hid
{
    public static class HidDeviceExtension
    {
        public static string GetVID(this HidDevice device)
        {
            int pos = device.DevicePath.IndexOf("vid_");
            if (pos > 0)
            {
                return device.DevicePath.Substring(pos + 4, 4);
            }
            return null;
        }

        public static string GetPID(this HidDevice device)
        {
            int pos = device.DevicePath.IndexOf("pid_");
            if (pos > 0)
            {
                return device.DevicePath.Substring(pos + 4, 4);
            }
            return null;
        }

        public static string GetUUID(this HidDevice device)
        {
            int start = device.DevicePath.IndexOf("{");
            int stop = device.DevicePath.IndexOf("}");
            if (start > 0 && stop > start)
            {
                return device.DevicePath.Substring(start++, stop-start);
            }
            return null;
        }

        public static string GetProduct(this HidDevice device)
        {
            device.ReadProduct(out byte[] product);
            return Encoding.Unicode.GetString(product);
        }

        public static string GetManufacturer(this HidDevice device)
        {
            device.ReadManufacturer(out byte[] manufacturer);
            return Encoding.Unicode.GetString(manufacturer);
        }
    }
}
