using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovingTanks.Models.Classes;
using MovingTanks.Services;
using Newtonsoft.Json;

namespace MovingTanks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            string path = "field.json";
            try
            {
                using (StreamReader read = new StreamReader(path))
                {
                    string file = read.ReadToEnd();
                    var json = new
                    {
                        Field = new Field(),
                        Obstacles = new Obstacle[0],
                        Tanks = new Tank[0],
                    };

                    var res = JsonConvert.DeserializeAnonymousType(file, json);

                    State.Field = res.Field;
                    State.Speed = 5;
                    State.Objects = new FieldObjects();

                    foreach (var i in res.Obstacles)
                    {
                        State.Objects.Add(i);
                    }

                    foreach (var i in res.Tanks)
                    {
                        State.Objects.Add(i);
                    }
                }
            }
            catch(Exception ex)
            {
                State.Objects = new FieldObjects();
                State.Objects.Add(new Tank(50, 50, 50, 50));
                State.Objects.Add(new Tank(50, 50, 20, 20));
            }

            State.SetCheckPosition();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddHostedService<TimedHostedService>();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
