using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using CategoriseApi.Dtos;
using CategoriseApi.Helpers;
using CategoriseApi.Models;
using CategoriseApi.Services;

namespace CategoriseApi.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class UploadTransactionController : ControllerBase
  {
    private CategoriseContext _context;
    private TransactionUploadService _transactionUploadService;

    public UploadTransactionController(CategoriseContext context)
    { 
      _context = context;
      _transactionUploadService = new TransactionUploadService(context);
    }

    [HttpPost]
    public IActionResult UploadCsv([FromBody]string csvB64)
    {
      try
      {
        _transactionUploadService.UploadCsv(csvB64);
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}