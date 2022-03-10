﻿namespace CsharpAdvanced.Introduction;

public class OverloadingOperators
{
    //We can overload the following operators:
    //+x,-x,!x,~x,++,--,true,false
    //x+y, x-y, x*y, x/y, x % y. x & y, x | y, x ^ y, x << y, x >> y, x == y, x != y, x < y, x > y, x <= y, x >= y
    //x && y, x || y
    //a[i], a?[i] (Element access is not considered an overloadable operator, but you can define an indexer.)
    //(T)x (casting cannot be overloaded by can define custom type conversion that can be performed by a cast expression)
    //+=, -=, *=, /=, %=, &=, |=, ^=, <<=, >>= 

    //and these cannot be overloaded:
    //^x, x = y, x.y, x?.y, c ? t : f, x ?? y, x ??= y, x..y, x->y, =>, f(x), as, await, checked, unchecked, default, delegate, is, nameof, new, sizeof, stackalloc, switch, typeof, with 	

    public static void InvokeOverloadingOperatorsExamples()
    {
        var a = new Fraction(5, 4);
        var b = new Fraction(1, 2);
        Console.WriteLine(-a);   // output: -5 / 4
        Console.WriteLine(a + b);  // output: 14 / 8
        Console.WriteLine(a - b);  // output: 6 / 8
        Console.WriteLine(a * b);  // output: 5 / 8
        Console.WriteLine(a / b);  // output: 10 / 4

        SomeList someList = new();
        Console.WriteLine(someList[3]);
        someList[3] = 10;
    }
}

//Standard operators overloading example
public readonly struct Fraction
{
    private readonly int _numerator;
    private readonly int _denominator;

    public Fraction(int numerator, int denominator)
    {
        if (denominator is 0)
            throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));

        _numerator = numerator;
        _denominator = denominator;
    }

    //overload operator '+' for single argument (returns the same fraction)
    public static Fraction operator +(Fraction fraction) => fraction;

    //overload operator '-' for single argument (returns the opposed fraction)
    public static Fraction operator -(Fraction fraction) => new(-fraction._numerator, fraction._denominator);

    //overload operator '+' for adding two fractions
    public static Fraction operator +(Fraction a, Fraction b)
        => new Fraction(a._numerator * b._denominator + b._numerator * a._denominator, a._denominator * b._denominator);

    public static Fraction operator -(Fraction a, Fraction b)
        => a + (-b);

    public static Fraction operator *(Fraction a, Fraction b)
        => new Fraction(a._numerator * b._numerator, a._denominator * b._denominator);

    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b._numerator == 0)
            throw new DivideByZeroException();
        return new Fraction(a._numerator * b._denominator, a._denominator * b._numerator);
    }

    public override string ToString() => $"{_numerator} / {_denominator}";
}

//overloading index operator 

public class SomeList
{
    public int this[int key]
    {
        get => GetValue(key);
        set => SetValue(key, value);
    }

    private void SetValue(int key, int value)
    {
        Console.WriteLine($"I'm setting value {value} for key {key}");
    }

    private int GetValue(int key)
    {
        Console.WriteLine($"I'm getting value that corresponds to key {key}");
        return 0;
    }
}

