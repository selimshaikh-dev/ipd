using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SearchController> _logger;

        public SearchController(IUnitOfWork unitOfWork, ILogger<SearchController> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }
    }
}