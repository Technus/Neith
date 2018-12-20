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
            List<string> names = new List<string>();
            foreach (var entry in iss)
            {
                names.Add(entry.Key);
            }
            Debug.Print(string.Join(" ", names));
        }
    }
}
