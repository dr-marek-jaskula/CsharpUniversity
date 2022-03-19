namespace Eltin_Buchard_Keller_Algorithm;

public class LeeNodeRecord : BKTreeNode
{
    public ushort Id { get; private set; }
    public int[] Data { get; private set; } // String of symbols

    public LeeNodeRecord(int[] values) : base()
    {
        Data = values;
        Id = 0;
    }

    public LeeNodeRecord(ushort id, int[] values): base()
    {
        if (id == 0)
            throw new ArgumentException("0 is a reserved Id value");
        Data = values;
        Id = id;
    }

    override protected int CalculateDistance(BKTreeNode node)
    {
        return DistanceMetric.CalculateLeeDistance(Data, ((LeeNodeRecord)node).Data);
    }
}

public class LevenshteinNodeRecord : BKTreeNode
{
    public ushort Id { get; private set; }
    public string Data { get; private set; }

    public LevenshteinNodeRecord(string values) : base()
    {
        Data = values;
        Id = 0;
    }

    public LevenshteinNodeRecord(ushort id, string values) : base()
    {
        if (id == 0)
            throw new ArgumentException("0 is a reserved Id value");
        Data = values;
        Id = id;
    }

    override protected int CalculateDistance(BKTreeNode node)
    {
        return DistanceMetric.CalculateLevenshteinDistance(Data, ((LevenshteinNodeRecord)node).Data);
    }
}