using NeithDevices.iss;
using System.Collections.Generic;
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
                Debug.Print(string.Join(" ", entry.Key));
                Debug.Print(string.Join(" ", entry.Value.PresentAddressesI2C()));
            }
        }
    }
}
