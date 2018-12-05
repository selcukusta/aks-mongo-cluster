using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using OpenHackApp.Models;

namespace OpenHackApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
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
            for (int i = 0; i < 100; i++)
            {
                var user = new User { Id = ObjectId.GenerateNewId(), Name = new Bogus.Faker().Name.FullName() };
                collection.InsertOne(user);
            }

        }
    }
}
