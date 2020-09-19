using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CategoriseApi.Models;
using CategoriseApi.Services;
using CategoriseApi.Extensions;

namespace CategoriseApi.Controllers
{
    /// <summary>
    /// This controller exposes CRUD actions for the Transactions table.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private CategoriseContext _context;
        private TransactionUploadService _transactionUploadService;

        /// <summary>
        /// Constructor for the TransactionsController.
        /// </summary>
        public TransactionsController(CategoriseContext context)
        {
            _context = context;
            _transactionUploadService = new TransactionUploadService(context);
        }

        /// <summary>
        /// Endpoint for bulk uploading transactions from a formatted CSV file.
        /// </summary>
        /// <param name="csvB64">Base64 string of the input CSV file.</param>
        [HttpPost]
        [Route("upload-csv")]
        public IActionResult UploadCsv([FromBody] string csvB64)
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