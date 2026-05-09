using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Services;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly GradeService _gradeService;

    public GradeController(GradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/grade called");
        return Ok(await _gradeService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/grade/{id} called");

        if (id <= 0)
        {
            return BadRequest("Id must be a positive integer.");
        }

        var grade = await _gradeService.GetById(id);

        if (grade == null)
        {
            return NotFound($"Item with Id {id} was not found.");
        }

        return Ok(grade);
    }

    [HttpGet("passing/{n}")]
    public async Task<IActionResult> FilterPassingGrades(int n)
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/grade/passing/{n} called");

        if (n < 0)
        {
            return BadRequest("N must be a positive integer.");
        }

        return Ok(await _gradeService.FilterPassingGradesAsync(n));
    }
}
