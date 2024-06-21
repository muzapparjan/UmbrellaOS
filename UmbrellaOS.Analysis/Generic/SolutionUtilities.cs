namespace UmbrellaOS.Analysis.Generic;

public static class SolutionUtilities
{
    public static bool TrySearchSolutionPath(out string solutionPath)
    {
        var path = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        while (true)
        {
            var solutionFile = path.GetFiles("UmbrellaOS.sln").FirstOrDefault();
            if (solutionFile != null)
            {
                solutionPath = path.FullName;
                return true;
            }
            path = path.Parent;
            if (path == null)
            {
                solutionPath = string.Empty;
                return false;
            }
        }
    }
    public static bool TryGetProjects(out string[] projects)
    {
        if (!TrySearchSolutionPath(out var solutionPath))
        {
            projects = [];
            return false;
        }
        var projectDirs = Directory.GetDirectories(solutionPath)
            .Where(projectDir => File.Exists($"{projectDir}/{Path.GetFileName(projectDir)}.csproj"));
        if (!projectDirs.Any())
        {
            projects = [];
            return false;
        }
        projects = projectDirs!.Select(Path.GetFileName).ToArray()!;
        return true;
    }
    public static bool TrySearchProjectPath(string projectName, out string projectPath)
    {
        if (!TrySearchSolutionPath(out var solutionPath) ||
            !Directory.Exists($"{solutionPath}/{projectName}") ||
            !File.Exists($"{solutionPath}/{projectName}/{projectName}.csproj"))
        {
            projectPath = string.Empty;
            return false;
        }
        projectPath = $"{solutionPath}/{projectName}/{projectName}.csproj";
        return true;
    }
}