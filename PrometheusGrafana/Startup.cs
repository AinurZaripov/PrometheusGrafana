using App.Metrics;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace PrometheusGrafana
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
      //services.Configure<KestrelServerOptionsSystemdExtensions>(options => { options.AllowSynchronousIO = true})
      //services.AddSingleton<IDbConnectionFactory>(x => new SqlCeConnectionFactory(Configuration.GetValue<string>("Database:DbLocation"), x.GetRequiredService<IMetrics>()));
      services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
      .AddCertificate();
      services.AddMetrics();
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrometheusGrafana", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseAuthentication();
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PrometheusGrafana v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.Use((ctx, next) =>
      {
        ctx.Response.Headers.Add("Access-Control-Expose-Headers", "*");
        if (ctx.Request.Method.ToLower() == "options")
        {
          ctx.Response.StatusCode = 204;

          return Task.CompletedTask;
        }
        return next();
      });
    }
  }
}
