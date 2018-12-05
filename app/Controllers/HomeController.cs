using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenHackApp.Models;
using MongoDB.Driver;

namespace OpenHackApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
              var settings = new MongoClientSettings
            {
                Servers = new[]
           {
                new MongoServerAddress("mongo-0.mongo.default.svc.cluster.local", 27017),
                new MongoServerAddress("mongo-1.mongo.default.svc.cluster.local", 27017),
                new MongoServerAddress("mongo-2.mongo.default.svc.cluster.local", 27017)
            },
                ConnectionMode = ConnectionMode.ReplicaSet,
                ReplicaSetName = "rs0"
            };

            var client = new MongoClient(settings);
            var database = client.GetDatabase("SampleDb");
            var collection = database.GetCollection<User>("User");
            var result = collection.Find(_ => true);
            return View(result.ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
