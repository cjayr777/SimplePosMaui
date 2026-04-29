using Pos.DataAccess;
using Pos.DataTransfer;
using Pos.ViewModel;
using Proj.Util;

namespace Pos.BusinessLogic;

using ProductTypeVmResult = DataResult<List<ProductTypeDto>>;

public class ProductTypeBizz
{
    ProductTypeDataAx da;

    public ProductTypeBizz(ProductTypeDataAx pda)
    {
        da = pda;
    }


    private ProductTypeDto VmToDto(ProductTypeVm vm)
    {
        ProductTypeDto dto = new();

        dto.ProductTypeId = vm.ProductTypeId;
        dto.Name = vm.Name;
        dto.Details = vm.Details;
        dto.SortOrder = vm.SortOrder;
        dto.RowVersion = vm.RowVersion;
        dto.StatId = vm.StatId;
        dto.StatName = vm.StatName;

        return dto;
    }


    public async Task<ProductTypeVmResult> GetListAsync(ProductTypeVm vm)
    {
        var result = await da.SelectAsync(Turn.String(vm.Search), Turn.String(vm.SortMode));

        if (!result.IsSuccess || result.Data == null)
        {
            return ProductTypeVmResult.Fail(new(), result.Message);
        }

        List<ProductTypeVm> list = new();

        list = result.Data.Select(dto => new ProductTypeVm
        {
            ProductTypeId = dto.ProductTypeId,
            Name = dto.Name,
            Details = dto.Details,
            SortOrder = dto.SortOrder,
            RowVersion = dto.RowVersion,
            StatId = dto.StatId,
            StatName = dto.StatName
        }).ToList();

        return ProductTypeVmResult.Ok(list, result.Message);
    }
}


