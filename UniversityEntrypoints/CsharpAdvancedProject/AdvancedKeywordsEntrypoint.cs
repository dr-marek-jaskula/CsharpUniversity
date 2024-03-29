﻿using Xunit;
using CsharpAdvanced.Keywords;

namespace UniversityEntrypoints.CsharpAdvancedProject;

public class AdvancedKeywordsEntrypoint
{
    [Fact]
    public void YieldEntrypoint()
    {
        Yield.InvokeYieldExamples();
    }

    [Fact]
    public void RefEntrypoint()
    {
        OutRef.InvokeRefExamples();
    }
}

