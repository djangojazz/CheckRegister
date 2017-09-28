using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetMVCCheckRegister.Controllers
{
  [Route("api/checkRegister")]
  public class CheckRegisterController : ApiController
  {
    //[HttpGet("")]
    //public IActionResult Get(string userName)
    //{
    //  try
    //  {
    //    var trip = _repository.GetTripByName(tripName, User.Identity.Name);

    //    return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
    //  }
    //  catch (Exception ex)
    //  {
    //    _logger.LogError("Failed to get stops: {0}", ex);
    //  }

    //  return BadRequest("Failed to get stops");
    //}

    // GET api/values
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/values/5
    public string Get(int id)
    {
      return "value";
    }

    // POST api/values
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    public void Delete(int id)
    {
    }
  }
}
