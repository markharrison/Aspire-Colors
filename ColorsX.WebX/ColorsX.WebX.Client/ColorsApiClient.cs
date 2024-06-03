using System.Net.Http.Json;

namespace ColorsX.WebX.Client;

public class ColorsApiClient(HttpClient httpClient)
{
    public async Task<ColorItem[]> GetColorsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<ColorItem> colors = new List<ColorItem>();

        await foreach (var color in httpClient.GetFromJsonAsAsyncEnumerable<ColorItem>("/colors", cancellationToken))
        {
            if (color != null)
            {
                colors.Add(color);
            }
        }

        return colors.ToArray();
    }

    public async Task<ColorItem> GetColorsRandomAsync(CancellationToken cancellationToken = default)
    {
        ColorItem? color = await httpClient.GetFromJsonAsync<ColorItem>("https://localhost:7471/colors/random", cancellationToken);

        return color;
    }

    public string GetBaseAddress()
    {
        return httpClient.BaseAddress?.ToString() ?? "Base address is not set";
    }




}


public record ColorItem(string Name, string HexCode);
