using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        public readonly ILogger<EnrollmentsController> _logger;
        public EnrollmentsController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<EnrollmentsController>>();
        }
    }
}
