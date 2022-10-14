using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.OpenApi.Models;

using BL;
using API.Middleware;

#nullable disable

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// public Startup(IConfiguration configuration)
		// {
		// 	string path = Directory.GetCurrentDirectory(); // Components/UI/API

		// 	var builder = new ConfigurationBuilder()
		// 		.SetBasePath(Directory.GetParent(path).ToString()) // Components/UI
		// 		.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
		// 	configuration = builder.Build();

		// 	Configuration = configuration;
		// }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDistributedMemoryCache();

			services.AddCors(options =>
			{
			options.AddPolicy("mycors", builder =>
			{
			builder
			.WithOrigins("http://localhost:3000") // путь к нашему SPA клиенту
			.AllowCredentials()
			.WithMethods("POST", "GET", "DELETE")
			.AllowAnyHeader();
			});

			});

			// services.AddSession(options =>
			// {
			// 	// По умолчанию 20 минут
			// 	// options.IdleTimeout = TimeSpan.FromSeconds(10); 
			// 	options.Cookie.HttpOnly = true;
			// 	options.Cookie.IsEssential = true;
			// });


			#region Swagger Configuration
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 6.0 Web API"
                });
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
            #endregion

			#region Authentication
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
				// options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
                };
            });
            #endregion


            // // // установка конфигурации подключения
            // // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            // //     .AddCookie(options => //CookieAuthenticationOptions
            // //     {
            // //         options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/api/v1/Authorization/login");
            // //     });
				
			services.AddControllersWithViews();

			// // http://localhost:5004/swagger/index.html
		    // services.AddSwaggerGen();

			//// services.AddControllers();

			//// // Register the Swagger generator, defining 1 or more Swagger documents
			//// services.AddSwaggerGen(c =>
			//// {
			//// 	c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });                
			//// });

			// var connStr = Configuration["ConnectionStringsPostgres:UNAUTHORIZED"];
			// services.AddDbContext<DB.ApplicationContext>(option => option.UseNpgsql(connStr));
			services.AddDbContext<DB.ApplicationContext>(option => option.UseNpgsql(
				Connection.GetConnectionString(Configuration, Connection.DBMS.Postgres, Permissions.UNAUTHORIZED))
				);
			
			AddTransients(services);

			services.AddSingleton<IConfiguration>(Configuration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
		

			app.UseCors("mycors");

			// app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();    // аутентификация
			app.UseAuthorization();     // авторизация

			// app.UseSession();

			app.UseMiddleware<JWTMiddleware>();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
    		// app.UseSwaggerUI();

			// app.UseSwagger(c => c.RouteTemplate = "api/v1");

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
			app.UseSwaggerUI(c =>
			{
				// http://localhost:5004/swagger/v1/swagger.json
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
				//c.RoutePrefix = "docs";
			});

			// app.UseSwagger(c =>
			// 	{
			// 		c.RouteTemplate = "swagger/{documentName}/swagger.json";
			// 		c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
			// 		{
			// 			swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://localhost:5004/api/v1/" } };
			// 		});
			// 	});

			// app.UseSwaggerUI();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				// If you want check old route
				// endpoints.MapControllerRoute(
				// 	name: "default",
				// 	pattern: "{controller=Home}/{action=Index}/{id?}");
			});
			

			// app.UseEndpoints(endpoints =>
			// {
			// 	endpoints.MapControllerRoute(
			// 		name: "default",
			// 		pattern: "{controller=Home}/{action=Index}/{id?}");
			// });
		}

		private void AddTransients(IServiceCollection services)
		{
			services.AddTransient<BL.IUsersRepository, DB.UsersRepository>();
			services.AddTransient<BL.IComingsRepository, DB.ComingsRepository>();
			services.AddTransient<BL.IDeparturesRepository, DB.DeparturesRepository>();
			services.AddTransient<BL.ICarsRepository, DB.CarsRepository>();
			services.AddTransient<BL.ILinksOwnerCarDepartureRepository, DB.LinksOwnerCarDepartureRepository>();

			services.AddTransient<BL.IRepositoriesFactory, DB.RepositoriesFactory>();

			services.AddTransient(x => 
				new BL.Facade(x.GetRequiredService<BL.IRepositoriesFactory>()));
		}
    }
}