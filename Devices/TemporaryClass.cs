using HidLibrary;
using NeithDevices.hid;
using NeithDevices.iss;
using System;
using System.Diagnostics;
using System.Text;

namespace NeithDevices
{
    public static class TemporaryClass
    {
        public static void Run()
        {
            Debug.Print(string.Join(" ", OpenLayers.Base.DeviceMgr.Get().GetDeviceNames()));

            var iss = UsbISS.GetAttachedISS();
            StringBuilder s = new StringBuilder();
            foreach (var entry in iss)
            {
                s.Append(entry.Key).Append(" ");
                s.Append(BitConverter.ToString(System.Linq.Enumerable.ToArray(entry.Value.PresentValidAddresses7BitI2C()))).Append(" ");
                s.Append(BitConverter.ToString(System.Linq.Enumerable.ToArray(entry.Value.PresentValidAddresses8BitI2C()))).Append("\n");
            }
            Debug.Write(s);
            foreach (var hidDevice in HidDevices.Enumerate())
            {
                Debug.Print(hidDevice.Description +" "+hidDevice.GetProduct()+" "+hidDevice.GetManufacturer());
                Debug.Print(" "+hidDevice.GetVID() + " " + hidDevice.GetPID()+" "+hidDevice.GetUUID()+ " "+hidDevice.DevicePath);
            }
        }
    }
}
