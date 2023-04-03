using System.Text;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddControllers();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:5112", "https://localhost:7293").AllowAnyHeader().AllowAnyMethod()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Token Dogrulama Islemleri
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("User",options =>
{
    options.TokenValidationParameters =
        new() //Dogrulanacak tokenin parametrelirini iceren TokenValidationParameters nesnesini ve dogrulanmasini istedigimiz parametreleri tanimliyoruz.
        {
            ValidateIssuer =
                true, // Olusuturulacak token degerini kimlerin, originlerin, sitelerin kullanacagini belirledigimiz alandir.
            ValidateAudience =
                true, // Tokeni kimin dagittigini ifade ettigimiz alandir yani bu API yi nerede kullanicaksak oranin alanini giriyoruz.
                     // todo Olusturulan token degerinin suresini kontrol edecek olan dogrulamayi ekleyebilirsin.
            ValidateIssuerSigningKey =
                true, // Uretilecek token degerinin uygulamamiza ait bir deger oldugunu ifade eden bir key verisinin dogrulamasidir. Bizim belirledigmiz bize ait olan degerdir.

            ValidAudience = builder.Configuration["TokenOptions:Audience"],
            ValidIssuer = builder.Configuration["TokenOptions:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenOptions:SecurityKey"]))//Bu APIye ait olacak olan signinkeyi sifreleyerek veriyoruz.
        };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();//wwwroot dizinini kullanabilmek icin.

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();