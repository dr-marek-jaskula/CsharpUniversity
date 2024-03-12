namespace CsharpAdvanced.PipelinerStateful.Interfaces;

internal interface IHasOutput
{
}

internal interface IHasOutput<TOutput> : IHasOutput
{
    public TOutput? Output { get; set; }
}