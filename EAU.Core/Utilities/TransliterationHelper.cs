using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EAU.Utilities
{
    /// <summary>
    /// Транслатор от кирилица към латиница
    /// </summary>
    public class TransliterationHelper
    {
        private static Dictionary<string, string> PatternReplacementDictionary = new Dictionary<string, string>()
        {
            //Подменяме на комбинацията "ия" само ако е накрая на думата с "ia"
            { @"(ия)([^(а-яA-Я)])|(ия)$", @"ia$2" }
        };

        private static Dictionary<string, string> AlphabetDictionary = new Dictionary<string, string>()
            {
                { "а", "a" },
                { "б", "b" },
                { "в", "v" },
                { "г", "g" },
                { "д", "d" },
                { "е", "e" },
                { "ж", "zh" },
                { "з", "z" },
                { "и", "i" },
                { "й", "y" },
                { "к", "k" },
                { "л", "l" },
                { "м", "m" },
                { "н", "n" },
                { "о", "o" },
                { "п", "p" },
                { "р", "r" },
                { "с", "s" },
                { "т", "t" },
                { "у", "u" },
                { "ф", "f" },
                { "х", "h" },
                { "ц", "ts" },
                { "ч", "ch" },
                { "ш", "sh" },
                { "щ", "sht" },
                { "ъ", "a" },
                { "ь", "y" },
                { "ю", "yu" },
                { "я", "ya" },

                { "А", "A" },
                { "Б", "B" },
                { "В", "V" },
                { "Г", "G" },
                { "Д", "D" },
                { "Е", "E" },
                { "Ж", "Zh" },
                { "З", "Z" },
                { "И", "I" },
                { "Й", "Y" },
                { "К", "K" },
                { "Л", "L" },
                { "М", "M" },
                { "Н", "N" },
                { "О", "O" },
                { "П", "P" },
                { "Р", "R" },
                { "С", "S" },
                { "Т", "T" },
                { "У", "U" },
                { "Ф", "F" },
                { "Х", "H" },
                { "Ц", "Ts" },
                { "Ч", "Ch" },
                { "Ш", "Sh" },
                { "Щ", "Sht" },
                { "Ъ", "A" },
                { "Ь", "Y" },
                { "Ю", "Yu" },
                { "Я", "Ya" },
            };

        /// <summary>
        /// Транслитериране от кирелица към латиница.
        /// </summary>
        /// <param name="value">Текст на кирилица.</param>
        /// <returns>Текст на латиница.</returns>
        public static string Transliterate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            string resultStr = value;

            foreach (var pattern in PatternReplacementDictionary)
            {
                resultStr = Regex.Replace(resultStr, pattern.Key, pattern.Value);
            }

            var result = new StringBuilder(resultStr);

            //Подменяме всички останали букви от речника
            foreach (var letter in AlphabetDictionary)
            {
                result.Replace(letter.Key, letter.Value);
            }

            return result.ToString();
        }
    }
}
