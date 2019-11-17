using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using XrnCourse.BucketList.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using XrnCourse.BucketList.WebApi.Domain.Services;
using XrnCourse.BucketList.WebApi.Dto;
using Newtonsoft.Json;

namespace XrnCourse.BucketList.WebApi
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
            services.AddDbContext<BucketlistContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BucketlistDb")));
            services.AddScoped<BucketlistRepository>();
            services.AddScoped<UserRepository>();

            var config = new AutoMapper.MapperConfiguration(
                cfg => {
                    cfg.AddProfile(new AutoMapperProfileConfig());
                });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
