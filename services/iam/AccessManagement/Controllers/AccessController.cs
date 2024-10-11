using System.ComponentModel.DataAnnotations;
using AccessManagement.Data;
using AccessManagement.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.Controllers;

[Route("api/v1/[controller]")]
public class AccessController(AppDbContext context, IPolicyRepository policyRepository) : BaseController
{
    private readonly AppDbContext _context = context;
    private readonly IPolicyRepository _policyRepository = policyRepository;
    
    [HttpGet("[action]")]
    public IActionResult Authorize()
    {
        throw new NotImplementedException();
    }
}