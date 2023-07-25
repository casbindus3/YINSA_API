using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApiYINSA.Database;
using WebApiYINSA.Models;
using WebApiYINSA.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions( x =>
	x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
	c =>
	{
		c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
		{
			Name = "Authorization",
			Type = SecuritySchemeType.ApiKey,
			Scheme = "Bearer",
			BearerFormat = "JWT",
			In = ParameterLocation.Header
		});
		c.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type=ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				},
				new string[]{}
			}
		});
	}); 
builder.Services.AddTransient<IProductosService, ProductosService>();
builder.Services.AddTransient<IDBConnection,DBConnection>();
builder.Services.AddTransient<IDocumentosService, DocumentosService>();
builder.Services.AddTransient<ISociosService,SociosService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<HashService>();

//builder.Services.AddTransient<IUserStore<Usuario>, UsuarioStore>();//autenticacion y autorización con identity
//builder.Services.AddTransient<SignInManager<Usuario>>();//autenticacion y autorización con identity
//builder.Services.AddIdentityCore<Usuario>(opciones =>
//{
//	opciones.Password.RequireDigit = false;
//	opciones.Password.RequireLowercase = false;
//	opciones.Password.RequireUppercase = false;
//	opciones.Password.RequireNonAlphanumeric = false;
//}).AddErrorDescriber<MensajesDeErrorIdentity>();//autenticacion y autorización con identity

//UN COMENTARIO
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			   .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidateIssuer = false,
				   ValidateAudience = false,
				   ValidateLifetime = true,
				   ValidateIssuerSigningKey = true,
				   IssuerSigningKey = new SymmetricSecurityKey(
					 Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
				   ClockSkew = TimeSpan.Zero
			   });

/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidateIssuer = true, //otro lo ponen false
		ValidateAudience = true,//otro lo ponen false
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],//se puede ignorar
		ValidAudience = builder.Configuration["Jwt:Audience"],//se puede ignorar
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		//	,ClockSkew = TimeSpan.Zero
	};
}); */

builder.Services.AddAuthorization( options =>
{   //options.AddPolicy("SuperAdmin", policy => policy.RequireClaim("AdminType","Super")) 
	options.AddPolicy("AdminMaster", policy => policy.RequireClaim("rol","1"));
	options.AddPolicy("Admin", policy => policy.RequireClaim("rol", new string[] {"1","2"}));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // manejo de archivos

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
