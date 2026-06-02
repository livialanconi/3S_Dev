using FIlmes.WebAPI.BdContextFilme;
using FIlmes.WebAPI.Interface;
using FIlmes.WebAPI.Repositories;
using FILmes.WebAPI.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Adiciona o contexto do banco de dados(exemplo com SQl Server)

builder.Services.AddDbContext<FilmeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Adiciona o repositório ao container de injeção de dependência

builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Adiciona serviço de Jwt Bearer (forma de autenticação)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";

})
    .AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //Valida quem está solicitado
            ValidateIssuer = true,
            //Valida quem está recebendo
            ValidateAudience = true,
            //Define se o tempo de expiraçõa será validado
            ValidateLifetime = true,
            //forma de criptogreafia  valida a chave de autenticação
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),
            //Valida o tempo de expiração do token
            ClockSkew = TimeSpan.FromMinutes(5),
            //Nome do issuer (de onde está vindo)
            ValidIssuer = "api_filmes",
            //Nome de audience (para onde ele está indo)
            ValidAudience = "api_filmes"
        };
    }
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Filmes API",
        Description = "Uma API com catálogo de filmes",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "laur4moura",
            Url = new Uri("https://github.com/laur4moura")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://eample.com/license")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT"

    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {

        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });

});


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//Adiciona o serviço de controllers

builder.Services.AddControllers()
    .AddJsonOptions(options =>

    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
    });
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

}

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsPolicy");

//ADICIONA O MAPEAMENTO DE CONTROLE
app.MapControllers();

app.Run();
