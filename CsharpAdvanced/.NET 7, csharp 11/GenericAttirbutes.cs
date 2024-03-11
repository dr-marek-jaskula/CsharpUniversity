namespace CsharpAdvanced.NET_7__csharp_11;

[Validator<UserValidator>]
public sealed class User
{

}

//We can have generic parameters and therefore we can set constraints!:
public sealed class ValidatorAttribute<TValidator> : Attribute
	where TValidator : IValidator
{
	public Type ValidatorType { get; }

	public ValidatorAttribute()
	{
		ValidatorType = typeof(TValidator);
	}
}

public interface IValidator
{

}

public sealed class UserValidator : IValidator
{

}