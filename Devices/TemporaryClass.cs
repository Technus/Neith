using HidLibrary;
using NeithDevices.hid;
using NeithDevices.iss;
using System;
using System.Diagnostics;

namespace NeithDevices
{
    public static class TemporaryClass
    {
        public static void Run()
        {
            Debug.Print(string.Join(" ", OpenLayers.Base.DeviceMgr.Get().GetDeviceNames()));

            var iss = UsbISS.GetAttachedISS();
            foreach (var entry in iss)
            {
                Debug.Print(entry.Key);
                Debug.Print(BitConverter.ToString(System.Linq.Enumerable.ToArray(entry.Value.PresentValidAddresses7BitI2C())));
                Debug.Print(BitConverter.ToString(System.Linq.Enumerable.ToArray(entry.Value.PresentValidAddresses8BitI2C())));
            }

            foreach (var hidDevice in HidDevices.Enumerate())
            {
                Debug.Print(hidDevice.Description +" "+hidDevice.GetProduct()+" "+hidDevice.GetManufacturer()+ " ");
                Debug.Print(hidDevice.GetVID() + " " + hidDevice.GetPID()+" "+hidDevice.GetUUID()+ " "+hidDevice.DevicePath);
            }
        }
    }
}
