using Microsoft.AspNetCore.Mvc;
using Module.Explorer.Contracts.Competencies.Queries.GetCompetencies;
using Module.Explorer.Contracts.Tasks.Queries.GetTasks;
using Module.Explorer.Contracts.Tree.Queries.Get;

namespace WebApi.Controllers.Dashboard
{
    public class ExplorerController : DashboardBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetTree()
        {
            var request = new GetTreeQuery();
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompetencies(int jobId)
        {
            var request = new GetCompetenciesQuery
            {
                JobId = jobId
            };
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks(int competenceId)
        {
            var request = new GetTasksQuery
            {
                CompetenceId = competenceId
            };
            var result = await Mediator.Send(request);
            return Ok(result);
        }
    }
}