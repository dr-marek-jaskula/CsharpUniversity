namespace CsharpAdvanced.PipelinerStateful.Interfaces;

internal interface IHasInput
{
}

internal interface IHasInput<TInput> : IHasInput
{
    public TInput? Input { get; set; }
}