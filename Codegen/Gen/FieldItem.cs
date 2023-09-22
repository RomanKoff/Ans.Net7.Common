using Ans.Net7.Common.Codegen.Schema;

namespace Ans.Net7.Common.Codegen.Gen
{

    public class FieldItem
    {

        /* ctors */


        public FieldItem(
            PropertyXmlElement source)
        {
            Use = source.Use;
            Name = source.Name;
            DefaultValue = source.DefaultValue;
            DefaultSql = source.DefaultSql;
            Prefix = source.Prefix ?? "";
            Enum = source.Enum ?? "";
            Face = source.Face ?? Name;
            Remark = source.Remark ?? "";
            IsReadonly = source.IsReadonly;
            switch (source.Type)
            {
                case PropertyTypesEnum.Text50:
                    CSharpType = "string";
                    CSharpAttribute = "StringValidation";
                    MaxLength = 50;
                    break;
                case PropertyTypesEnum.Text100:
                    CSharpType = "string";
                    CSharpAttribute = "StringValidation";
                    MaxLength = 100;
                    break;
                case PropertyTypesEnum.Text250:
                    CSharpType = "string";
                    CSharpAttribute = "StringValidation";
                    MaxLength = 250;
                    break;
                case PropertyTypesEnum.Text400:
                    CSharpType = "string";
                    CSharpAttribute = "StringValidation";
                    MaxLength = 400;
                    break;
                case PropertyTypesEnum.TextBox400:
                    CSharpType = "string";
                    CSharpAttribute = "StringValidation";
                    MaxLength = 400;
                    break;
                case PropertyTypesEnum.Memo:
                    CSharpType = "string";
                    CSharpAttribute = "StringValidation";
                    break;
                case PropertyTypesEnum.Doc:
                    CSharpType = "string";
                    CSharpAttribute = "StringValidation";
                    break;
                case PropertyTypesEnum.Name:
                    CSharpType = "string";
                    CSharpAttribute = "StringValidation";
                    MaxLength = 50;
                    RegexPattern = _Consts.REGEX_NAME;
                    break;
                case PropertyTypesEnum.Varname:
                    CSharpType = "string";
                    CSharpAttribute = "StringValidation";
                    MaxLength = 50;
                    RegexPattern = _Consts.REGEX_VARNAME;
                    break;
                case PropertyTypesEnum.Email:
                    CSharpType = "string";
                    CSharpAttribute = "StringValidation";
                    MaxLength = 50;
                    RegexPattern = _Consts.REGEX_EMAIL;
                    break;
                case PropertyTypesEnum.Int:
                    CSharpType = "int";
                    CSharpAttribute = "IntValidation";
                    break;
                case PropertyTypesEnum.Long:
                    CSharpType = "long";
                    CSharpAttribute = "LongValidation";
                    break;
                case PropertyTypesEnum.Float:
                    CSharpType = "float";
                    CSharpAttribute = "FloatValidation";
                    break;
                case PropertyTypesEnum.Double:
                    CSharpType = "double";
                    CSharpAttribute = "DoubleValidation";
                    break;
                case PropertyTypesEnum.Decimal:
                    CSharpType = "decimal";
                    CSharpAttribute = "DecimalValidation";
                    break;
                case PropertyTypesEnum.Datetime:
                    CSharpType = "DateTime?";
                    CSharpAttribute = "DateTimeValidation";
                    break;
                case PropertyTypesEnum.Date:
                    CSharpType = "DateTime?";
                    CSharpAttribute = "DateTimeValidation";
                    break;
                case PropertyTypesEnum.Time:
                    CSharpType = "DateTime?";
                    CSharpAttribute = "DateTimeValidation";
                    break;
                case PropertyTypesEnum.Bool:
                    CSharpType = "bool";
                    CSharpAttribute = "FieldSuccess";
                    break;
                case PropertyTypesEnum.Enum:
                    CSharpType = "int";
                    CSharpAttribute = "FieldSuccess";
                    IsRegistry = true;
                    IsEnum = true;
                    if (source.Use == PropertyUsesEnum.Required)
                        Enum = "!;" + Enum;
                    break;
                case PropertyTypesEnum.Set:
                    CSharpType = "string";
                    CSharpAttribute = "StringValidation";
                    MaxLength = 400;
                    break;
                case PropertyTypesEnum.Reference:
                    CSharpType = "int?";
                    CSharpAttribute = "RegistryValidation";
                    Name = $"{Prefix}{Name}Ptr";
                    IsRegistry = true;
                    break;
                case PropertyTypesEnum.PtrInt:
                    CSharpType = "int?";
                    CSharpAttribute = "RegistryValidation";
                    break;
            }
        }


        /* properties */


        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public string DefaultSql { get; set; }
        public string Prefix { get; set; }
        public string Enum { get; set; }
        public string Face { get; set; }
        public string Remark { get; set; }
        public bool IsReadonly { get; set; }

        public PropertyUsesEnum Use
        {
            get => _use;
            set
            {
                _use = value;
                if (_use != PropertyUsesEnum.Normal)
                {
                    IsRequired = true;
                    if (_use != PropertyUsesEnum.Required)
                    {
                        IsUnique = true;
                        IsAbsoluteUnique = _use != PropertyUsesEnum.Unique;
                    }
                }
            }
        }
        private PropertyUsesEnum _use;

        public string CSharpType { get; set; }
        public string CSharpAttribute { get; set; }
        public int MaxLength { get; set; }
        public string RegexPattern { get; set; }
        public bool IsRegistry { get; set; }
        public bool IsEnum { get; set; }

        public TableItem TargetTable { get; set; }
        public TableItem ManyrefTable { get; set; }
        public string Target { get; set; }
        public string Manyref { get; set; }
        public string ManyrefFieldName { get; set; }


        /* readonly properties */


        public bool IsRequired { get; private set; }
        public bool IsUnique { get; private set; }
        public bool IsAbsoluteUnique { get; private set; }


        /* functions */


        public string GetCSharpAttribute()
        {
            var props1 = new List<string>();
            if (IsRequired)
                props1.Add($"IsRequired = true");
            if (MaxLength > 0)
                props1.Add($"MaxLength = {MaxLength}");
            if (!string.IsNullOrEmpty(RegexPattern))
                props1.Add($"RegexPattern = \"{RegexPattern}\"");
            return $"[{CSharpAttribute}({props1.MakeFromCollection(null, "\n\t\t\t{0}", ",")})]";
        }


        public string GetCSharpDerlace()
        {
            var s1 = Remark.Make(" // {0}");
            return $"{CSharpType} {Name} {{ get; set; }}{s1}";
        }

    }

}