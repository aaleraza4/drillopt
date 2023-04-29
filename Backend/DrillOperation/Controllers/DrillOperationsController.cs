using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrillOperation;
using DrillOperation.DTOs;
using Microsoft.OpenApi.Extensions;

namespace DrillOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrillOperationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DrillOperationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DrillOperations
        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.DrillOperation.ToListAsync());
        }

        //// POST: DrillOperations/Create
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(DrillOperationDTO drillOperation)
        {
            if (ModelState.IsValid)
            {
                var drillDbModel = new DrillOperation()
                {
                    EventName = drillOperation.EventName,
                    EndInterval = drillOperation.EndInterval,
                    StartInterval = drillOperation.StartInterval,
                    EventType =(EventEnum) drillOperation.EventType
                };
                _context.DrillOperation.Add(drillDbModel);
                await _context.SaveChangesAsync();
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet(nameof(Edit))]
        public async Task<IActionResult> Edit(int? id)
        {
            var drillOperation = await _context.DrillOperation.FindAsync(id);
            if (drillOperation == null)
            {
                return NotFound();
            }
            return Ok(drillOperation);
        }

        [HttpPost(nameof(Edit))]
        public async Task<IActionResult> Edit(DrillOperation drillOperation)
        {
            var DbModel = await _context.DrillOperation.Where(x => x.Id == drillOperation.Id).FirstOrDefaultAsync();
            if (DbModel == null)
                return Ok(false);

            DbModel.StartInterval = drillOperation.StartInterval;
            DbModel.EndInterval = drillOperation.EndInterval;
            DbModel.EventType = drillOperation.EventType;
            _context.DrillOperation.Update(DbModel);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpGet(nameof(Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.DrillOperation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DrillOperation'  is null.");
            }
            var drillOperation = await _context.DrillOperation.FindAsync(id);
            if (drillOperation != null)
            {
                _context.DrillOperation.Remove(drillOperation);
            }

            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpGet(nameof(GetChart))]
        public async Task<ActionResult<IEnumerable<BarChartDTO>>> GetChart()
        {

            var response = await
                _context.DrillOperation.Select(e => new BarChartDTO
                {
                    Label = e.EventName,
                    Stack = "a",
                    Data = new List<long>() { e.EndInterval - e.StartInterval },
                }).ToListAsync();

            return response;

        }
    }
}
