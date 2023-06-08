using System.Net.Http.Json;
using System.Text.Json;

namespace blazor_web_assembly_course;

public class ProductService {
    private readonly HttpClient Client;
    private readonly JsonSerializerOptions Options;

    public ProductService(HttpClient httpClient, JsonSerializerOptions optionsJson) {
        Client = httpClient;
        Options = optionsJson;
    }

    public async Task<List<Product>?> Get() {
        var response = await Client.GetAsync("v1/products");

        return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
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