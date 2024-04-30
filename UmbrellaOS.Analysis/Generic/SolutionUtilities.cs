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
}