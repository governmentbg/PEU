using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EAU.Utilities
{
    public sealed class EAUJsonEnumConverterFactory : JsonConverterFactory
    {
        private readonly JsonNamingPolicy _namingPolicy;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="namingPolicy">
        /// Optional naming policy for writing enum values.
        /// </param>
        /// <param name="allowIntegerValues">
        /// True to allow undefined enum values. When true, if an enum value isn't
        /// defined it will output as a number rather than a string.
        /// </param>
        public EAUJsonEnumConverterFactory(JsonNamingPolicy namingPolicy = null)
        {
            _namingPolicy = namingPolicy;
        }

        /// <inheritdoc />
        public override bool CanConvert(Type typeToConvert)
        {

            return typeToConvert.IsEnum;
        }

        /// <inheritdoc />
        //[PreserveDependency(
        //    ".ctor(System.Text.Json.Serialization.Converters.EnumConverterOptions, System.Text.Json.JsonNamingPolicy)",
        //    "System.Text.Json.Serialization.Converters.JsonConverterEnum`1")]
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(EАJsonEnumConverter<>).MakeGenericType(typeToConvert),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                new object[] { },
                culture: null);

            return converter;
        }
    }
}
