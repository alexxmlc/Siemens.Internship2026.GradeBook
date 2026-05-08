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
        var items = await _reader.GetAllAsync();
        var itemList = items.ToList();

        var totalCount = itemList.Count;
        var averageValue = itemList.Any() ? itemList.Average(i => i.Value) : 0;

        Console.WriteLine($"[LOG] Returning {totalCount} items, average value: {averageValue}");

        return new GradeDto
        {
            Data = itemList,
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
}