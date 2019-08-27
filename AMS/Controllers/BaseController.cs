using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        protected readonly ILogger<BaseController> _logger;
        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("{OperationUserID}", UserId);
            base.OnActionExecuted(context);
        }
        /// <summary>
        /// 当前Token中包含的用户ID
        /// </summary>
        protected internal long UserId
        {
            get
            {
                if (long.TryParse(User.FindFirst(x => x.Type == ClaimTypes.PrimarySid)?.Value, out long result))
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
