using Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(builder => builder.WithOrigins("https://localhost:7171")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapHub<StickyNoteHub>("/stickynotehub");

app.Run();