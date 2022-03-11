﻿namespace CsharpAdvanced.Settings;

public class GlobalUsings
{
    //Since c# 10 (.NET 6) the most common global usings:
    /* <auto-generated/>
    global using global::System;
    global using global::System.Collections.Generic;
    global using global::System.IO;
    global using global::System.Linq;
    global using global::System.Net.Http;
    global using global::System.Threading;
    global using global::System.Threading.Tasks;
    */
    //are included globally by default
    //This can be verified in .csproj file:
    //<ImplicitUsings>enable</ImplicitUsings>
    //Above line states that the default global usings will be applied

    //To add other usings into the default package of usings that are used for the project we need to add in .csproj file:
    /*
     <ItemGroup>
    <Using Include="CsharpBasics.Memory"/>
    <Using Include="System.Console" Static="True" />
    <Using Include="System.DateTime" Alias="DT" />
    </ItemGroup>
     */

    //inside the "obj" folder that gets created when you build a project can find the subfolder named for our build configuration (debug or relese) containing a net6.0 folder
    //Inside we can find a file called similarly to "MyProject.GlobalUsings.g.cs" and in this file we find all the default global usings. For AspNetCore there are even more:
    /*
     // <autogenerated />
    global using global::System;
    global using global::System.Collections.Generic;
    global using global::System.IO;
    global using global::System.Linq;
    global using global::System.Net.Http;
    global using global::System.Threading;
    global using global::System.Threading.Tasks;
    global using global::System.Net.Http.Json;
    global using global::Microsoft.AspNetCore.Builder;
    global using global::Microsoft.AspNetCore.Hosting;
    global using global::Microsoft.AspNetCore.Http;
    global using global::Microsoft.AspNetCore.Routing;
    global using global::Microsoft.Extensions.Configuration;
    global using global::Microsoft.Extensions.DependencyInjection;
    global using global::Microsoft.Extensions.Hosting;
    global using global::Microsoft.Extensions.Logging;
     */

    //The preferred way is to add new global usings through .csproj file
}

