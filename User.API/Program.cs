using System.Net.Http.Headers;
using User.Business;
using User.Business.Interfaces;
using User.DataAccess;
using User.DataAccess.Interfaces;
using Constants = User.API.Constants;
using DataAccessConstants = User.DataAccess.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(DataAccessConstants.Source.GitHub, httpClient =>
{
    httpClient.BaseAddress = new Uri(Constants.Connections.GitHubBaseAddress);

    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

    var githubBearerToken = builder.Configuration.GetConnectionString(Constants.Connections.GitHubBearerToken);
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Connections.Bearer, $" {githubBearerToken}");

    httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
});

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
