namespace CsharpAdvanced.Introduction;

using System;
using System.Globalization;
using System.Threading;
using static System.FormattableString;

internal class FormattableStrings
{
    public static void InvokeFormattableStringsExamples()
    {
        //create a CultureInfo instance 
        CultureInfo uk = CultureInfo.CreateSpecificCulture("en-GB");
        Thread.CurrentThread.CurrentCulture = uk; //set the uk culture as default

        string now = $"Default: it is now {DateTime.UtcNow}";
        Console.WriteLine(now); // UK format

        var germany = CultureInfo.CreateSpecificCulture("de-DE"); //other CultureInfo
        IFormattable formattableString = $"Specific: It is now {DateTime.UtcNow}";
        Console.WriteLine(formattableString.ToString("ignored", germany));

        FormattableString y = $"FormattableString: It is now {DateTime.UtcNow}";
        Console.WriteLine(FormattableString.Invariant(y)); // Via using static we can omit the "FormattableString" here
        Console.WriteLine(Invariant($"It is now {DateTime.UtcNow}"));
    }

    /* results:
     Default: it is now 16/02/2016 07:16:21
     Specific: It is now 16.02.2016 07:16:21
     FormattableString: It is now 02/16/2016 07:16:21
     It is now 02/16/2016 07:16:21
    */
}
