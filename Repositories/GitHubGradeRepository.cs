using System.Text.Json;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class GitHubGradeRepository : IGradeReader
{
    private readonly HttpClient _httpClient;
    private readonly String _url = "https://gist.githubusercontent.com/ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw/145b121103dd1…";

    public GitHubGradeRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Grade>> GetAllAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"[LOG] API Error: {response.StatusCode}. The link can't be found or isn't working.");
                return new List<Grade>();
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var grades = JsonSerializer.Deserialize<IEnumerable<Grade>>(jsonString, options);

            return grades?.Where(g => g.IsActive) ?? new List<Grade>();
        }catch (Exception ex)
        {
            Console.WriteLine($"[LOG] Exeption when fetching data from GitHub: {ex.Message}");
            return new List<Grade>();
        }
    }

    public async Task<Grade?> GetByIdAsync(int id)
    {
        var allGrades = await GetAllAsync();
        return allGrades.FirstOrDefault(g => g.Id == id);
    }
}
