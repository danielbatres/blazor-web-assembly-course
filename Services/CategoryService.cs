using System.Text.Json;

namespace blazor_web_assembly_course;

public class CategoryService : ICategoryService {
    private readonly HttpClient Client;
    private readonly JsonSerializerOptions Options;

    public CategoryService(HttpClient client) {
        Client = client;
        Options = new JsonSerializerOptions { 
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<Category>?> Get() {
        var response = await Client.GetAsync("v1/categories");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<List<Category>>(content, Options);
    }
}

public interface ICategoryService {
    Task<List<Category>> Get();
}