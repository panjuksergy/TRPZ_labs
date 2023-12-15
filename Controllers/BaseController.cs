using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace SparkSwim.GoodsService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    internal Guid UserId => !User.Identity.IsAuthenticated
        ? Guid.Empty
        : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
}