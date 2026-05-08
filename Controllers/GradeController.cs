using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Services;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly GradeService _gradeService;

    public GradeController(GradeService gradeService, IGradeReader reader)
    {
        _gradeService = gradeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item called");
        return Ok(await _gradeService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item/{id} called");

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
}
