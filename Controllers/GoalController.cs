using InnovationApi.Context;
using InnovationApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InnovationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly InnovationContext _context;
        public GoalController(InnovationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Goal>>> Get()
        {
            try
            {
                var goals = await _context.Goals
                    .Include(d=>d.Tasks)
                    .ToListAsync();
                return Ok(goals);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id:Guid}")]
        public async Task<ActionResult<IList<Goal>>> Get(Guid Id)
        {
            try
            {
                var goal = await _context.Goals.Where(d=>d.Id == Id)
                    .Include(d => d.Tasks)
                    .FirstOrDefaultAsync();
                return Ok(goal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Goal goal)
        {
            try
            {
                goal.Id = Guid.NewGuid();
                goal.CreatedDate = DateTime.Now;
                _context.Add<Goal>(goal);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<GoalController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Goal goal)
        {
            try
            {
                var data = await _context.Goals.Where(d => d.Id == goal.Id).FirstOrDefaultAsync();

                if (data == null) return NotFound();

                data.Name = goal.Name;

                _context.Update(data);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<GoalController>/5
        [HttpDelete("{Id:Guid}")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            try
            {
                var task = await _context.Goals.Where(d=>d.Id == Id).FirstOrDefaultAsync();
                if(task == null) return NotFound();

                _context.Remove(task);
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
