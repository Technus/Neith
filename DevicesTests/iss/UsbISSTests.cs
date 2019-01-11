using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Text;

namespace NeithDevices.iss.Tests
{
    [TestClass()]
    public class UsbISSTests
    {
        [TestMethod()]
        public void GetAttachedISSTest()
        {
            string target = "00008283 18-1B-1F-22-24-25-2C-2D-2F-50-51-56-57-5F-72-73-75 30-36-3E-44-48-4A-58-5A-5E-A0-A2-AC-AE-BE-E4-E6-EA";
            StringBuilder s = new StringBuilder();
            bool result = true;
            int i;
            for (i = 0; i < 10; i++)
            {
                var iss = UsbISS.GetAttachedISS();
                s.Clear();
                foreach (var entry in iss)
                {
                    s.Append(entry.Key).Append(" ");
                    s.Append(BitConverter.ToString(System.Linq.Enumerable.ToArray(entry.Value.PresentValidAddresses7BitI2C()))).Append(" ");
                    s.Append(BitConverter.ToString(System.Linq.Enumerable.ToArray(entry.Value.PresentValidAddresses8BitI2C())));
                }
                if (target.Equals(s.ToString()))
                {
                    Debug.Print("succeded " + i + ",");
                }
                else
                {
                    Debug.Print("fail " + s.ToString());
                    result = false;
                }
            }
            Assert.IsTrue(result,"succeded "+i);
        }
    }
}