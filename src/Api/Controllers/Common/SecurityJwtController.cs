using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Common;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class SecurityJwtController : ControllerBase { }
