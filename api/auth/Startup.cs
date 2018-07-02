using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;

namespace auth
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserValidate, UserValidate>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            string secretKey = "encrypt_the_validate_site_key";
             var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
             var options = new TokenGenerateOption
             {
                 Path = "/token",
                 Audience = "www.duanby.com",
                 Issuer = "www.duanby.com",
                 SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                 Expiration = TimeSpan.FromMinutes(15),
             };
             var userValidate = app.ApplicationServices.GetService<IUserValidate>();
             // var userValidate = new UserValidate();

             var tokenGenerator = new TokenGenerator(options, userValidate);
             app.Map(options.Path, tokenGenerator.GenerateToken);


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }


        class UserValidate : IUserValidate
         {
             public UserModel GetUserByContext(string userName, string password)
             {
                 UserModel rct = null;
                 if (userName == "moto" && password == "111")
                 {
                     rct = new UserModel { UserName = userName, UniqueId = "1234567890" };
                 }
 
                 return rct;
             }
         }
    }
}
