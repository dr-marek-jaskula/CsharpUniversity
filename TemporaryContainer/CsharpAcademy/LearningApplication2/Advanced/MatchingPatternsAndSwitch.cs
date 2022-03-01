using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication2.Advanced
{
    public class MatchingPatternsAndSwitch
    {
        public static void LearnMarchingPattern()
        {
            ILogger logger = new ConsoleLogger();

            string result = GetErrorMessage(logger, "Nice area");

            string result2 = RockPaperScissors("rock", "scissors");
            string result3 = RockPaperScissors("rock", "Hello");
            string result4 = RockPaperScissors("dsds", "dfd");
            string result5 = RockPaperScissors("rock", "dfdd");

            Perssson record1 = new("Goo", "mb");
            Perssson record2 = record1 with { FirstName = "ff" }; //record to check if its .net5

            string String1 = SwitchMe(new Perssson("Frank", "Jonas"));
            string String2 = SwitchMe(new MysteryClass() { LevelOrMystery = 20, PowerLevel = 3 });
            string String3 = SwitchMe(new MysteryClass() { LevelOrMystery = 20, PowerLevel = 7 });
            string String4 = SwitchMe(new OddClass() { PowerLevel = 20 });
            string String5 = SwitchMe(new OddClass() { LevelOrOddness = 50 });
            string String6 = SwitchMe(new List<int>() { 1, 2 });
            string String7 = SwitchMe(new OddClass() { LevelOrOddness = -5 });
        }

        //this function enables switching with pattern, so if logger "logLevel" is Debug then gives first result.
        public static string GetErrorMessage(ILogger logger, string area) => logger switch
        {
            { logLevel: LogLevel.Debug } => $"{area} - Debug: {logger.logLevel}",
            { logLevel: LogLevel.Verbose } =>$"{area} = Verbose: {logger.logLevel}",
            _ => $"{area} - { logger.logLevel}"
        };
        
        //this function shows the switching on tuple, and using discard (_) we can specify "any" character
        public static string RockPaperScissors(string first, string second) => (first, second) switch
        {
            ("rock", "paper") => "rock is covered by paper. Paper wins",
            ("rock", "scissors") => "rock breaks scissors. Rock wins.",
            (_, "Hello") => "ERROR",
            (_, _) => "tie"
        };

        public static string SwitchMe(object somethingStrange)
        {
            switch (somethingStrange)
            {
                case IPower { PowerLevel: <10 and >=5 }:
                    return "Average power";
                case MysteryClass { LevelOrMystery: 30 or 20}:
                    return "Mystery of 30 or 20";
                case OddClass { LevelOrOddness: 30 or > 40 }:
                    return "Odd of 30 or more then 40";
                case Perssson { FirstName: "Frank", LastName: "Jonas" }:
                    return "Frank Jonas here";
                case OddClass oddClass:
                    return $"Im so odd: {oddClass.LevelOrOddness} HEHE!";
                default:
                    return "It is a secret";
            }
        }
    }

    public record Perssson(string FirstName, string LastName);

    interface IPower
    {
        int PowerLevel { get; set; }
    }

    class MysteryClass : IPower
    {
        public int LevelOrMystery { get; set; }
        public int PowerLevel { get; set; }
    }

    class OddClass : IPower
    {
        public int LevelOrOddness { get; set; }
        public int PowerLevel { get; set; }
    }
}
