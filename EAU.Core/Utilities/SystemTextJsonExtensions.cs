using EAU.Utilities;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text.Json
{
    /// <summary>
    /// Използва се JsonSerializer - а само че се подават опциите по подразбиране от DefaultSerializerOptions.
    /// TODO след като се изправи https://github.com/dotnet/corefx/issues/41612
    /// може да го махнем.
    /// </summary>
    public static class EAUJsonSerializer
    {
        static EAUJsonSerializer()
        {
            DefaultSerializerOptions = GetDefaultSerializerOptionsCopy();
        }

        public static JsonSerializerOptions DefaultSerializerOptions { get; set; }

        public static JsonSerializerOptions GetDefaultSerializerOptionsCopy()
        {
            var serializerOptions = new JsonSerializerOptions();
            serializerOptions.PropertyNameCaseInsensitive = true;
            serializerOptions.Converters.Add(new SystemTextIsoTimeSpanConverter());
            serializerOptions.Converters.Add(new EAUJsonEnumConverterFactory(JsonNamingPolicy.CamelCase));
            serializerOptions.Converters.Add(new SystemTextAdditionalDataConverter());
            serializerOptions.IgnoreNullValues = true;
            serializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            serializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            
            return serializerOptions;
        }

        //
        // Summary:
        //     Parses the text representing a single JSON value into an instance of the type
        //     specified by a generic type parameter.
        //
        // Parameters:
        //   json:
        //     The JSON text to parse.
        //
        //   options:
        //     Options to control the behavior during parsing.
        //
        // Type parameters:
        //   TValue:
        //     The target type of the JSON value.
        //
        // Returns:
        //     A TValue representation of the JSON value.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     json is null.
        //
        //   T:System.Text.Json.JsonException:
        //     The JSON is invalid. -or- TValue is not compatible with the JSON. -or- There
        //     is remaining data in the string beyond a single JSON value.
        public static TValue Deserialize<TValue>(string json, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Deserialize<TValue>(json, options ?? DefaultSerializerOptions);
        }

        public static TValue Deserialize<TValue>(JsonElement jElement, JsonSerializerOptions options = null)
        {
            return Deserialize<TValue>(jElement.ToString(), options);
        }

        //
        // Summary:
        //     Asynchronously reads the UTF-8 encoded text representing a single JSON value
        //     into an instance of a type specified by a generic type parameter. The stream
        //     will be read to completion.
        //
        // Parameters:
        //   utf8Json:
        //     The JSON data to parse.
        //
        //   options:
        //     Options to control the behavior during reading.
        //
        //   cancellationToken:
        //     A token that may be used to cancel the read operation.
        //
        // Type parameters:
        //   TValue:
        //     The target type of the JSON value.
        //
        // Returns:
        //     A TValue representation of the JSON value.
        //
        // Exceptions:
        //   T:System.Text.Json.JsonException:
        //     The JSON is invalid. -or- TValue is not compatible with the JSON. -or- There
        //     is remaining data in the stream.
        public static ValueTask<TValue> DeserializeAsync<TValue>(Stream utf8Json, JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            return JsonSerializer.DeserializeAsync<TValue>(utf8Json, options ?? DefaultSerializerOptions, cancellationToken);
        }

        //
        // Summary:
        //     Reads one JSON value (including objects or arrays) from the provided reader and
        //     converts it into an instance of a specified type.
        //
        // Parameters:
        //   reader:
        //     The reader to read the JSON from.
        //
        //   returnType:
        //     The type of the object to convert to and return.
        //
        //   options:
        //     Options to control the serializer behavior during reading.
        //
        // Returns:
        //     A returnType representation of the JSON value.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     returnType is null.
        //
        //   T:System.Text.Json.JsonException:
        //     The JSON is invalid. -or- returnType is not compatible with the JSON. -or- A
        //     value could not be read from the reader.
        //
        //   T:System.ArgumentException:
        //     reader is using unsupported options.
        public static TValue Deserialize<TValue>(ref Utf8JsonReader reader, JsonSerializerOptions options = null)
        {
            return (TValue)JsonSerializer.Deserialize(ref reader, typeof(TValue), options ?? DefaultSerializerOptions);
        }

        //
        // Summary:
        //     Converts the value of a type specified by a generic type parameter into a JSON
        //     string.
        //
        // Parameters:
        //   value:
        //     The value to convert.
        //
        //   options:
        //     Options to control serialization behavior.
        //
        // Type parameters:
        //   TValue:
        //     The type of the value to serialize.
        //
        // Returns:
        //     A JSON string representation of the value.
        public static string Serialize<TValue>(TValue value, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Serialize(value, options ?? DefaultSerializerOptions);
        }

        //
        // Summary:
        //     Writes the JSON representation of a type specified by a generic type parameter
        //     to the provided writer.
        //
        // Parameters:
        //   writer:
        //     A JSON writer to write to.
        //
        //   value:
        //     The value to convert and write.
        //
        //   options:
        //     Options to control serialization behavior.
        //
        // Type parameters:
        //   TValue:
        //     The type of the value to serialize.
        public static void Serialize<TValue>(Utf8JsonWriter writer, TValue value, JsonSerializerOptions options = null)
        {
            JsonSerializer.Serialize(writer, value, options ?? DefaultSerializerOptions);
        }

        //
        // Summary:
        //     Asynchronously converts a value of a type specified by a generic type parametaer
        //     to UTF-8 encoded JSON text and writes it to a stream.
        //
        // Parameters:
        //   utf8Json:
        //     The UTF-8 stream to write to.
        //
        //   value:
        //     The value to convert.
        //
        //   options:
        //     Options to control serialization behavior.
        //
        //   cancellationToken:
        //     A token that may be used to cancel the write operation.
        //
        // Type parameters:
        //   TValue:
        //     The type of the value to serialize.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        public static Task SerializeAsync<TValue>(Stream utf8Json, TValue value, JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            return JsonSerializer.SerializeAsync(utf8Json, value, options ?? DefaultSerializerOptions, cancellationToken);
        }

        //
        // Summary:
        //     Asynchronously converts the value of a specified type to UTF-8 encoded JSON text
        //     and writes it to the specified stream.
        //
        // Parameters:
        //   utf8Json:
        //     The UTF-8 stream to write to.
        //
        //   value:
        //     The value to convert.
        //
        //   inputType:
        //     The type of the value to convert.
        //
        //   options:
        //     Options to control serialization behavior.
        //
        //   cancellationToken:
        //     A token that may be used to cancel the write operation.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        public static Task SerializeAsync(Stream utf8Json, object value, Type inputType, JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            return JsonSerializer.SerializeAsync(utf8Json, value, inputType, options ?? DefaultSerializerOptions, cancellationToken);
        }
    }
}
