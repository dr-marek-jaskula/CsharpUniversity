using Xunit;
using CsharpBasics.Introduction;

namespace UniversityEntrypoints.CsharpBasicsProject;

public class BasicIntroductionEntrypoint
{
    [Fact]
    public void LoopsConditionlsEntrypoint()
    {
        LoopsConditionals.InvokeLoopsExamples();
    }

    [Fact]
    public void EnumsEntrypoint()
    {
        Enums.InvokeEnumExamples();
    }

    [Fact]
    public void DictionariesEntrypoint()
    {
        Dictionaries.InvokeDictionaryExamples();
    }

    [Fact]
    public void StringsEntrypoint()
    {
        Strings.InvokeStringsExample();
    }

    [Fact]
    public void TypesEntrypoint()
    {
        Types.InvokeTypesExamples();
    }

    [Fact]
    public void CastingEntrypoint()
    {
        Casting.InvokeCastingExamples();
    }

    [Fact]
    public void PropertiesEntrypoint()
    {
        Properties.InvokePropertiesExamples();
    }
    
    [Fact]
    public void TryCatchFinallyEntrypoint()
    {
        TryCatchFinally.InvakeTryCatchFinallyExamples();
    }
    
    [Fact]
    public void FinalizersEntrypoint()
    {
        Finalizers.InvokeFinalizersExample();
    }

    [Fact]
    public void GuidEntrypoint()
    {
        GUID.InvokeGuidExamples();
    }

    [Fact]
    public void IndexesEntrypoint()
    {
        IndexRange.InvokeIndexesExamples();
    }
}