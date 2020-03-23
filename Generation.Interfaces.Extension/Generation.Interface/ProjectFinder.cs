using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

namespace Generation.Interface
{
    public class ProjectFinder
    {
        private readonly Solution solution;

        public ProjectFinder(Solution solution)
        {
            this.solution = solution;
        }

        internal IEnumerable<Project> GetProjects()
        {
            var result = solution.Projects
                .Where(project => IsProjectToAnalyze(project) && TargetProjectExists(project));

            return result;
        }

        internal static bool IsProjectToAnalyze(Project project)
        {
            return project.Name.EndsWith(".Implementations");
        }

        internal bool TargetProjectExists(Project project)
        {
            var result = solution.Projects.Any(projectTarget => projectTarget.Name == NamespaceHelper.GetTarget(project.Name.ToString()));

            return result;
        }
    }
}
