using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GholfReg.Controllers
{
	public class HomeController: Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}