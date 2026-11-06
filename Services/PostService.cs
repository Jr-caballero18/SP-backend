using System;
using System.Text.Json;
using Backend.DTOs;

namespace Backend.Services;

public class PostService: IPostService
{
// Recibe el HttpClient configurado por DI para reutilizar su BaseAddress y gestión de vida.
private readonly HttpClient _httpClient;
public PostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<PostDto>> Get()
    {
        var result = await _httpClient.GetAsync(_httpClient.BaseAddress);
        result.EnsureSuccessStatusCode();
        var body = await result.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var post = JsonSerializer.Deserialize<IEnumerable<PostDto>>(body, options);
        // Una respuesta sin contenido deserializable es un error, no una colección exitosa vacía.
        return post ?? throw new JsonException("The posts response contained no data.");
    }
}
