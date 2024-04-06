using InnovationApi.Context;
using Microsoft.AspNetCore.Mvc;
using InnovationApi.Entities;
using Microsoft.EntityFrameworkCore;
using Task = InnovationApi.Entities.Task;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InnovationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly InnovationContext _context;
        public TaskController(InnovationContext context)
        {
            _context = context;
        }
        [HttpGet("GetAll/{IdGoal:Guid}")]
        public async Task<ActionResult<IList<Task>>> GetAll(Guid IdGoal)
        {
            try
            {
                var tasks = await _context.Tasks.Where(d => d.GoalId == IdGoal).ToListAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Task>> Get(Guid id)
        {
            try
            {
                var task = await _context.Tasks.Where(d => d.Id == id).FirstOrDefaultAsync();
                if (task != null)
                {
                    return Ok(task);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Task task)
        {
            try
            {
                if (task.Id == Guid.Empty)
                {
                    task.Id = Guid.NewGuid();
                    task.Date = DateTime.Now;
                    task.Priority = false;
                    task.Status = "Incompleted";
                    _context.Add(task);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Task task)
        {
            try
            {
                var data = _context.Tasks.Where(d => d.Id == task.Id).FirstOrDefault();
                if (data == null) return NotFound();

                data.Name = task.Name;

                _context.Update<Task>(data);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        [HttpPost("CompletTask")]
        public async Task<ActionResult> CompletTask(List<Guid> completeItems)
        {
            try
            {
                await _context.Tasks.Where(d=>completeItems.Contains(d.Id)).ExecuteUpdateAsync(s =>
                
                    s.SetProperty(u => u.Status, "Completed")
                );

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("DeleteAll")]
        public async Task<ActionResult> DeleteAll(List<Guid> deleteItems)
        {
            try
            {
                var tasks = await _context.Tasks.Where(d => deleteItems.Contains(d.Id)).ToListAsync();
                _context.Tasks.RemoveRange(tasks);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}
