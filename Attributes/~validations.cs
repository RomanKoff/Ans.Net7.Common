using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ans.Net7.Common.Attributes
{

	public class AnsRequiredAttribute
		: RequiredAttribute
	{
		public AnsRequiredAttribute()
		{
			ErrorMessage = Resources.ModelErrors.Required;
		}
	}



	public class AnsMaxLengthAttribute
		: MaxLengthAttribute
	{
		public AnsMaxLengthAttribute(
			int length)
			: base(length)
		{
			ErrorMessage = Resources.ModelErrors.MaxLength_TML;
		}
	}



	public class AnsMinLengthAttribute
		: MinLengthAttribute
	{
		public AnsMinLengthAttribute(
			int length)
			: base(length)
		{
			ErrorMessage = Resources.ModelErrors.MinLength_TML;
		}
	}



	public class AnsRegularExpressionAttribute
		: RegularExpressionAttribute
	{
		public AnsRegularExpressionAttribute(
			[StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
			: base(pattern)
		{
			ErrorMessage = Resources.ModelErrors.RegularExpression;
		}
	}

}
