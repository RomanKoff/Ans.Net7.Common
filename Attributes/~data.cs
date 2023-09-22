using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Ans.Net7.Common.Attributes.Data
{

    /*
     * class StringValidationAttribute : ValidationAttribute
     * class IntValidationAttribute : ValidationAttribute
     * class LongValidationAttribute : ValidationAttribute
     * class FloatValidationAttribute : ValidationAttribute
     * class DoubleValidationAttribute : ValidationAttribute
     * class DecimalValidationAttribute : ValidationAttribute
     * class DateTimeValidationAttribute : ValidationAttribute
     * class RegistryValidationAttribute : ValidationAttribute
     * class FieldSuccessAttribute : ValidationAttribute
     */



    [AttributeUsage(AttributeTargets.Property)]
    public class StringValidationAttribute
        : ValidationAttribute
    {
        public bool IsRequired { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public string RegexPattern { get; set; }
        public string CompareProperty { get; set; }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var v1 = Convert.ToString(value);
            if (IsRequired && string.IsNullOrWhiteSpace(v1))
                return new ValidationResult(
                    Resources.Validation.Text_ValueIsRequired);
            if (!string.IsNullOrEmpty(CompareProperty))
            {
                var compare2 = validationContext.ObjectType.GetProperty(CompareProperty);
                var attribute2 = (StringValidationAttribute)compare2
                    .GetCustomAttributes(typeof(StringValidationAttribute), false)[0];
                var valid2 = attribute2.IsValid(compare2
                    .GetValue(validationContext.ObjectInstance, null), validationContext);
                if (valid2 == ValidationResult.Success)
                {
                    var v2 = Convert.ToString(compare2
                        .GetValue(validationContext.ObjectInstance, null));
                    if (v1 != v2)
                        return new ValidationResult(
                            Resources.Validation.Text_ValueAndConfirmDoNotMatch);
                }
            }
            if (MinLength > 0 && v1.Length < MinLength)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_LengthMinLimit,
                    MinLength));
            if (MaxLength > 0 && v1.Length > MaxLength)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_LengthMaxLimit,
                    MaxLength));
            if (!string.IsNullOrEmpty(RegexPattern) && !Regex.IsMatch(v1, RegexPattern))
                return new ValidationResult(
                    Resources.Validation.Text_ValueDoesNotFit);
            return ValidationResult.Success;
        }
    }



    [AttributeUsage(AttributeTargets.Property)]
    public class IntValidationAttribute
        : ValidationAttribute
    {
        public bool IsRequired { get; set; }
        public int? NullValue { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var v1 = Convert.ToString(value).ToInt(0);
            if (Min != null || Max != null)
                IsRequired = true;
            if (IsRequired && v1 == NullValue)
                return new ValidationResult(
                    Resources.Validation.Text_ValueIsRequired);
            if (Min != null && v1 < Min.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_NumberMinValueLimit,
                    Min.Value));
            if (Max != null && v1 > Max.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_NumberMaxValueLimit,
                    Max.Value));
            return ValidationResult.Success;
        }
    }



    [AttributeUsage(AttributeTargets.Property)]
    public class LongValidationAttribute
        : ValidationAttribute
    {
        public bool IsRequired { get; set; }
        public long? NullValue { get; set; }
        public long? Min { get; set; }
        public long? Max { get; set; }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var v1 = Convert.ToString(value).ToLong(0);
            if (Min != null || Max != null)
                IsRequired = true;
            if (IsRequired && v1 == NullValue)
                return new ValidationResult(
                    Resources.Validation.Text_ValueIsRequired);
            if (Min != null && v1 < Min.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_NumberMinValueLimit,
                    Min.Value));
            if (Max != null && v1 > Max.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_NumberMaxValueLimit,
                    Max.Value));
            return ValidationResult.Success;
        }
    }



    [AttributeUsage(AttributeTargets.Property)]
    public class FloatValidationAttribute
        : ValidationAttribute
    {
        public bool IsRequired { get; set; }
        public float? NullValue { get; set; }
        public float? Min { get; set; }
        public float? Max { get; set; }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var v1 = Convert.ToString(value).ToFloat(0);
            if (Min != null || Max != null)
                IsRequired = true;
            if (IsRequired && v1 == NullValue)
                return new ValidationResult(
                    Resources.Validation.Text_ValueIsRequired);
            if (Min != null && v1 < Min.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_NumberMinValueLimit,
                    Min.Value));
            if (Max != null && v1 > Max.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_NumberMaxValueLimit,
                    Max.Value));
            return ValidationResult.Success;
        }
    }



    [AttributeUsage(AttributeTargets.Property)]
    public class DoubleValidationAttribute
        : ValidationAttribute
    {
        public bool IsRequired { get; set; }
        public double? NullValue { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var v1 = Convert.ToString(value).ToDouble(0);
            if (Min != null || Max != null)
                IsRequired = true;
            if (IsRequired && v1 == NullValue)
                return new ValidationResult(
                    Resources.Validation.Text_ValueIsRequired);
            if (Min != null && v1 < Min.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_NumberMinValueLimit,
                    Min.Value));
            if (Max != null && v1 > Max.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_NumberMaxValueLimit,
                    Max.Value));
            return ValidationResult.Success;
        }
    }



    [AttributeUsage(AttributeTargets.Property)]
    public class DecimalValidationAttribute
        : ValidationAttribute
    {
        public bool IsRequired { get; set; }
        public decimal? NullValue { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var v1 = Convert.ToString(value).ToDecimal(0);
            if (Min != null || Max != null)
                IsRequired = true;
            if (IsRequired && v1 == NullValue)
                return new ValidationResult(
                    Resources.Validation.Text_ValueIsRequired);
            if (Min != null && v1 < Min.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_NumberMinValueLimit,
                    Min.Value));
            if (Max != null && v1 > Max.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_NumberMaxValueLimit,
                    Max.Value));
            return ValidationResult.Success;
        }
    }



    [AttributeUsage(AttributeTargets.Property)]
    public class DateTimeValidationAttribute
        : ValidationAttribute
    {
        public bool IsRequired { get; set; }
        public DateTime? NullValue { get; set; }
        public DateTime? Min { get; set; }
        public DateTime? Max { get; set; }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var v1 = Convert.ToString(value).ToDateTime();
            if (Min != null || Max != null)
                IsRequired = true;
            if (IsRequired && v1 == NullValue)
                return new ValidationResult(
                    Resources.Validation.Text_ValueIsRequired);
            if (Min != null && v1 < Min.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_DataMinValueLimit,
                    Min.Value));
            if (Max != null && v1 > Max.Value)
                return new ValidationResult(string.Format(
                    Resources.Validation.Template_DataMaxValueLimit,
                    Max.Value));
            return ValidationResult.Success;
        }
    }



    [AttributeUsage(AttributeTargets.Property)]
    public class RegistryValidationAttribute
        : ValidationAttribute
    {
        public bool IsRequired { get; set; }
        public int? NullValue { get; set; }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var v1 = Convert.ToString(value).ToInt();
            if (IsRequired && v1 == NullValue)
                return new ValidationResult(
                    Resources.Validation.Text_ValueIsRequired);
            return ValidationResult.Success;
        }
    }



    [AttributeUsage(AttributeTargets.Property)]
    public class FieldSuccessAttribute
        : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }

}
