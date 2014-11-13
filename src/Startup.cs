using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Security;
using Microsoft.AspNet.Security.Cookies;
using Microsoft.AspNet.Security.Google;

using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Security;

namespace GholfReg
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<ExternalAuthenticationOptions>(options =>
			{
                options.SignInAsAuthenticationType = GoogleAuthenticationDefaults.AuthenticationType;
			});

			services.ConfigureGoogleAuthentication(options =>
			{
				options.ClientId = "294926599688-28fo2akavfu58oh0887kcnmm76sqvldi.apps.googleusercontent.com";
				options.ClientSecret = "UVFxh_qchMlJChdYBq2-ukhu";
			});
			services.AddMvc();

			//inject other services??
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseErrorPage()
				.UseStaticFiles()
				//.UseCookieAuthentication(options =>
				//{
				//	options.LoginPath = new PathString("/login");
				//})
				.UseGoogleAuthentication()
				.UseMvc(routes =>
				{
					routes.MapRoute(name: "Default", template: "{controller=Home}/{action=Index}/{id?}");
					//api routes
					routes.MapRoute("ApiRoute", "{controller}/{id?}");
				});
			;

			/*app.Map("/login", signoutApp =>
            {
                signoutApp.Run(async context =>
                {
                    string authType = context.Request.Query["authtype"];
                    if (!string.IsNullOrEmpty(authType))
                    {
                        // By default the client will be redirect back to the URL that issued the challenge (/login?authtype=foo),
                        // send them to the home page instead (/).
                        context.Response.Challenge(new AuthenticationProperties() { RedirectUri = "/" }, authType);
                        return;
                    }

                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("<html><body>");
                    await context.Response.WriteAsync("Choose an authentication type: <br>");
                    foreach (var type in context.GetAuthenticationTypes())
                    {
                        await context.Response.WriteAsync("<a href=\"?authtype=" + type.AuthenticationType + "\">" + (type.Caption ?? "(suppressed)") + "</a><br>");
                    }
                    await context.Response.WriteAsync("</body></html>");
                });
            });

// Sign-out to remove the user cookie.
            app.Map("/logout", signoutApp =>
            {
                signoutApp.Run(async context =>
                {
                    context.Response.SignOut(CookieAuthenticationDefaults.AuthenticationType);
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("<html><body>");
                    await context.Response.WriteAsync("You have been logged out. Goodbye " + context.User.Identity.Name + "<br>");
                    await context.Response.WriteAsync("<a href=\"/\">Home</a>");
                    await context.Response.WriteAsync("</body></html>");
                });
            });

 			app.Use(async (context, next) =>
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    // The cookie middleware will intercept this 401 and redirect to /login
                    context.Response.Challenge();
                    return;
                }
                await next();
            });

            app.Run(async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<html><body>");
                await context.Response.WriteAsync("Hello " + context.User.Identity.Name + "<br>");
                foreach (var claim in context.User.Claims)
                {
                    await context.Response.WriteAsync(claim.Type + ": " + claim.Value + "<br>");
                }
                await context.Response.WriteAsync("<a href=\"/logout\">Logout</a>");
                await context.Response.WriteAsync("</body></html>");
            });*/
			//app.UseWelcomePage();
		}
	}
}