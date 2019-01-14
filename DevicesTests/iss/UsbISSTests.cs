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
            Trace.WriteLine("START");
            string target = "00008283 18-1B-1F-22-24-25-2C-2D-2F-50-51-56-57-5F-72-73-75 30-36-3E-44-48-4A-58-5A-5E-A0-A2-AC-AE-BE-E4-E6-EA";
            StringBuilder s = new StringBuilder();
            bool result = true;
            int i,j=0;
            for (i = 0; i < 10; i++)
            {
                Trace.WriteLine("attempt " + i + ",");
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
                    j++;
                    Trace.WriteLine("succeded " + i + ",");
                }
                else
                {
                    Trace.WriteLine("fail " + i + " " + s.ToString());
                    result = false;
                }
            }
            Assert.IsTrue(result, "succeded " + j + "/" + i + ",");
        }
    }
}