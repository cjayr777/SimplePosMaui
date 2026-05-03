using Pos.ViewModel;
using Proj.Util;
using System.Net.Http.Json;

namespace Pos.MauiView.Service;

using ProductTypeVmResult = DataResult<List<ProductTypeVm>>;

public class ProductTypeService
{
    private readonly HttpClient _http;

    public ProductTypeService(HttpClient http)
    {
        _http = http;
    }


    public async Task<ProductTypeVmResult> GetProductTypesAsync(ProductTypeVm vm)
    {
        // Encode the search string to handle spaces/special characters
        var search = Uri.EscapeDataString(vm.Search ?? "");
        var sort = Uri.EscapeDataString(vm.SortMode ?? "");

        // Reconstruct the VM manually to pa

        var response = await _http.GetAsync($"api/ProductTypeApi/select?search={search}&sortMode={sort}");

        // Try to read the DataResult even if it's a 400 error
        var result = await response.Content.ReadFromJsonAsync<ProductTypeVmResult>();

        if (response.IsSuccessStatusCode) return result;

        // If it failed, return the message sent by the DataAccess layer
        return ProductTypeVmResult.Fail(new(), result?.Message ?? "Unknown API Error");
    }

}

