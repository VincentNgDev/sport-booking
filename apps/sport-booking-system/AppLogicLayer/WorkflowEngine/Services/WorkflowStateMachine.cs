using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public interface IWorkflowStateMachine
    {

    }

    public class WorkflowStateMachine
    {
        private readonly AppDbContext _dbContext;

        public WorkflowStateMachine(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AdvanceAsync(Guid instanceId, string trigger)
        {
            //var instance = await _dbContext.Set<WorkflowInstance>()
            //    .Include(w => w.CurrentWorkflowNode)
            //        .ThenInclude(n => n.Workflow)
            //        .ThenInclude(wf => wf.WorkflowEdges)
            //    .FirstOrDefaultAsync(w => w.ObjectId == instanceId);

            //if (instance == null)
            //    throw new Exception("Workflow instance not found.");

            //var possibleEdges = instance.Workflow.Edges
            //    .Where(e => e.SourceNodeId == instance.CurrentNode.ReactFlowId && e.Label == trigger)
            //    .ToList();

            //if (!possibleEdges.Any())
            //    throw new Exception("Invalid transition for this trigger.");

            //// For now, assume single possible transition
            //var transition = possibleEdges.First();

            //var nextNode = await _dbContext.WorkflowNodes
            //    .FirstOrDefaultAsync(n => n.ReactFlowId == transition.TargetNodeId);

            //if (nextNode == null)
            //    throw new Exception("Target node not found.");

            //instance.CurrentNodeId = nextNode.Id;

            //// Optional: Auto complete if no outgoing transitions
            //bool isFinalStep = !instance.Workflow.Edges.Any(e => e.SourceNodeId == nextNode.ReactFlowId);
            //if (isFinalStep)
            //    instance.Status = "Completed";

            //await _dbContext.SaveChangesAsync();
        }
    }
}
