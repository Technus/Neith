using NeithDevices.iss;
using System;
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
                Debug.Print(BitConverter.ToString(System.Linq.Enumerable.ToArray(entry.Value.PresentValidAddresses7BitI2C())));
                Debug.Print(BitConverter.ToString(System.Linq.Enumerable.ToArray(entry.Value.PresentValidAddresses8BitI2C())));
            }
        }
    }
}
