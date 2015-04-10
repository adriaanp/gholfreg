using System;
using Microsoft.AspNet.Mvc;
using GholfReg.Domain;

namespace GholfReg.Web.Controllers
{
    [Route("/api")]
    public class GholfController: BaseController
    {
        [HttpGet("day")]
        public IActionResult GetAllDays()
        {
            return new ObjectResult(DbSession.GetAll<GolfDay>());
        }

        [HttpGet("day/{id:Guid}")]
        public IActionResult GetGolfDay(Guid id)
        {
            var day = DbSession.Get<GolfDay>(id);
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
            }
            DbSession.Save(golfDay);
            string url = Url.RouteUrl("GetGolfDay", new {id = golfDay.Id},
                Request.Scheme, Request.Host.ToUriComponent());

            Context.Response.StatusCode = 201;
            Context.Response.Headers["Location"] = url;
        }

        [HttpDelete("day/{id:Guid}")]
        public IActionResult DeleteGolfDay(Guid id)
        {
            var day = DbSession.Get<GolfDay>(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            DbSession.Delete(day);
            return new HttpStatusCodeResult(204);
        }
    }
}