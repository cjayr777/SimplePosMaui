using Pos.DataAccess;
using Pos.DataTransfer;
using Pos.ViewModel;
using Proj.Util;

namespace Pos.BusinessLogic;

using ProductTypeVmResult = DataResult<List<ProductTypeVm>>;

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

    private ProductTypeVm DtoToVm(ProductTypeDto dto)
    {
        ProductTypeVm vm = new();

        vm.ProductTypeId = dto.ProductTypeId;
        vm.Name = dto.Name;
        vm.Details = dto.Details;
        vm.SortOrder = dto.SortOrder;
        vm.RowVersion = dto.RowVersion;
        vm.StatId = dto.StatId;
        vm.StatName = dto.StatName;

        return vm;
    }


    public async Task<ProductTypeVmResult> GetListAsync(ProductTypeVm vm)
    {
        var result = await da.SelectAsync( Turn.Int(vm.ProductTypeId), Turn.String(vm.Search), Turn.String(vm.SortMode));

        if (!result.IsSuccess || result.Data == null)
        {
            return ProductTypeVmResult.Fail(new(), result.Message);
        }

        List<ProductTypeVm> list = new();

        list = result.Data.Select(DtoToVm).ToList();

        return ProductTypeVmResult.Ok(list, result.Message);
    }
}


