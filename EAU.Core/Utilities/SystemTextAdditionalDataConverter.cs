using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;

namespace EAU.Utilities
{
    public class AdditionalData : Dictionary<string, string> { }

    public class SystemTextAdditionalDataConverter : System.Text.Json.Serialization.JsonConverter<AdditionalData>
    {
        public override AdditionalData Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            if (reader.TokenType != System.Text.Json.JsonTokenType.StartObject)
            {
                throw new System.Text.Json.JsonException();
            }

            AdditionalData value = new AdditionalData();

            var _valueConverter = (System.Text.Json.Serialization.JsonConverter<string>)options
                   .GetConverter(typeof(string));

            while (reader.Read())
            {
                if (reader.TokenType == System.Text.Json.JsonTokenType.EndObject)
                {
                    return value;
                }

                // Get the key.
                if (reader.TokenType != System.Text.Json.JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string key = reader.GetString();

                // Get the value.
                string v = null;

                reader.Read();

                if (reader.TokenType == System.Text.Json.JsonTokenType.Number)
                {
                    v = reader.GetInt64().ToString();
                }
                if (reader.TokenType == System.Text.Json.JsonTokenType.String)
                {
                    v = reader.GetString();
                }
                if (reader.TokenType == System.Text.Json.JsonTokenType.True)
                {
                    v = true.ToString();
                }
                if (reader.TokenType == System.Text.Json.JsonTokenType.False)
                {
                    v = false.ToString();
                }

                // Add to dictionary.
                value.Add(key, v);
            }

            return null;
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, AdditionalData value, System.Text.Json.JsonSerializerOptions options)
        {
            var _valueConverter = (System.Text.Json.Serialization.JsonConverter<string>)options
                   .GetConverter(typeof(string));

            writer.WriteStartObject();

            foreach (KeyValuePair<string, string> kvp in value)
            {
                writer.WritePropertyName(kvp.Key.ToString());

                if (_valueConverter != null)
                {
                    _valueConverter.Write(writer, kvp.Value, options);
                }
                else
                {
                    System.Text.Json.JsonSerializer.Serialize(writer, kvp.Value, options);
                }
            }

            writer.WriteEndObject();
        }
    }

    public class AdditionalDataDapperMapHandler : SqlMapper.TypeHandler<AdditionalData>
    {
        public override void SetValue(IDbDataParameter parameter, AdditionalData value)
        {
            parameter.DbType = DbType.String;

            if (value == null)
            {
                parameter.Value = (object)DBNull.Value;
            }
            else
            {
                string json = EAUJsonSerializer.Serialize<AdditionalData>(value);
                parameter.Value = json;
            }
        }

        public override AdditionalData Parse(object value)
        {
            if (value == null)
            {
                return null;
            }

            string json = value.ToString();
            AdditionalData result = EAUJsonSerializer.Deserialize<AdditionalData>(json);

            return result;
        }
    }
}
