namespace CsharpAdvanced.TreeStructure;

public class ExecuterTreeStructure
{
    public static void Invoke()
    {
        var tree = Tree<NodeName, NodeData>.Create(NodeName.Root, ExecuteRootData, DetermineNodeNameFromRoot, root => 
        {
            root.AddSubsequentNode(NodeName.NodeOne, ExecuteNodeOneData, DetermineNodeNameFromNodeOne, x => 
            {
                x.AddSubsequentNode(NodeName.FirstDeepNodeOne, ExecuteFirstDeepNodeOneData)
                    .AddSubsequentNode(NodeName.VeryDeepNodeOne, ExecuteVeryDeepNodeOneData)
                        .AddSubsequentNode(NodeName.VeryVeryDeepNodeOne, ExecuteVeryVeryDeepNodeOneData);
                x.AddSubsequentNode(NodeName.SecondDeepNodeOne, ExecuteSecondDeepNodeOneData);
            });

            root.AddSubsequentNode(NodeName.NodeTwo, ExecuteNodeTwoData, DetermineNodeNameFromNodeTwo, x =>
            {
                x.AddSubsequentNode(NodeName.FirstDeepNodeTwo, ExecuteFirstDeepNodeTwoData);
                x.AddSubsequentNode(NodeName.SecondDeepNodeTwo, ExecuteSecondDeepNodeTwoData);
            });
        });

        var lastResult = tree.ExecuteTreeFlow(new NodeData("Start:"));
        Console.WriteLine(lastResult); //Output: "NodeData { Input = Start: -> Root -> OptionOne -> DeepOne -> DeepIndeed -> LastBoss }"
    }

    public static NodeData ExecuteRootData(NodeData nodeData)
    {
        return new NodeData(nodeData.Input + $" -> {NodeName.Root}");
    }

    public static NodeData ExecuteNodeOneData(NodeData nodeData)
    {
        return new NodeData(nodeData.Input + $" -> {NodeName.NodeOne}");
    }

    public static NodeData ExecuteNodeTwoData(NodeData nodeData)
    {
        return new NodeData(nodeData.Input + $" -> {NodeName.NodeTwo}");
    }

    public static NodeData ExecuteFirstDeepNodeOneData(NodeData nodeData)
    {
        return new NodeData(nodeData.Input + $" -> {NodeName.FirstDeepNodeOne}");
    }

    public static NodeData ExecuteSecondDeepNodeOneData(NodeData nodeData)
    {
        return new NodeData(nodeData.Input + $" -> {NodeName.SecondDeepNodeOne}");
    }
    
    public static NodeData ExecuteFirstDeepNodeTwoData(NodeData nodeData)
    {
        return new NodeData(nodeData.Input + $" -> {NodeName.FirstDeepNodeTwo}");
    }

    public static NodeData ExecuteSecondDeepNodeTwoData(NodeData nodeData)
    {
        return new NodeData(nodeData.Input + $" -> {NodeName.SecondDeepNodeTwo}");
    }

    public static NodeData ExecuteVeryDeepNodeOneData(NodeData nodeData)
    {
        return new NodeData(nodeData.Input + $" -> {NodeName.VeryDeepNodeOne}");
    }

    public static NodeData ExecuteVeryVeryDeepNodeOneData(NodeData nodeData)
    {
         return new NodeData(nodeData.Input + $" -> {NodeName.VeryVeryDeepNodeOne}");
    }

    public static NodeName DetermineNodeNameFromRoot(NodeData nodeData)
    {
        return TreeExtension.GetRandomOf(NodeName.NodeOne, NodeName.NodeTwo);
    }

    public static NodeName DetermineNodeNameFromNodeOne(NodeData nodeData)
    {
        return TreeExtension.GetRandomOf(NodeName.FirstDeepNodeOne, NodeName.SecondDeepNodeOne);
    }

    public static NodeName DetermineNodeNameFromNodeTwo(NodeData nodeData)
    {
        return TreeExtension.GetRandomOf(NodeName.FirstDeepNodeTwo, NodeName.SecondDeepNodeTwo);
    }
}