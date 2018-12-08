#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#tool "nuget:?package=GitVersion.CommandLine"
#addin "nuget:?package=NuGet.Core"
#addin "Cake.ExtendedNuGet"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var solution = "./Hitai.sln";
var buildDir = Directory("./Hitai/bin") + Directory(configuration);

var version = GitVersion();
var nugetResultPath = $"./nuget/Hitai.{version.NuGetVersion}.nupkg";

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
    .Does(()=>{
        CleanDirectory(buildDir);
        CleanDirectory(Directory("./Hitai.Test/bin") + Directory(configuration));
        CleanDirectory("./nuget");
        EnsureDirectoryExists("./Hitai/Properties");
    });
Task("NuGet-Restore")
    .Does(() => {
        NuGetRestore(solution);
    });
Task("AppVeyor-Version")
    .Does(() => {
        if(AppVeyor.IsRunningOnAppVeyor) {
            AppVeyor.UpdateBuildVersion($"{version.FullSemVer}.build.{AppVeyor.Environment.Build.Number}");
        }
    });
Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("NuGet-Restore")
    .IsDependentOn("AppVeyor-Version")
    .Does(()=>{
        if(IsRunningOnWindows()) {
            MSBuild(solution, settings => {
                    settings.SetConfiguration(configuration);
                    settings.SetVerbosity(Verbosity.Minimal);
                });
        }else{
            XBuild(solution, settings =>
                settings.SetConfiguration(configuration));
        }
    });
Task("Unit-Tests")
    .IsDependentOn("Build")
    .Does(() => {
        NUnit3("./**/bin/" + configuration + "/*.Test.dll", new NUnit3Settings {
        NoResults = false});
        if(AppVeyor.IsRunningOnAppVeyor){
            AppVeyor.UploadTestResults("TestResult.xml", AppVeyorTestResultsType.NUnit3);
        }
    });
Task("NuGet-Pack")
    .IsDependentOn("Unit-Tests")
    .IsDependentOn("Clean")
    .Does(() => {
        var settings = new NuGetPackSettings {
            Id = "Hitai",
            Version = version.NuGetVersion,
            Title = "Hitai",
            Authors = new [] {"Dennis Pražák"},
            Owners = new [] {"Dennis Pražák"},
            ProjectUrl = new Uri(@"https://github.com/sorashi/Hitai"),
            LicenseUrl = new Uri(@"https://github.com/sorashi/Hitai/blob/master/LICENSE.txt"),
            RequireLicenseAcceptance = false,
            Description = "A GUI application for client encryption using custom RSA implementation",
            Copyright = "Copyright Dennis Pražák 2019",
            Tags = new [] { "rsa" },
            BasePath = buildDir,
            OutputDirectory = "./nuget",
            Files = new [] {
                new NuSpecContent { Source = "Hitai.exe", Target = "lib/net452" },
                new NuSpecContent { Source = "LICENSE.txt", Target = "Content/Licenses/LICENSE.txt"}
            },
            Dependencies = GetPackageReferences("./Hitai")
                .Where(x => !x.IsDevelopmentDependency)
                .Select(x => new NuSpecDependency { 
                    Id = x.Id,
                    Version = x.Version.ToString(),
                    TargetFramework = x.TargetFramework.ToString() })
                .ToList()
        };
        NuGetPack(settings);
    });
Task("Default")
    .IsDependentOn("NuGet-Pack")
    .Does(() => {
        if(AppVeyor.IsRunningOnAppVeyor) {
            AppVeyor.UploadArtifact(nugetResultPath);
        }
    });

RunTarget(target);
