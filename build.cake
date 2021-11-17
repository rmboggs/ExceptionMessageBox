using System;
using System.Collections.Generic;

var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

var ProjectName = "ExceptionMessageBox";
var MainRepo = String.Concat("rmboggs/", ProjectName);
var MasterBranch = "main";

var netCoreAppsRoot= "./Source/";
var projects = new string[] { "WpfExceptionMessageBox" };
var netCoreProjects = new Dictionary<string, string>();

foreach (var p in projects)
{
    netCoreProjects.Add(p, String.Concat(netCoreAppsRoot, p));
}

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    foreach (var p in netCoreProjects)
    {
        Console.WriteLine($"Cleaning project {p.Key}";
        var path = $"{p.Value}/bin/{configuration}";
        CleanDirectory(path);
    }
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreBuild("./src/Example.sln", new DotNetCoreBuildSettings
    {
        Configuration = configuration,
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest("./src/Example.sln", new DotNetCoreTestSettings
    {
        Configuration = configuration,
        NoBuild = true,
    });
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);