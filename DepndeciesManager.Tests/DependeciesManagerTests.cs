using System;
using System.Collections.Generic;
using DependeciesManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using static DependeciesManager.Controllers.DependeciesManagerController;

namespace DepndeciesManager.Tests
{
	public class DependeciesManagerTests
	{

		[Fact]
		public void RegisterCircularDependencyTest0()
		{

			PostRq req = new PostRq();
			req.item = "A";
			req.deps = new HashSet<String> { "A" };

			var controller = new DependeciesManagerController();
			Assert.IsType<BadRequestResult>(controller.RegisterDependencies(req));

		}

		[Fact]
		public void RegisterCircularDependencyTest1()
		{
			PostRq req = new PostRq();
			req.item = "A";
			req.deps = new HashSet<String> { "B" };

			PostRq req1 = new PostRq();
			req1.item = "B";
			req1.deps = new HashSet<String> { "A" };

			var controller = new DependeciesManagerController();
			controller.RegisterDependencies(req);

			Assert.IsType<BadRequestResult>(controller.RegisterDependencies(req1));

		}


		[Fact]
		public void GetSimpleDependencyTestNestedLevels()
		{

			PostRq req = new PostRq();
			req.item = "D";
			req.deps = new HashSet<String> { "F" };

			var controller = new DependeciesManagerController();

			controller.RegisterDependencies(req);
			
			Assert.Equal(controller.GetDependencies("D"), new HashSet<String> { "F" });
		}


		[Fact]
		public void GetEmptyDependencyTest()
		{
			var controller = new DependeciesManagerController();

			Assert.Equal(new HashSet<String> {}, controller.GetDependencies("@"));
		}


		[Fact]
		public void GetComplexDependencyTest()
		{

			PostRq req1 = new PostRq();
			req1.item = "D";
			req1.deps = new HashSet<String> { "F" };

			PostRq req2 = new PostRq();
			req2.item = "A";
			req2.deps = new HashSet<String> { "B", "C", "D" };

			PostRq req3 = new PostRq();
			req3.item = "B";
			req3.deps = new HashSet<String> { "E", "C" };

			var controller = new DependeciesManagerController();

			controller.RegisterDependencies(req1);
			controller.RegisterDependencies(req2);
			controller.RegisterDependencies(req3);

			Assert.Equal(controller.GetDependencies("A"), new HashSet<String> { "B", "C", "D", "E", "F" });
			Assert.Equal(controller.GetDependencies("D"), new HashSet<String> { "F" });
			Assert.Equal(controller.GetDependencies("F"), new HashSet<String> { });
		}
	}
}
