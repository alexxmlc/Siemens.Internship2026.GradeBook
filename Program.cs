using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Repositories;
using Siemens.Internship2026.GradeBook.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IGradeReader, ItemRepository>();
builder.Services.AddScoped<GradeService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
