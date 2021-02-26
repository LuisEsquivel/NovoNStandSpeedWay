using System;
using System.IO;
using System.Text;
using Api.AutoMapper;
using Api.Helpers;
using Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Api
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

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            /*Generate Token*/
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };

                    });


            services.AddControllers();

            services.AddAutoMapper(typeof(AutoMappers));


            /*Documentation*/


            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("ApiActivos", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Api NovoNStandSpeedWay",
                    Version = "v1",

                });


                //File Comments Documentation
                var PathFileCommentsDocumentation = Path.Combine(AppContext.BaseDirectory, "ApiDocumentation.xml");
                options.IncludeXmlComments(PathFileCommentsDocumentation);

            });



            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("ApiRoles", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Api NovoNStandSpeedWay",
                    Version = "v1",

                });


                //File Comments Documentation
                var PathFileCommentsDocumentation = Path.Combine(AppContext.BaseDirectory, "ApiDocumentation.xml");
                options.IncludeXmlComments(PathFileCommentsDocumentation);

            });




            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("ApiUsuarios", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Api NovoNStandSpeedWay",
                    Version = "v1",

                });


                var PathFileCommentsDocumentation = Path.Combine(AppContext.BaseDirectory, "ApiDocumentation.xml");
                options.IncludeXmlComments(PathFileCommentsDocumentation);

            });


            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("ApiLectores", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Api NovoNStandSpeedWay",
                    Version = "v1",

                });


                var PathFileCommentsDocumentation = Path.Combine(AppContext.BaseDirectory, "ApiDocumentation.xml");
                options.IncludeXmlComments(PathFileCommentsDocumentation);

            });


            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("ApiUbicaciones", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Api NovoNStandSpeedWay",
                    Version = "v1",

                });


                var PathFileCommentsDocumentation = Path.Combine(AppContext.BaseDirectory, "ApiDocumentation.xml");
                options.IncludeXmlComments(PathFileCommentsDocumentation);

            });



            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("ApiFormaAdquisicion", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Api NovoNStandSpeedWay",
                    Version = "v1",

                });


                var PathFileCommentsDocumentation = Path.Combine(AppContext.BaseDirectory, "ApiDocumentation.xml");
                options.IncludeXmlComments(PathFileCommentsDocumentation);

            });



            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("ApiCentroDeCostos", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Api NovoNStandSpeedWay",
                    Version = "v1",

                });


                var PathFileCommentsDocumentation = Path.Combine(AppContext.BaseDirectory, "ApiDocumentation.xml");
                options.IncludeXmlComments(PathFileCommentsDocumentation);

            });

            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("ApiEventos", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Api NovoNStandSpeedWay",
                    Version = "v1",

                });


                var PathFileCommentsDocumentation = Path.Combine(AppContext.BaseDirectory, "ApiDocumentation.xml");
                options.IncludeXmlComments(PathFileCommentsDocumentation);

            });


            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("ApiEpcProductosRel", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Api NovoNStandSpeedWay",
                    Version = "v1",

                });


                var PathFileCommentsDocumentation = Path.Combine(AppContext.BaseDirectory, "ApiDocumentation.xml");
                options.IncludeXmlComments(PathFileCommentsDocumentation);

            });


            /*End Documentation*/


            services.AddCors();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                /*Soporte para CORS*/
                app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                IdentityModelEventSource.ShowPII = true;
            }
            else
            {
                /*Exceptions in production*/
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;

                    var result = JsonConvert.SerializeObject(new { error = exception.Message });
                    context.Response.ContentType = "application/json";

                    var response = new Response();
                    await context.Response.WriteAsync((string)response.ResponseValues(context.Response.StatusCode, null, result.ToString()));
                }));
            }

            /*Documentation*/
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/ApiActivos/swagger.json", "Api Activos");
                options.SwaggerEndpoint("/swagger/ApiRoles/swagger.json", "Api Roles");
                options.SwaggerEndpoint("/swagger/ApiUsuarios/swagger.json", "Api Usuarios");
                options.SwaggerEndpoint("/swagger/ApiLectores/swagger.json", "Api Lectores");
                options.SwaggerEndpoint("/swagger/ApiUbicaciones/swagger.json", "Api Ubicaciones");
                options.SwaggerEndpoint("/swagger/ApiFormaAdquisicion/swagger.json", "Api Forma Adquisición");
                options.SwaggerEndpoint("/swagger/ApiCentroDeCostos/swagger.json", "Api Centro De Costos");
                options.SwaggerEndpoint("/swagger/ApiEventos/swagger.json", "Api Eventos");
                options.SwaggerEndpoint("/swagger/ApiEpcProductosRel/swagger.json", "Api ApiEpcProductosRel");

                options.RoutePrefix = "";
            });
            /*End Documentation*/


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            /*Soporte para CORS*/
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
    }
}
