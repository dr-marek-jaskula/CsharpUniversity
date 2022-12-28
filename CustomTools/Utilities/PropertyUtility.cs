using System.Linq.Expressions;
using System.Reflection;

namespace CustomTools.Utilities;

public static class PropertyUtility
{
    public static MemberExpression MemberExpression(Expression expression)
    {
        LambdaExpression? lambda = expression as LambdaExpression;

        if (lambda is null)
        {
            throw new ArgumentException($"{nameof(expression)}");
        }

        MemberExpression? memberExpression = null;

        if (lambda.Body.NodeType is ExpressionType.Convert)
        {
            memberExpression = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
        }
        else if (lambda.Body.NodeType is ExpressionType.MemberAccess)
        {
            memberExpression = lambda.Body as MemberExpression;
        }

        if (memberExpression is null)
        {
            throw new ArgumentException($"Invalid argument: '{nameof(expression)}'");
        }

        return memberExpression;
    }

    public static string PropertyName<TType, TValue>(Expression<Func<TType, TValue>> property)
    {
        var memberExpression = MemberExpression(property);
        return memberExpression.Member.Name;
    }

    public static PropertyInfo? Property<TType>(string name)
    {
        var type = typeof(TType);
        return type.GetProperty(name);
    }

    public static PropertyInfo? Property<TType, TValue>(Expression<Func<TType, TValue>> property)
    {
        var memberExpression = MemberExpression(property);
        var type = typeof(TType);
        return type.GetProperty(memberExpression.Member.Name);
    }

    public static string PropertyNameCamelCase<TType, TValue>(Expression<Func<TType, TValue>> property)
    {
        return PropertyName(property)
            .ToCamelCase();
    }
}