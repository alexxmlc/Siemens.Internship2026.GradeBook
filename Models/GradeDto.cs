namespace Siemens.Internship2026.GradeBook.Models;

public class GradeDto
{
    public IEnumerable<Grade> Data { get; set; } = new List<Grade>();
    public StatisticsDto Statistics { get; set; } = new StatisticsDto();
}

public class StatisticsDto
{
    public int TotalCount { get; set; }
    public decimal AverageValue{ get; set; }
    public DateTime RetrievedAt{ get; set; }
}