using Pos.DataTransfer;
using Proj.Util;
using Proj.Util.Tabular;

namespace Pos.DataAccess;

using ProductTypeResult = DataResult<List<ProductTypeDto>>;

public class ProductTypeDataAx : SimplifiedResultReturn
{
    PostgreHelper ph;

    public ProductTypeDataAx(PostgreHelper pph)
    {
        ph = pph;
    }


    public async Task<ProductTypeResult> SelectAsync(string search, string sortMode)
    {
        var args = ph.Args(
                (ArgName.Search, search),
                (ArgName.SortMode, sortMode)
            );

        try
        {
            var list = await ph.CallFuncListAsync(
                StoredFunc.ProductType_Select, 
                args, 
                reader => new ProductTypeDto {
                    ProductTypeId = reader.GetInt32(reader.GetOrdinal(ColName.ProductTypeId)),
                    Name = Turn.String(reader[ColName.Name]),
                    Details = Turn.String(reader[ColName.Details]),
                    SortOrder = Turn.Int(reader[ColName.SortOrder]),
                    RowVersion = Turn.String(reader[ColName.RowVersion]),
                    StatId = Turn.Int(reader[ColName.StatId]),
                    StatName = Turn.String(reader[ColName.StatName]),
                });

            return Ok(list);        
        }
        catch (Exception ex)
        {
            return Fail(new(), ex.Message);
        }
    }

}

