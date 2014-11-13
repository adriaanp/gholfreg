using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MyCouch;
using MyCouch.Requests;
using System.Threading.Tasks;

namespace GholfReg.Controllers
{
	public class HomeController: Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Create()
		{
			using (var client = new MyCouchClient("http://localhost:5984/test"))
			{
				var put = await client.Entities.PutAsync(new Person() {Id="persons/2", Name = "Adriaan", Age= 35});
				var person = await client.Entities.GetAsync<Person>(put.Id);
				//Console.WriteLine(person.Content);
				//ObjectDumper.Write(person);
				return View(person.Content);
			}
		}

		public async Task<IActionResult> Detail()
		{
			using (var client = new MyCouchClient("http://localhost:5984/test"))
			{
				var person = await client.Entities.GetAsync<Person>("persons/1");
				return View("Create", person.Content);
			}
		}
	}

	public class Person
	{
		public string Id {get;set;}
		public string Name {get;set;}
		public int Age {get;set;}
	}
}