using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ColorsX.Web;

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

    public async Task<ColorItem?> GetColorsRandomAsync(CancellationToken cancellationToken = default)
    {
        ColorItem? color = await httpClient.GetFromJsonAsync<ColorItem>("/colors/random", cancellationToken);

        return color;
    }


}
 

public record ColorItem(string Name, string HexCode);
