using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;

using Microsoft.AspNet.Routing;

using System.Security.Claims;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authentication.Google;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Authentication;
using Newtonsoft.Json.Linq;

using ds = GholfReg.Domain.Services;

namespace GholfReg.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDataProtection();
            // services.Configure<ExternalAuthenticationOptions>(options =>
            // {
            //     options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            // });
            // services.ConfigureClaimsTransformation(p =>
            // {
            //     var id = new ClaimsIdentity("xform");
            //     id.AddClaim(new Claim("ClaimsTransformation", "TransformAddedClaim"));
            //     p.AddIdentity(id);
            //     return p;
            // });

            services.AddMvc();

            services.AddSingleton<ds.ISession, ds.Session>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseErrorPage();

            //SetupAuth(app);
            app.UseStaticFiles();
                
            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(name: "Default", template: "{controller=Home}/{action=Index}/{id?}");
            //     //api routes
            //     //routes.MapRoute("ApiRoute", "{controller}/{id?}");
            // });
            app.UseMvc();

        }

        public void SetupAuth(IApplicationBuilder app)
        {
            app.UseCookieAuthentication(options =>
                {
                    options.LoginPath = new PathString("/login");
                });

            app.UseOAuthAuthentication("Google-AccessToken", options =>
                {
                    options.ClientId = "294926599688-28fo2akavfu58oh0887kcnmm76sqvldi.apps.googleusercontent.com";
                    options.ClientSecret = "UVFxh_qchMlJChdYBq2-ukhu";
                    options.CallbackPath = new PathString("/signin-google-token");
                    options.AuthorizationEndpoint = GoogleAuthenticationDefaults.AuthorizationEndpoint;
                    options.TokenEndpoint = GoogleAuthenticationDefaults.TokenEndpoint;
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                });

            app.UseGoogleAuthentication(options =>
                {
                    options.ClientId = "294926599688-28fo2akavfu58oh0887kcnmm76sqvldi.apps.googleusercontent.com";
                    options.ClientSecret = "UVFxh_qchMlJChdYBq2-ukhu";
                    //options.CallbackPath = new PathString("/signin-google-token");
                });

            app.Map("/login", signoutApp =>
                {
                    signoutApp.Run(async context =>
                        {
                            string authType = context.Request.Query["authscheme"];
                            if (!string.IsNullOrEmpty(authType))
                            {
                                // By default the client will be redirect back to the URL that issued the challenge (/login?authtype=foo),
                                // send them to the home page instead (/).
                                context.Response.Challenge(new AuthenticationProperties() { RedirectUri = "/" }, authType);
                                return;
                            }

                            context.Response.ContentType = "text/html";
                            await context.Response.WriteAsync("<html><body>");
                            await context.Response.WriteAsync("Choose an authentication scheme: <br>");
                            foreach (var type in context.GetAuthenticationSchemes())
                            {
                                await context.Response.WriteAsync("<a href=\"?authscheme=" + type.AuthenticationScheme + "\">" + (type.Caption ?? "(suppressed)") + "</a><br>");
                            }
                            await context.Response.WriteAsync("</body></html>");
                        });
                });

            // Sign-out to remove the user cookie.
            app.Map("/logout", signoutApp =>
                {
                    signoutApp.Run(async context =>
                        {
                            context.Response.SignOut(CookieAuthenticationDefaults.AuthenticationScheme);
                            context.Response.ContentType = "text/html";
                            await context.Response.WriteAsync("<html><body>");
                            await context.Response.WriteAsync("You have been logged out. Goodbye " + context.User.Identity.Name + "<br>");
                            await context.Response.WriteAsync("<a href=\"/\">Home</a>");
                            await context.Response.WriteAsync("</body></html>");
                        });
                });

            // Deny anonymous request beyond this point.
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

            // Display user information
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
                });

        }
    }
}