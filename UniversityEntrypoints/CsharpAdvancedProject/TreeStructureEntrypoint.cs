﻿using CsharpAdvanced.TreeStructure;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace UniversityEntrypoints.CsharpAdvancedProject;

public class TreeStructureEntrypoint
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TreeStructureEntrypoint(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Entrypoint()
    {
        ExecuterTreeStructure.Invoke(_testOutputHelper.WriteLine);
    }
}