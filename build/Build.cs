using Fallout.Common;
using Fallout.Common.IO;
using static Fallout.Common.Tools.Npm.NpmTasks;

// Dogfood: the extension's own build/publish pipeline is a Fallout build. It drives the
// Node toolchain (npm scripts wrapping tsc / vsce / ovsx) rather than reimplementing it —
// Fallout as a general orchestrator, not just a .NET build tool.
//
//   dotnet fallout Pack       -> produces fallout.vsix
//   dotnet fallout Publish    -> publishes to VS Marketplace + Open VSX
//
// Marketplace tokens are read from the environment by vsce (VSCE_PAT) and ovsx (OVSX_PAT);
// this build never handles them directly.
class Build : FalloutBuild
{
    public static int Main() => Execute<Build>(x => x.Pack);

    Target Restore => _ => _
        .Executes(() =>
        {
            Npm("ci", workingDirectory: RootDirectory);
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            Npm("run compile", workingDirectory: RootDirectory);
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            Npm("run package", workingDirectory: RootDirectory);
        });

    Target PublishMarketplace => _ => _
        .DependsOn(Pack)
        .Executes(() =>
        {
            Npm("run publish:vsce", workingDirectory: RootDirectory);
        });

    Target PublishOpenVsx => _ => _
        .DependsOn(Pack)
        .Executes(() =>
        {
            Npm("run publish:ovsx", workingDirectory: RootDirectory);
        });

    Target Publish => _ => _
        .DependsOn(PublishMarketplace, PublishOpenVsx);
}
