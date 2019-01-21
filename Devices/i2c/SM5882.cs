using NeithDevices.iss;
using System;
using System.Threading;

namespace NeithDevices.i2c
{
    public class DeviceSM5882
    {
        public readonly byte address;
        public readonly UsbISS iss;

        public double TemperatureOffset = 233.0597014925370000D;
        public double TemperatureCoefficient = -0.0895522388059701D;
        public double PressureOffset = 1.875457875D;
        public double PressureCoefficient = 0.000915750915750916;


        public DeviceSM5882(UsbISS parentISS, byte address7bit)
        {
            address = (byte)(address7bit << 1);
            iss = parentISS;
        }

        public ushort ReadPressure()
        {
            return (ushort)((iss.ReadI2C(address, 0x80, 1)[0] & 0b00111111) | ((iss.ReadI2C(address, 0x81, 1)[0] & 0b00111111) << 6));
        }

        public ushort ReadTemperature()
        {
            return (ushort)((iss.ReadI2C(address, 0x82, 1)[0] & 0b00111111) | ((iss.ReadI2C(address, 0x83, 1)[0] & 0b00111111) << 6)); 
        }

        public double Temperature()
        {
            return ReadTemperature() * TemperatureCoefficient + TemperatureOffset;
        }

        public double Pressure()
        {
            return ReadPressure() * PressureCoefficient + PressureOffset;
        }
    }
}
