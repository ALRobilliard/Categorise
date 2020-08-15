using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CategoriseApi.Models;
using CategoriseApi.Services;
using CategoriseApi.Extensions;

namespace CategoriseApi.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class TransactionsController : ControllerBase
  {
    private CategoriseContext _context;
    private TransactionUploadService _transactionUploadService;

    public TransactionsController(CategoriseContext context)
    { 
      _context = context;
      _transactionUploadService = new TransactionUploadService(context);
    }

    [HttpPost]
    [Route("upload-csv")]
    public IActionResult UploadCsv([FromBody]string csvB64)
    {
      ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
      Guid? userId = identity.GetUserId();

      if (userId.HasValue)
      {
        try
        {
          _transactionUploadService.UploadCsv(csvB64, userId.Value);
          return NoContent();
        }
        catch (Exception ex)
        {
          return BadRequest(ex.Message);
        }
      }
      return BadRequest("User ID unable to be retrieved from token.");
    }
  }
}