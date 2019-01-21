using NeithDevices.iss;

namespace NeithDevices.i2c
{
    public class DeviceAD5245
    {
        //terminal resistance 50-120
        //nominal resistance 5k,10k,50k,100k

        public readonly byte address;
        public readonly UsbISS iss;

        public DeviceAD5245(UsbISS parentISS, byte address7bit)
        {
            address = (byte)(address7bit << 1);
            iss = parentISS;
        }

        public byte ReadWiper()
        {
            return iss.ReadOneI2C(address).Value;
        }

        public bool SetWiper(byte wiper, bool resetToMid = false, bool shutdown = false)
        {
            byte instruction =(byte)( (resetToMid ? 0b01000000 : 0) | (shutdown?0b00100000:0));
            return iss.WriteI2C(address, instruction, wiper);
        }

        public static double ComputeResistanceWA(byte wiper, double nominalResistance, double terminalResistance = 50)
        {
            return wiper * nominalResistance / 256D + 2D * terminalResistance;
        }

        public static double ComputeResistanceWB(byte wiper, double nominalResistance, double terminalResistance = 50)
        {
            return (256-wiper) * nominalResistance / 256D + 2D * terminalResistance;
        }

        public double ComputeResistanceWA(double nominalResistance, double terminalResistance=50)
        {
            return ComputeResistanceWA(ReadWiper(),nominalResistance,terminalResistance);
        }

        public double ComputeResistanceWB(double nominalResistance, double terminalResistance=50)
        {
            return ComputeResistanceWB(ReadWiper(),nominalResistance, terminalResistance);
        }
    }
}
