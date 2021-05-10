using DeepakAPI.Repository.Account;
using DeepakAPI.Repository.User;
using DeepakAPI.Utility.EmailSender;
using DeepakAPI.Utility.StringConstants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepakAPI
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
            services.AddControllers();
            #region Add Services JWT, CORS etc.
            services.AddMvc().AddControllersAsServices();
            services.AddCors(options =>
            {
                options.AddPolicy("CORS", corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()

                    .WithOrigins(Configuration["OriginUrlLocal"])
                    // Apply CORS policy for any type of origin  
                    .AllowAnyMethod()
                    // Apply CORS policy for any type of http methods  
                    .AllowAnyHeader()
                    // Apply CORS policy for any headers  
                    .AllowCredentials().WithExposedHeaders("Content-Disposition"));
                // Apply CORS policy for all users  

            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Jwt:ValidIssuer"],
                   ValidAudience = Configuration["Jwt:ValidIssuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DeepakPatelDemoApi@6893")),
                   ValidateLifetime = true
               };
           });

            services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStringConstants, StringConstants>();
            #endregion


            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CORS");

            app.UseAuthentication();//impletention for middle layer in pipeline for securing APIs 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
            });
            // Enable middleware to serve generated Swagger as a JSON endpoint.  
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),  
            // specifying the Swagger JSON endpoint.  
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeepakAPI v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
