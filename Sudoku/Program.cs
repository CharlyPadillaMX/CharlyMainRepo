using Sudoku.Models;
using Sudoku.Models.SudokuGenerator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISudokuRepository, SudokuRepository>();

builder.Services.AddRazorPages();

builder.Services.AddControllers();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();

app.UseStaticFiles();

app.MapRazorPages();

app.MapControllers();

app.Run();
