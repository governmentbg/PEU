using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace EAU.Utilities
{
    public class EАJsonEnumConverter<TEnum> : System.Text.Json.Serialization.JsonConverter<TEnum> where TEnum : struct, Enum
    {
        private static readonly TypeCode s_enumTypeCode = Type.GetTypeCode(typeof(TEnum));

        public EАJsonEnumConverter()
        {
        }

        public override bool CanConvert(Type type)
        {
            return type.IsEnum;
        }

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonTokenType token = reader.TokenType;

            if (token == JsonTokenType.String)
            {
                string enumString = reader.GetString();

                if (!Enum.TryParse(enumString, out TEnum value)
                    && !Enum.TryParse(enumString, ignoreCase: true, out value))
                {

                    return default;
                }

                return value;
            }
            else if (token == JsonTokenType.Number)
            {
                switch (s_enumTypeCode)
                {
                    // Switch cases ordered by expected frequency

                    case TypeCode.Int32:
                        if (reader.TryGetInt32(out int int32))
                        {
                            return Unsafe.As<int, TEnum>(ref int32);
                        }
                        break;
                    case TypeCode.UInt32:
                        if (reader.TryGetUInt32(out uint uint32))
                        {
                            return Unsafe.As<uint, TEnum>(ref uint32);
                        }
                        break;
                    case TypeCode.UInt64:
                        if (reader.TryGetUInt64(out ulong uint64))
                        {
                            return Unsafe.As<ulong, TEnum>(ref uint64);
                        }
                        break;
                    case TypeCode.Int64:
                        if (reader.TryGetInt64(out long int64))
                        {
                            return Unsafe.As<long, TEnum>(ref int64);
                        }
                        break;

                    // When utf8reader/writer will support all primitive types we should remove custom bound checks
                    // https://github.com/dotnet/corefx/issues/36125
                    case TypeCode.SByte:
                        if (reader.TryGetInt32(out int byte8) /*&& JsonHelpers.IsInRangeInclusive(byte8, sbyte.MinValue, sbyte.MaxValue)*/)
                        {
                            sbyte byte8Value = (sbyte)byte8;
                            return Unsafe.As<sbyte, TEnum>(ref byte8Value);
                        }
                        break;
                    case TypeCode.Byte:
                        if (reader.TryGetUInt32(out uint ubyte8) /*&& JsonHelpers.IsInRangeInclusive(ubyte8, byte.MinValue, byte.MaxValue)*/)
                        {
                            byte ubyte8Value = (byte)ubyte8;
                            return Unsafe.As<byte, TEnum>(ref ubyte8Value);
                        }
                        break;
                    case TypeCode.Int16:
                        if (reader.TryGetInt32(out int int16) /*&& JsonHelpers.IsInRangeInclusive(int16, short.MinValue, short.MaxValue)*/)
                        {
                            short shortValue = (short)int16;
                            return Unsafe.As<short, TEnum>(ref shortValue);
                        }
                        break;
                    case TypeCode.UInt16:
                        if (reader.TryGetUInt32(out uint uint16) /*&& JsonHelpers.IsInRangeInclusive(uint16, ushort.MinValue, ushort.MaxValue)*/)
                        {
                            ushort ushortValue = (ushort)uint16;
                            return Unsafe.As<ushort, TEnum>(ref ushortValue);
                        }
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }

            return default(TEnum);
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            switch (s_enumTypeCode)
            {
                case TypeCode.Int32:
                    writer.WriteNumberValue(Unsafe.As<TEnum, int>(ref value));
                    break;
                case TypeCode.UInt32:
                    writer.WriteNumberValue(Unsafe.As<TEnum, uint>(ref value));
                    break;
                case TypeCode.UInt64:
                    writer.WriteNumberValue(Unsafe.As<TEnum, ulong>(ref value));
                    break;
                case TypeCode.Int64:
                    writer.WriteNumberValue(Unsafe.As<TEnum, long>(ref value));
                    break;
                case TypeCode.Int16:
                    writer.WriteNumberValue(Unsafe.As<TEnum, short>(ref value));
                    break;
                case TypeCode.UInt16:
                    writer.WriteNumberValue(Unsafe.As<TEnum, ushort>(ref value));
                    break;
                case TypeCode.Byte:
                    writer.WriteNumberValue(Unsafe.As<TEnum, byte>(ref value));
                    break;
                case TypeCode.SByte:
                    writer.WriteNumberValue(Unsafe.As<TEnum, sbyte>(ref value));
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
