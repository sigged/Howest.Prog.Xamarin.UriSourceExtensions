using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        [HttpGet("public")]
        public IActionResult GetPublicImages()
        {
            return Ok(new[] {
                new {
                    Description = "Pepperoni pizza for everyone!",
                    Url = "/public/pepperoni.jpg"
                },
                new {
                    Description = "Chicken pita for all",
                    Url = "/public/chickenpita.jpg"
                }
            });
        }

        [Authorize()]
        [HttpGet("protected")]
        public IActionResult GetProtectedImages()
        {
            return Ok(new[] {
                new {
                    Description = "Tacos for the authenticated",
                    Url = "/private/tacos.jpg"
                }
            });
        }


    }
}
