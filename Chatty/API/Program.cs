using System.Globalization;
using API.Extensions;
using API.Hubs;
using Application.Middleware;

var clutoreInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = clutoreInfo;
CultureInfo.DefaultThreadCurrentUICulture = clutoreInfo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<WebSocketsMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chat");

app.Run();
