using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAccounting.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeCategoriesController : ControllerBase
    {
        public IActionResult Index()
        {
            return null;
        }
    }
}
