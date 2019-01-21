using NeithDevices.iss;
using System;
using System.Collections.Generic;
using NeithCore.utility;

namespace NeithDevices.i2c
{
    /**
     * Should throw OperationFailedI2C if operation fails!
     * **/
    public interface IBusI2C
    {
        bool TestPresenceI2C(IAddressI2C address);

        /**
         * A timeout version of test presence waiting for true result with spinwait...
         * **/
        void AwaitReadyI2C(IAddressI2C address, uint timeout = 2000);

        HashSet<IAddressI2C> PresentValidAddressesI2C(bool check7Bit=true,bool check10Bit=true);


        //byte ReadOneI2C(IAddressI2C address);
        //
        //void WriteOneI2C(IAddressI2C address, IConvertible value);
        //
        //
        //byte[] ReadSimpleI2C(IAddressI2C address, int count);
        //
        //void ReadSimpleI2C(IAddressI2C address, byte[] bytes);
        //
        //void WriteSimpleI2C(IAddressI2C address, params IConvertible[] value);
        //
        //
        //byte[] ReadAddressedI2C(IAddressI2C address, IConvertible internalAddressByte, int count);
        //
        //void ReadAddressedI2C(IAddressI2C address, IConvertible internalAddressByte, byte[] bytes);
        //
        //void WriteAddressedI2C(IAddressI2C address, IConvertible internalAddressByte, params IConvertible[] value);
        //
        //
        //byte[] ReadShortAddressedI2C(IAddressI2C address, IConvertible internalAddressShort, int count);
        //
        //void ReadShortAddressedI2C(IAddressI2C address, IConvertible internalAddressShort, byte[] bytes);
        //
        //void WriteShortAddressedI2C(IAddressI2C address, IConvertible internalAddressShort, params IConvertible[] value);
        //
        //
        //byte[] ReadShortAddressedI2C(IAddressI2C address, IConvertible internalAddressHigh, IConvertible internalAddressLow, int count);
        //
        //void ReadShortAddressedI2C(IAddressI2C address, IConvertible internalAddressHigh, IConvertible internalAddressLow, byte[] bytes);
        //
        //void WriteShortAddressedI2C(IAddressI2C address, IConvertible internalAddressHigh, IConvertible internalAddressLow, params IConvertible[] value);

        void SendCustomPacketI2C(PacketI2C packet);
    }

    public class OperationFailedI2C:Exception
    {
        public OperationFailedI2C() : base() { }
        public OperationFailedI2C(string message):base(message) { }
        public OperationFailedI2C(string message, Exception innerException):base(message,innerException) { }
    }

    public class OperationNoDeviceACK : OperationFailedI2C
    {
        public OperationNoDeviceACK() : base() { }
        public OperationNoDeviceACK(string message) : base(message) { }
        public OperationNoDeviceACK(string message, Exception innerException) : base(message, innerException) { }
    }

    public interface IDeviceI2C
    {
        void Initialize(IBusI2C bus,IAddressI2C address);
        IBusI2C GetBus();
        IAddressI2C GetAddress();
    }

    public class PacketI2C:List<ActionI2C>
    {
        public PacketI2C(params ActionI2C[] actions)
        {
            AddRange(actions);
        }

        public byte[] GetReadBytes()
        {
            var list = new List<byte>();
            foreach (var item in this)
            {
                if (item.IsReadOperation() && item.Data != null)
                {
                    foreach (var convertible in item.Data)
                    {
                        list.Add((byte)convertible);
                    }
                }
            }
            return list.ToArray();
        }

        public PacketI2C Append(params ActionI2C[] actions)
        {
            AddRange(actions);
            return this; 
        }
    }

    public class ActionI2C
    {
        public static readonly ActionI2C Start = new ActionI2C { Type = ActionTypeI2C.Start };
        public static readonly ActionI2C Restart = new ActionI2C { Type = ActionTypeI2C.Stop };
        public static readonly ActionI2C Stop = new ActionI2C { Type = ActionTypeI2C.Restart };
        public static ActionI2C SendAddress(IAddressI2C address,bool read,bool format10BitAddress=true)
        {
            if (address.Is7Bit())
            {
                return new ActionI2C { Type = ActionTypeI2C.SendAddress, Data = address.ToAddress7BitI2C().GetAddress8Bit(read).AsIConvertibleArray() };
            }
            else
            {
                return new ActionI2C { Type = ActionTypeI2C.SendAddress, Data = address.ToAddress10BitI2C().GetAddress16BitBytes(read, format10BitAddress).AsIConvertibleArray() };
            }
        }
        public static ActionI2C Write(params IConvertible[] data)
        {
            return new ActionI2C { Type = ActionTypeI2C.Write,Data=data };
        }
        public static ActionI2C Read(IConvertible[] data)
        {
            return new ActionI2C { Type = ActionTypeI2C.Read, Data = data };
        }
        public static ActionI2C ReadAndNackLast(IConvertible[] data)
        {
            return new ActionI2C { Type = ActionTypeI2C.ReadAndNackLast, Data = data };
        }
        //public static ActionI2C WriteCount(int count)
        //{
        //    return new ActionI2C { Type = ActionTypeI2C.Write, Data = new IConvertible[count] };
        //}
        public static ActionI2C Read(int count)
        {
            return new ActionI2C { Type = ActionTypeI2C.Read, Data = new IConvertible[count] };
        }
        public static ActionI2C ReadAndNackLast(int count)
        {
            return new ActionI2C { Type = ActionTypeI2C.ReadAndNackLast, Data = new IConvertible[count] };
        }

        public ActionTypeI2C Type { get; private set; } = ActionTypeI2C.UNDEFINED;
        public IConvertible[] Data { get; set; } = null;
        public IConvertible this[int key]
        {
            get
            {
                return Data[key];
            }
            set
            {
                Data[key] = value;
            }
        }

        public bool IsReadOperation()
        {
            return (byte)Type >= (byte)ActionTypeI2C.Read;
        }
    }

    public enum ActionTypeI2C : byte
    {
        UNDEFINED=0x00,Start=0x01, Restart=0x02, Stop=0x03, Write = 0x10, SendAddress = 0x11, Read=0x90, ReadAndNackLast=0x91
    }

    public interface IAddressI2C: IComparable<IAddressI2C>, IEquatable<IAddressI2C>
    {
        bool Is7Bit();
        bool Is10Bit();
        bool IsValid();
    }

    public struct Address7BitI2C :IAddressI2C, IComparable, IFormattable, IConvertible, IComparable<Address7BitI2C>, IEquatable<Address7BitI2C>, IComparable<byte>, IEquatable<byte>
    {
        public static readonly byte MIN_ADDRESS_VALUE = 0x08;
        public static readonly byte MAX_ADDRESS_VALUE = 0x77;
        public static readonly Address7BitI2C MIN_ADDRESS = new Address7BitI2C(MIN_ADDRESS_VALUE);
        public static readonly Address7BitI2C MAX_ADDRESS = new Address7BitI2C(MAX_ADDRESS_VALUE);

        private readonly byte address7Bit;

        public Address7BitI2C(IConvertible address, bool formatIs7Bit = true)
        {
            if (formatIs7Bit)
            {
                address7Bit = (byte)(address.ToByte()&0x7F);
            }
            else
            {
                address7Bit = (byte)(address.ToByte() >> 1);
            }
        }

        public bool Is7Bit()
        {
            return true;
        }

        public bool Is10Bit()
        {
            return false;
        }

        public bool IsValid()
        {
            return address7Bit >= 0x08 && address7Bit <= 0x77;
        }

        public byte GetAddress8Bit(bool read)
        {
            return (byte)(read ? (address7Bit << 1) | 0b1 : (address7Bit << 1));
        }

        public int CompareTo(object obj)
        {
            return address7Bit.CompareTo(obj);
        }

        public int CompareTo(Address7BitI2C other)
        {
            return address7Bit.CompareTo(other);
        }

        public int CompareTo(byte other)
        {
            return address7Bit.CompareTo(other);
        }

        public bool Equals(Address7BitI2C other)
        {
            return address7Bit.Equals(other);
        }

        public bool Equals(byte other)
        {
            return address7Bit.Equals(other);
        }

        public TypeCode GetTypeCode()
        {
            return address7Bit.GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToBoolean(provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToByte(provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToChar(provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToDateTime(provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToDecimal(provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToDouble(provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToInt16(provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToInt32(provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToInt64(provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToSByte(provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToSingle(provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return address7Bit.ToString(provider);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return address7Bit.ToString(format, formatProvider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToType(conversionType, provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToUInt16(provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToUInt32(provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return ((IConvertible)address7Bit).ToUInt64(provider);
        }

        public int CompareTo(IAddressI2C other)
        {
            return other.Is7Bit() ? address7Bit.CompareTo(other.ToAddress7BitI2C().address7Bit) : -1;
        }

        public bool Equals(IAddressI2C other)
        {
            return other.Is7Bit() ? address7Bit == other.ToAddress7BitI2C().address7Bit : false;
        }
    }

    public struct Address10BitI2C : IAddressI2C, IComparable, IFormattable, IConvertible, IComparable<Address7BitI2C>, IEquatable<Address7BitI2C>, IComparable<byte>, IEquatable<byte>
    {
        public static readonly ushort MIN_ADDRESS_VALUE = 0x000;
        public static readonly ushort MAX_ADDRESS_VALUE = 0x3FF;
        public static readonly Address10BitI2C MIN_ADDRESS = new Address10BitI2C(MIN_ADDRESS_VALUE);
        public static readonly Address10BitI2C MAX_ADDRESS = new Address10BitI2C(MAX_ADDRESS_VALUE);

        private readonly ushort address10Bit;

        public Address10BitI2C(IConvertible address, bool formatIs10Bit = true)
        {
            if (formatIs10Bit)
            {
                address10Bit = (ushort)(address.ToUInt16()&0x3FF);
            }
            else
            {
                ushort addr = address.ToUInt16();
                address10Bit = (ushort)(((addr >> 1) & 0x300) | (addr & 0xFF));
            }
        }

        public Address10BitI2C(IConvertible[] addressBytes, bool formatIs10Bit = true)
        {
            if (formatIs10Bit)
            {
                address10Bit = (ushort)(((addressBytes[1].ToByte() & 0b011) << 8) | (addressBytes[0].ToByte()));
            }
            else
            {
                address10Bit = (ushort)(((addressBytes[1].ToByte() & 0b110) << 7) | (addressBytes[0].ToByte()));
            }
        }

        public Address10BitI2C(byte[] addressBytes, bool formatIs10Bit = true)
        {
            if (formatIs10Bit)
            {
                address10Bit = (ushort)(((addressBytes[1] & 0x3) << 8) | (addressBytes[0]));
            }
            else
            {
                address10Bit = (ushort)(((addressBytes[1] & 0b110) << 7) | (addressBytes[0]));
            }
        }

        public bool Is7Bit()
        {
            return false;
        }

        public bool Is10Bit()
        {
            return true;
        }

        public bool IsValid()
        {
            return address10Bit <= 0b1111111111;
        }

        public byte[] GetAddress10BitBytes()
        {
            return new byte[] { (byte)(((address10Bit&0x300)>>8)&0xff),(byte)(address10Bit&0xff)};
        }

        public ushort GetAddress16Bit(bool read,bool formatted=true)
        {
            ushort temp= read ? (ushort)(((address10Bit & 0x300) << 1) | (address10Bit & 0xff) | 0x100)
                              : (ushort)(((address10Bit & 0x300) << 1) | (address10Bit & 0xff));
            return formatted ?(ushort)(temp|0xF000&0xF7FF):temp;
        }

        public byte[] GetAddress16BitBytes(bool read, bool formatted = true)
        {
            byte[] temp= read ? new byte[] { (byte)((((address10Bit & 0x300) >> 7) & 0xff)|0x1), (byte)(address10Bit & 0xff) }
                              : new byte[] { (byte)(((address10Bit & 0x300) >> 7) & 0xff), (byte)(address10Bit & 0xff) }; ;//shift bits 9 and 8
            if (formatted)
            {
                temp[0] = (byte)(temp[0] | 0xF0 & 0xF7);
            }
            return temp;
        }

        public int CompareTo(object obj)
        {
            return address10Bit.CompareTo(obj);
        }

        public int CompareTo(Address7BitI2C other)
        {
            return address10Bit.CompareTo(other);
        }

        public int CompareTo(byte other)
        {
            return address10Bit.CompareTo(other);
        }

        public bool Equals(Address7BitI2C other)
        {
            return address10Bit.Equals(other);
        }

        public bool Equals(byte other)
        {
            return address10Bit.Equals(other);
        }

        public TypeCode GetTypeCode()
        {
            return address10Bit.GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToBoolean(provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToByte(provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToChar(provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToDateTime(provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToDecimal(provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToDouble(provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToInt16(provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToInt32(provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToInt64(provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToSByte(provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToSingle(provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return address10Bit.ToString(provider);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return address10Bit.ToString(format, formatProvider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToType(conversionType, provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToUInt16(provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToUInt32(provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return ((IConvertible)address10Bit).ToUInt64(provider);
        }

        public int CompareTo(IAddressI2C other)
        {
            return other.Is10Bit() ? address10Bit.CompareTo(other.ToAddress10BitI2C().address10Bit) : -1;
        }

        public bool Equals(IAddressI2C other)
        {
            return other.Is10Bit() ? address10Bit == other.ToAddress10BitI2C().address10Bit : false;
        }
    }

    public static partial class Extensions
    {
        public static Address7BitI2C ToAddress7BitI2C(this IAddressI2C data, bool formatIs7Bit = true)
        {
            if (data is Address7BitI2C)
            {
                return (Address7BitI2C)data;
            }
            throw new InvalidCastException("Cannot convert IAddressI2C, invalid size!");
        }

        public static Address10BitI2C ToAddress10BitI2C(this IAddressI2C data, bool formatIs10Bit = true)
        {
            if (data is Address10BitI2C)
            {
                return (Address10BitI2C)data;
            }
            throw new InvalidCastException("Cannot convert IAddressI2C, invalid size!");
        }

        public static Address7BitI2C ToAddress7BitI2C(this IConvertible data, bool formatIs7Bit = true)
        {
            if (data is Address7BitI2C)
            {
                return (Address7BitI2C)data;
            }
            return new Address7BitI2C(data, formatIs7Bit);
        }

        public static Address10BitI2C ToAddress10BitI2C(this IConvertible data, bool formatIs10Bit = true)
        {
            if (data is Address10BitI2C)
            {
                return (Address10BitI2C)data;
            }
            return new Address10BitI2C(data, formatIs10Bit);
        }
    }
}
