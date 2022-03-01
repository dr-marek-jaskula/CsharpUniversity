using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using Eltin_Buchard_Keller_Algorithm;

/*  
    To use BKTree:
    1. Create a class dervied from BKTreeNode
    2. Add a member variable of your data to be sorted / retrieved
    3. Override the calculateDistance method to calculate the distance metric between two nodes for the data to be sorted / retrieved.
    4. Instantiate a BKTree with the type name of the class created in (1).
*/

Console.WriteLine(DistanceMetric.CalculateLevenshteinDistance("kitten", "sitten"));
Console.WriteLine(DistanceMetric.CalculateLevenshteinDistance("sittin", "sitten"));
Console.WriteLine(DistanceMetric.CalculateLevenshteinDistance("sittin", "sitting"));

Console.WriteLine(DistanceMetric.CalculateLevenshteinDistance("thme", "them"));
Console.WriteLine(DistanceMetric.CalculateLevenshteinDistance("arithmatic", "arithmetic"));
Console.WriteLine(DistanceMetric.CalculateLevenshteinDistance("helo", "hello"));
Console.WriteLine(DistanceMetric.CalculateLevenshteinDistance("tommorrow", "tomorrow"));

Console.WriteLine(DistanceMetric.CalculateLevenshteinDistance("Ahri", "Ari"));
Console.WriteLine(DistanceMetric.CalculateLevenshteinDistance("Ahri", "Arhi"));
Console.WriteLine(DistanceMetric.CalculateLevenshteinDistance("Ahri", "Arh"));

//for those the appropriate threshold shoudl be equal to 2

// Create BKTree with derived node class from top of file
BKTree<LevenshteinNodeRecord> tree = new();

// Add some nodes
tree.Add(new LevenshteinNodeRecord(1, "demorogor"));
tree.Add(new LevenshteinNodeRecord(2, "bomoromoro"));
tree.Add(new LevenshteinNodeRecord(3, "sitting"));
tree.Add(new LevenshteinNodeRecord(4, "genzo"));
tree.Add(new LevenshteinNodeRecord(5, "wizugo"));

// Get best node from our tree with best distance
Dictionary<LevenshteinNodeRecord, Int32> results = tree.FindBestNodeWithDistance(new LevenshteinNodeRecord("kitten"));

// Get best nodes below threshold
results = tree.Query(new LevenshteinNodeRecord("kitten"), 3); // arbitrary threshold

Console.WriteLine("End of the application");