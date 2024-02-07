using NTypewriter.CodeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EAU.PBZN.Documents.Models.NTypeWriter
{
    class NTypeWriterCustomFunctions
    {
        //Enum-и, които трябва да бъдат генерирани
        private readonly static string[] includedEnums = new string[]
        {
            "EntityOrPerson",
            "CertificateType"
        };

        //Важен е реда в който се добавят. Първо се поставят класове без базови такива, следват всички който ги наследяват но не са абстрактни и накрая са абстрактните.
        private readonly static string[] includedClasses = new string[]
        {
            "ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesVM",
            "ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM",
            "ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM",
            "ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM",
            "CertifiedPersonelVM",
            "ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutVM",
            "ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM",
            "CertificateForAccidentVM",
            "CertificateToWorkWithFluorinatedGreenhouseGassesVM"
        };

        //Namespace-ите, които се вземат предвид при генериране на класовете
        private readonly static string[] includedNamespaces = new string[]
        {
            "EAU.PBZN.Documents.Models.Forms",
            "EAU.PBZN.Documents.Models",
            "EAU.PBZN.Documents.Domain.Models"
        };

        public static IEnumerable<IEnum> GetEnums(IEnumerable<IEnum> enums)
        {
            var allowedEnums = enums.Where(c => includedEnums.Contains(c.Name) && includedNamespaces.Contains(c.Namespace));

            return allowedEnums;
        }

        public static bool HasBaseProp(IClass @class, String @propName)
        {
            var baseclass = @class.BaseClass;
            while (baseclass != null)
            {
                if (baseclass.Properties.Any(p => p.Name == @propName))
                {
                    return true;
                }

                baseclass = baseclass.BaseClass;
            }

            return false;
        }

        public static IEnumerable<IClass> GetClasses(IEnumerable<IClass> classes, params string[] namespaces)
        {
            var allowedClasses = classes.Where(c => string.Compare(c.Name, "Namespaces", true) != 0
                && string.Compare(c.Name, "NTypeWriterCustomFunctions.nt", true) != 0
                && includedNamespaces.Contains(c.Namespace));

            var result = new List<IClass>();

            foreach (string item in includedClasses)
            {
                var tmp = allowedClasses.First(c => string.Compare(c.Name, item, true) == 0 || (c.IsGeneric && string.Compare(c.BareName, item, true) == 0));

                result.Add(tmp);
            }

            return result;
        }

        public static string getTypeSystemForClass(IClass @class)
        {
            if (@class.IsGeneric == true)
            {
                return string.Format("@TypeSystem.typeDecorator('{0}', moduleContext.moduleName)", @class.BareName);
            }
            else
            {
                return string.Format("@TypeSystem.typeDecorator('{0}', moduleContext.moduleName)", @class.Name);
            }
        }

        public static string getClassDeclaration(IClass @class, IEnumerable<IType> allReference = null)
        {
            if (@class.IsGeneric && allReference != null)
            {
                StringBuilder genericArgs = new StringBuilder(string.Empty);
                List<IType> argsTypes = new List<IType>();
                int argsCount = @class.TypeArguments.Count();
                int n = argsCount, allRefCnt = allReference.Count();

                int lastrefIndex = allRefCnt - 1;

                while (n > 0)
                {
                    argsTypes.Add(allReference.ElementAt(lastrefIndex));
                    lastrefIndex -= 1;
                    n -= 1;
                }

                argsTypes.Reverse();

                for (int i = 0; i < argsCount; i++)
                {
                    string baseClassName = argsTypes.ElementAt(i).Name;
                    genericArgs.AppendFormat("{0} extends {1}{2}", @class.TypeArguments.ElementAt(i).Name, baseClassName, i < (argsCount - 1) ? "," : "");
                }

                return string.Format("export class {0}<{1}> extends {2}", @class.BareName, genericArgs.ToString(), @class.BaseType.Name);
            }
            else
            {
                if (@class.HasBaseClass)
                {
                    return string.Format("export class {0} extends {1}", @class.Name, @class.BaseType.Name);
                }
                else
                {
                    return string.Format("export class {0} extends BaseDataModel", @class.Name);
                }
            }
        }

        public static string getClassProperty(IClass @class, IProperty property, string unwrapedTypeScriptType, string unwrapedTypeScriptArrayItemType, IEnumerable<IType> allReference = null)
        {
            StringBuilder sbProp = new StringBuilder(string.Empty);
            List<IType> argsTypes = new List<IType>();
            int genericPropIdx = 0;

            if (allReference != null)
            {
                int argsCount = @class.TypeArguments.Count();
                int n = argsCount, allRefCnt = allReference.Count();

                int lastrefIndex = allRefCnt - 1;

                while (n > 0)
                {
                    argsTypes.Add(allReference.ElementAt(lastrefIndex));
                    lastrefIndex -= 1;
                    n -= 1;
                }

                argsTypes.Reverse();
            }

            string camelCasePropName = ToCamelCase(property.Name);

            if (property.Attributes.Any(a => a.Name == "JsonPropertyName"))
            {
                var jsonAttr = property.Attributes.First(a => a.Name == "JsonPropertyName");
                string jsonAttrName = jsonAttr.Arguments.ElementAt(0).Value.ToString();
                camelCasePropName = ToCamelCase(jsonAttrName);
            }

            if (property.Type.IsArray || property.Type.IsCollection)
            {
                string arrayItemTSType = property.Type.IsArray ? unwrapedTypeScriptArrayItemType : unwrapedTypeScriptType;

                sbProp.AppendFormat("{0}\r\n\r\n", getMobxField(camelCasePropName, arrayItemTSType, true));

                if (allReference != null && property.Type.Name == @class.TypeArguments.ElementAt(genericPropIdx).Name)
                {
                    sbProp.AppendFormat("{0}\r\n", getPropertyTypeSystem(argsTypes.ElementAt(genericPropIdx).Name, true));

                    genericPropIdx += 1;
                }
                else
                {
                    sbProp.AppendFormat("{0}\r\n", getPropertyTypeSystem(arrayItemTSType, true));
                }

                sbProp.AppendFormat("{0}\r\n", getPropertySetter(camelCasePropName, arrayItemTSType, true));
                sbProp.AppendFormat("{0}\r\n", getPropertyGetter(camelCasePropName, arrayItemTSType, true));
            }
            else
            {
                sbProp.AppendFormat("{0}\r\n\r\n", getMobxField(camelCasePropName, unwrapedTypeScriptType));

                if (allReference != null && property.Type.Name == @class.TypeArguments.ElementAt(genericPropIdx).Name)
                {
                    sbProp.AppendFormat("{0}\r\n", getPropertyTypeSystem(argsTypes.ElementAt(genericPropIdx).Name), true);

                    genericPropIdx += 1;
                }
                else
                {
                    sbProp.AppendFormat("{0}\r\n", getPropertyTypeSystem(unwrapedTypeScriptType), true);
                }

                sbProp.AppendFormat("{0}\r\n", getPropertySetter(camelCasePropName, unwrapedTypeScriptType));
                sbProp.AppendFormat("{0}\r\n", getPropertyGetter(camelCasePropName, unwrapedTypeScriptType));
            }

            return sbProp.ToString();
        }

        public static string getClassConstructor()
        {
            return string.Format("\tconstructor(data?: any) {{{0}\t\tsuper(data);{0}{0}\t\tthis.copyFrom(data);{0}\t}}{0}", Environment.NewLine);
        }

        private static string ToCamelCase(string str)
        {
            var words = str.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);
            var leadWord = Regex.Replace(words[0], @"([A-Z])([A-Z]+|[a-z0-9]+)($|[A-Z]\w*)",
                m =>
                {
                    return m.Groups[1].Value.ToLower() + m.Groups[2].Value.ToLower() + m.Groups[3].Value;
                });
            var tailWords = words.Skip(1)
                .Select(word => char.ToUpper(word[0]) + word.Substring(1))
                .ToArray();
            return $"{leadWord}{string.Join(string.Empty, tailWords)}";
        }

        private static string getTSType(string unwrapedTypeScriptType, bool isArray = false)
        {
            switch (unwrapedTypeScriptType)
            {
                case "Date":
                    return isArray == true ? "moment.Moment[]" : "moment.Moment";
                case "JsonElement":
                    return isArray == true ? "any[]" : "any";
                default:
                    return isArray == true ? string.Format("{0}[]", unwrapedTypeScriptType) : unwrapedTypeScriptType;
            }
        }

        private static string getMobxField(string camelCasePropName, string unwrapedTypeScriptType, bool isArray = false)
        {
            string pattern = "\t@observable private _{0}: {1} = null;";

            return string.Format(pattern, camelCasePropName, getTSType(unwrapedTypeScriptType, isArray));
        }

        private static string getPropertyTypeSystem(string unwrapedTypeScriptType, bool isArray = false)
        {
            string pattern = isArray ? "\t@TypeSystem.propertyArrayDecorator({0})" : "\t@TypeSystem.propertyDecorator({0})";

            switch (unwrapedTypeScriptType)
            {
                case "Date":
                case "Date | null":
                    return string.Format(pattern, "'moment'");
                case "string":
                    return string.Format(pattern, "'string'");
                case "any":
                case "JsonElement":
                    return string.Format(pattern, "'any'");
                case "boolean":
                case "boolean | null":
                    return string.Format(pattern, "'boolean'");
                case "number":
                case "number | null":
                    return string.Format(pattern, "'number'");
                default:
                    return string.Format(pattern, string.Format("{0} ? {0} : moduleContext.moduleName + '.{0}'", unwrapedTypeScriptType));
            }
        }

        private static string getPropertySetter(string camelCasePropName, string unwrapedTypeScriptType, bool isArray = false)
        {
            string pattern = "\tpublic set {0}(val: {1}) {{{2}\t\tthis._{0} = val;{2}\t}}{2}{2}";

            return string.Format(pattern, camelCasePropName, getTSType(unwrapedTypeScriptType, isArray), Environment.NewLine);
        }

        private static string getPropertyGetter(string camelCasePropName, string unwrapedTypeScriptType, bool isArray = false)
        {
            string pattern = "\tpublic get {0}(): {1} {{{2}\t\treturn this._{0};{2}\t}}{2}";

            return string.Format(pattern, camelCasePropName, getTSType(unwrapedTypeScriptType, isArray), Environment.NewLine);
        }
    }
}