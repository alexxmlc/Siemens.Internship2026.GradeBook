using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class ItemRepository : IGradeReader
{
    protected readonly List<Grade> _grades = new();
    protected int _nextId = 1;

    public virtual Task<Grade?> GetByIdAsync(int id)
    {
        var grade = _grades.FirstOrDefault(i => i.Id == id && i.IsActive);
        return Task.FromResult(grade);
    }

    public virtual Task<IEnumerable<Grade>> GetAllAsync()
    {
        var grades = _grades.Where(i => i.IsActive).AsEnumerable();
        return Task.FromResult(grades);
    }
}
