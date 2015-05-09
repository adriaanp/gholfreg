using Microsoft.AspNet.Mvc;
using GholfReg.Domain.Services;

namespace GholfReg.Web.Controllers
{
    public abstract class BaseController: Controller
    {
        [FromServices]
        public ISession DbSession { get; set; }
    }
}
