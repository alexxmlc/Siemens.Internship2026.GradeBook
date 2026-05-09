using System.Reflection.Emit;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Services;

public class GradeService
{
    private readonly IGradeReader _reader;

    public GradeService(IGradeReader reader)
    {
        _reader = reader;
    }

    public async Task<GradeDto> GetAll()
    {
        var grades = await _reader.GetAllAsync();
        var gradeList = grades.ToList();

        var totalCount = gradeList.Count;
        var averageValue = gradeList.Any() ? gradeList.Average(i => i.Value) : 0;

        Console.WriteLine($"[LOG] Returning {totalCount} grades, average value: {averageValue}");

        return new GradeDto
        {
            Data = gradeList,
            Statistics = new StatisticsDto
            {
                TotalCount = totalCount,
                AverageValue = averageValue,
                RetrievedAt = DateTime.UtcNow
            }
        };
    }

    public async Task<Grade?> GetById(int id)
    {
        return await _reader.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Grade>> FilterPassingGradesAsync(int n)
    {
        var allGrades = await _reader.GetAllAsync();

        var filteredGrades = allGrades.Where(i => i.IsActive && i.Value >= 5).Take(n);

        return filteredGrades;
    } 
}