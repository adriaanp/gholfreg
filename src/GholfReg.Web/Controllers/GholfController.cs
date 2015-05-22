using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using GholfReg.Domain;
using GholfReg.Domain.Data;
using Microsoft.Framework.Logging;

namespace GholfReg.Web.Controllers
{
    [Route("/api")]
    public class GholfController: BaseController
    {
        public GholfController()
        {
        }

        [HttpGet("day")]
        public IActionResult GetAllDays()
        {
            return new ObjectResult(MockData.GolfDays);
        }

        [HttpGet("day/{id:Guid}")]
        public IActionResult GetGolfDay(Guid id)
        {
            var day = MockData.GolfDays.FirstOrDefault(days => days.Id == id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(day);
        }

        [HttpPost("day")]
        public IActionResult SaveGolfDay([FromBody] GolfDay golfDay)
        {
            //TODO: check of ModelState.IsValid
            if (golfDay == null)
            {
                return HttpBadRequest();
            }

            MockData.GolfDays.Add(golfDay);
            return CreatedAtAction("SaveGolfDay", golfDay.Id);
        }

        [HttpPut("day/{id:Guid}")]
        public IActionResult SaveGolfDay(Guid id, [FromBody] GolfDay golfDay)
        {
            var dbDay = MockData.GolfDays.FirstOrDefault(x => x.Id == id);
            if (dbDay == null)
            {
                return HttpNotFound();
            }

            MockData.GolfDays.Remove(dbDay);
            golfDay.Id = dbDay.Id;
            MockData.GolfDays.Add(golfDay);
            return new NoContentResult();
        }

        [HttpDelete("day/{id:Guid}")]
        public IActionResult DeleteGolfDay(Guid id)
        {
            var day = MockData.GolfDays.FirstOrDefault(g => g.Id == id);
            if (day == null)
            {
                return HttpNotFound();
            }
            MockData.GolfDays.Remove(day);
            return new NoContentResult();
        }
    }
}
