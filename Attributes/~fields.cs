namespace Ans.Net7.Common.Attributes.Fields
{

	/*

	[AttributeUsage(AttributeTargets.Property)]
	public class FieldText50Attribute
		: StringValidationAttribute
	{
		public FieldText50Attribute()
			: base()
		{
			MaxLength = 50;
		}
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldText100Attribute
		: StringValidationAttribute
	{
		public FieldText100Attribute()
			: base()
		{
			MaxLength = 100;
		}
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldText250Attribute
		: StringValidationAttribute
	{
		public FieldText250Attribute()
			: base()
		{
			MaxLength = 250;
		}
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldText400Attribute
		: StringValidationAttribute
	{
		public FieldText400Attribute()
			: base()
		{
			MaxLength = 400;
		}
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldTextBox400Attribute
		: StringValidationAttribute
	{
		public FieldTextBox400Attribute()
			: base()
		{
			MaxLength = 400;
		}
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldMemoAttribute
		: StringValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldDocAttribute
		: StringValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldNameAttribute
		: StringValidationAttribute
	{
		public FieldNameAttribute()
			: base()
		{
			MaxLength = 50;
			RegexPattern = _Consts.REGEX_NAME;
		}
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldVarnameAttribute
		: StringValidationAttribute
	{
		public FieldVarnameAttribute()
			: base()
		{
			MaxLength = 50;
			RegexPattern = _Consts.REGEX_VARNAME;
		}
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldEmailAttribute
		: StringValidationAttribute
	{
		public FieldEmailAttribute()
			: base()
		{
			MaxLength = 50;
			RegexPattern = _Consts.REGEX_EMAIL;
		}
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldIntAttribute
		: IntValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldLongAttribute
		: LongValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldFloatAttribute
		: FloatValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldDoubleAttribute
		: DoubleValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldDecimalAttribute
		: DecimalValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldDateTimeAttribute
		: DateTimeValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldDateAttribute
		: DateTimeValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldTimeAttribute
		: DateTimeValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldBoolAttribute
		: ValidationAttribute
	{
		protected override ValidationResult IsValid(
			object value,
			ValidationContext validationContext)
		{
			return ValidationResult.Success;
		}
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldEnumAttribute
		: ValidationAttribute
	{
		protected override ValidationResult IsValid(
			object value,
			ValidationContext validationContext)
		{
			return ValidationResult.Success;
		}
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldSetAttribute
		: StringValidationAttribute
	{
		public FieldSetAttribute()
			: base()
		{
			MaxLength = 400;
		}

		protected override ValidationResult IsValid(
			object value,
			ValidationContext validationContext)
		{
			string v1 = Convert.ToString(value);
			if (IsRequired && v1 == ";;")
				return new ValidationResult(
					Resources.Validation.ValueIsRequired);
			return base.IsValid(value, validationContext);
		}
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldReferenceAttribute
		: RegistryValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldPtrIntAttribute
		: RegistryValidationAttribute
	{
	}



	[AttributeUsage(AttributeTargets.Property)]
	public class FieldPasswordAttribute
		: StringValidationAttribute
	{
		public FieldPasswordAttribute()
			: base()
		{
			MaxLength = 50;
			RegexPattern = _Consts.REGEX_PASSWORD;
		}
	}






	[AttributeUsage(AttributeTargets.Property)]
	public class FieldHtmlAttribute
		: StringValidationAttribute
	{
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class FileSizeAttribute
		: ValidationAttribute
	{
		private readonly int maxSize;

		public FileSizeAttribute(
			int maxSize)
		{
			this.maxSize = maxSize;
		}

		public override bool IsValid(
			object value)
		{
			if (value == null)
				return true;
			return ((value as HttpPostedFileBase)
				.ContentLength <= maxSize);
		}

		public override string FormatErrorMessage(
			string name)
		{
			return string.Format(
				res_Errors.Template_File_FileSizeShouldNotExceed,
				maxSize);
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class FileTypesAttribute
		: ValidationAttribute
	{
		private readonly List<string> types;

		public FileTypesAttribute(
			string types)
		{
			this.types = types.Split(',').ToList();
		}

		public override bool IsValid(
			object value)
		{
			if (value == null)
				return true;
			string s1 = Path.GetExtension((value as HttpPostedFileBase).FileName)
				.Substring(1);
			return types.Contains(s1, StringComparer.OrdinalIgnoreCase);
		}

		public override string FormatErrorMessage(
			string name)
		{
			return string.Format(
				res_Errors.Template_File_InvalidFileType,
				string.Join(", ", types));
		}




	public class RegularExpressionWithOptionsAttribute
		: RegularExpressionAttribute,
		IClientValidatable
	{

		public RegularExpressionWithOptionsAttribute(
			string pattern)
			: base(pattern)
		{
		}


		public RegexOptions RegexOptions { get; set; }


		public override bool IsValid(
			object value)
		{
			if (string.IsNullOrEmpty(value as string))
				return true;
			return Regex.IsMatch(value as string, $"^{Pattern}$", RegexOptions);
		}

		public IEnumerable<System.Web.Mvc.ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			var rule = new ModelClientValidationRule
			{
				ErrorMessage = FormatErrorMessage(metadata.DisplayName),
				ValidationType = "regexwithoptions"
			};

			rule.ValidationParameters["pattern"] = Pattern;

			string flags = "";
			if ((RegexOptions & RegexOptions.Multiline) == RegexOptions.Multiline)
				flags += "m";
			if ((RegexOptions & RegexOptions.IgnoreCase) == RegexOptions.IgnoreCase)
				flags += "i";
			rule.ValidationParameters["flags"] = flags;

			yield return rule;
		}
	}



	}

	*/

}
