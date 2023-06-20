using System.Net.Http.Json;
using System.Text.Json;

namespace blazor_web_assembly_course;

public class ProductService : IProductService {
    private readonly HttpClient Client;
    private readonly JsonSerializerOptions Options;

    public ProductService(HttpClient httpClient, JsonSerializerOptions optionsJson) {
        Client = httpClient;
        Options = optionsJson;
    }

    public async Task<List<Product>?> Get() {
        var response = await Client.GetAsync("v1/products");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<List<Product>>(content, Options);
    }

    public async Task Add(Product product) {
        var response = await Client.PostAsync("v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException(content);
    }

    public async Task Delete(int productId) {
        var response = await Client.DeleteAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException(content);
    }
}

public interface IProductService {
    Task<List<Product>> Get();
    Task Add(Product product);
    Task Delete(int productId);
}