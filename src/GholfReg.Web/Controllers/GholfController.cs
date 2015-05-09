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
        private ILogger _logger;

        public GholfController(ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger("GholfController");
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
        public void SaveGolfDay([FromBody] GolfDay golfDay)
        {
            //TODO: check of ModelState.IsValid
            if (golfDay == null)
            {
                Context.Response.StatusCode = 400;
                return;
            }
            //have to check golfDay.Id
            _logger.LogDebug(golfDay.Id.ToString());
            MockData.GolfDays.Add(golfDay);
            string url = Url.RouteUrl("GetGolfDay", new {id = golfDay.Id},
                Request.Scheme, Request.Host.ToUriComponent());

            Context.Response.StatusCode = 201;
            Context.Response.Headers["Location"] = url;
        }

        [HttpPut("day/{id:Guid}")]
        public void SaveGolfDay(Guid id, [FromBody] GolfDay golfDay)
        {
            var dbDay = MockData.GolfDays.FirstOrDefault(x => x.Id == id);
            if (dbDay == null)
            {
                Context.Response.StatusCode = 400;
                return;
            }

            MockData.GolfDays.Remove(dbDay);
            golfDay.Id = dbDay.Id;
            MockData.GolfDays.Add(golfDay);
            Context.Response.StatusCode = 201;
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
            return new HttpStatusCodeResult(204);
        }
    }
}
