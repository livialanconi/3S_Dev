using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus_.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Registrar as Repositories(injeçao de dependencia)
builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();

builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();

builder.Services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();

//Adiciona Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "API de Eventos",
    Description = "Aplicação para gerenciamento de eventos",
    TermsOfService = new Uri("https://example.com/terms"),
    Contact = new OpenApiContact
    {
        Name = "Livia Lançoni",
        Email = "https://www.linkedin.com/in/l%C3%ADvia-caires-lan%C3%A7oni-97834637a/"
    },
    License = new OpenApiLicense
    {
        Name = "Exemplo de Licensa",
        Url = new Uri("https://example.com/license")
    }
});

//Usando a autenticação no Swagger
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Insira o token JW:"
});

options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
    [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
