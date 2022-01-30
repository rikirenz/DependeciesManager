using System;
using System.Collections.Generic;
using DependeciesManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace DependeciesManager.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DependeciesManagerController : ControllerBase
	{

		public class PostRq
		{
			public string item { get; set; }
			public HashSet<String> deps { get; set; }
		}

		[HttpPost]
		public StatusCodeResult RegisterDependencies(PostRq req)
		{
			String item = req.item;
			HashSet<String> deps = req.deps;
			DB curr_instance = DB.getInstance();
			foreach (string d in deps) {
				if (!curr_instance.db.ContainsKey(d))
				{
					curr_instance.db.Add(d, new HashSet<String>());
				}
				if (d.Equals(item) || this.GetDependencies(d).Contains(item)) {
					// Error, trying to add circular dependency
					return BadRequest();
				}
			}
			curr_instance.db[item] = deps;
			return Ok();
		}

		[HttpGet]
		public HashSet<String> GetDependencies(String item)
		{
			DB curr_instance = DB.getInstance();
			HashSet<String> results = new HashSet<String>();			
			if (String.IsNullOrEmpty(item) || !curr_instance.db.ContainsKey(item)) {
				return results;
			}

			foreach (String dep in curr_instance.db[item]) {
				results.Add(dep);
				foreach (String sub_dep in this.GetDependencies(dep)) {
					results.Add(sub_dep);
				}
			}

			return results;
		}
	}
}
