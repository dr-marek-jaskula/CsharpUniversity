using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CsharpAdvanced.Introduction;

public class UniversityRegex
{
    //We will discuss the use of regular expression (shot. regex) both with its syntax:
    //To learn regex use: https://regexr.com/

    //If we just write a regex like 'test' it matches all 'test' strings in the text

    //Escape character for regex "\"

    /*
        1. Anchors

            Beginning: ^
                Matches the beginning of the string, or the beginning of a line if the multiline flag (m) is enabled. This matches a position, not a character.
                Example: ^Regex

            End: $
                Matches the end of the string, or the end of a line if the multiline flag (m) is enabled. This matches a position, not a character.
                Example: Regex$

            Word boundary:
                Matches a word boundary position between a word character and non-word character or position (start / end of string). See the word character class (w) for more info.
                Example: s\b -> from 'she sells seashells' -> selects 's' from sells and seashells

            Not word boundary:
                Matches any position that is not a word boundary. This matches a position, not a character.
                Example: s\B -> from 'she sells seashells' -> selects all 's' that are not selected by s\b

            More examples:
            ^Good
            morning$

            To combine both we write:
            ^Good morning$ -> match the string to start from Good and end on morning

    */

    /*
        2. Character classes

            Character set: []
                Match any character in the set.
                Example: [ABC]

            Negated set: [^]
                Match any character that is not in the set.
                Example: [^ABC]
                Example: [^] -> match any character set, including line breaks, without the dotfall flag

            Range: [A-Z]
                Matches a character having a character code between the two specified characters inclusive.
                Example: [a-zA-Z] this regex gives small or capital letter
                Example: [a-z0-9] this regex gives small letter or digit

            Dot: .
                Matches any character except linebreaks. Equivalent to [^\n\r]. When dotall flag is enabled, . represents any character (including newline)

            Word: \w
                Matches any word character (alphanumeric & underscore). Only matches low-ascii characters (no accented or non-roman characters). Equivalent to [A-Za-z0-9_]
                Example: \w -> match for example "bonjur"

            Not word: \W
                Matches any character that is not a word character (alphanumeric & underscore). Equivalent to [^A-Za-z0-9_]
                Example: \W -> match for example ',' or space or '#', '$',, '*'

            Digit: \d
                Matches any digit character (0-9). Equivalent to [0-9]

            Not Digit: \D
                Matches any character that is not a digit character (0-9). Equivalent to [^0-9].

            Whitespace: \s
                Matches any whitespace character (spaces, tabs, line breaks).

            Not whitespace: \S
                Matches any character that is not a whitespace character (spaces, tabs, line breaks).
    */

    /*
       3. Quantifiers

            Zero or more: *
            Matches 0 or more of the preceding token.
            Example: abc* -> string needs to contain 'ab' and some 'c' letters (zero or more)
            Example: a(bc)* -> string needs to contain 'a' and some 'bc' (zero or more)

            One or more: +
            Matches 1 or more of the preceding token.
            Example: abc+ -> string needs to contain 'ab' and at least one 'c' letter

            Optional, Zero or 1: ?
            Matches 0 or 1 of the preceding token, effectively making it optional.
            Example: abc? -> string needs to contain 'ab' and zero or one 'c' letter

            Quantifier: {1,3}
            Matches the specified quantity of the previous token.
            {1,3} will match 1 to 3.
            {3} will match exactly 3.
            {3,} will match 3 or more.
            Example: abc{2} -> 'ab' and 'cc'
            Example: abc{2,} -> 'ab' and at least two 'c'
            Example: abc{2,5} -> 'ab' and at least two 'c' but max five 'c'

            Alternation: |
            Acts like a boolean OR. Matches the expression before or after the |. Be careful
            It can operate within a group, or on a whole expression. The patterns will be tested in order.
            Example: b(a|e|i)d -> words like "bad", "bed", "bid"
            Example: ba|d -> or 'ba' or 'd'
    */

    /*
      4. Flags:

        Global: g
            Retain the index of the last match, allowing subsequent searches to start from the end of the previous match.
            Without the global flag, subsequent searches will return the same match.
            RegExr only searches for a single match when the global flag is disabled to avoid infinite match errors.
                Without global it gives just first occurrence

        Multiline flag: m
            When the multiline flag is enabled, beginning and end anchors (^ and $) will match the start and end of a line, instead of the start and end of the whole string.
            Note that patterns such as /^[\s\S]+$/m may return matches that span multiple lines because the anchors will match the start/end of any line.

        Ignore case: i
            Makes the whole expression case-insensitive. For example, /aBc/i would match AbC.

        Dotall: s
            Dot (.) will match any character, including newline.

    5. Any in the string:

    ^                                            Match the beginning of the string
    (?=.*[0-9])                                  Require that at least one digit appear anywhere in the string
    (?=.*[a-z])                                  Require that at least one lowercase letter appear anywhere in the string
    (?=.*[A-Z])                                  Require that at least one uppercase letter appear anywhere in the string
    (?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\])        Require that at least one special character appear anywhere in the string
    .{8,32}                                      The password must be at least 8 characters long, but no more than 32
    $                                            Match the end of the string.

    summary:
    ^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$
*/

    //In order to use regex in c#:

    public void InvokeRegexExamples()
    {
        //For email validation
        Regex regex = new(@"^([a-z0-9]+)\.?([a-z0-9]+)@([a-z]+)\.[a-z]{2,3}$");
        string email = "test.test2@gmail.com";
        string email2 = ".test.test2@gmail.com";

        bool IsMatch = regex.IsMatch(email);
        bool IsMatch2 = regex.IsMatch(email2);
        Debug.WriteLine(IsMatch);
        Debug.WriteLine(IsMatch2);

        //Regex for email starting from small or capital letter and then can be a number.
        Regex regex2 = new(@"^([a-zA-Z])([a-zA-Z0-9]+)\.?([a-zA-Z0-9]+)@([a-z]+)\.[a-z]{2,3}$");
    }
}