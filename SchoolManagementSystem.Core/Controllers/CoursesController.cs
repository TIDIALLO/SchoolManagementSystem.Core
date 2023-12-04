using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        public readonly ILogger<CoursesController> _logger;
        public CoursesController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<CoursesController>>();
        }
    }
}
