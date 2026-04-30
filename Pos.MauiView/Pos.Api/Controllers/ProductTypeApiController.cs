using Microsoft.AspNetCore.Mvc;
using Pos.Api.Helper;
using Pos.BusinessLogic;
using Pos.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pos.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductTypeApiController : BaseApiController
{
    #region old code

    //// GET: api/<ProductTypeApiController>
    //[HttpGet]
    //public IEnumerable<string> Get()
    //{
    //    return new string[] { "value1", "value2" };
    //}

    //// GET api/<ProductTypeApiController>/5
    //[HttpGet("{id}")]
    //public string Get(int id)
    //{
    //    return "value";
    //}

    //// POST api/<ProductTypeApiController>
    //[HttpPost]
    //public void Post([FromBody] string value)
    //{
    //}

    //// PUT api/<ProductTypeApiController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE api/<ProductTypeApiController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}

    #endregion

    ProductTypeBizz biz;


    public ProductTypeApiController(ProductTypeBizz pbiz)
    {
        biz = pbiz;
    }


    [HttpGet("select")]
    public async Task<ActionResult> GetList([FromQuery] ProductTypeVm vm)
    {
        var result = await biz.GetListAsync(vm);
        return HandleResult(result);
    }

}
