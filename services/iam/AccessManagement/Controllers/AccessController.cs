using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.Controllers;

public class AccessController : BaseController
{
    [HttpGet("[action]")]
    public async Task<IActionResult> Authorize()
    {
        throw new NotImplementedException();
    }
}