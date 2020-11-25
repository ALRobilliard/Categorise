using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Categorise.Dtos;
using Categorise.Models;
using Categorise.Services;
using Categorise.Extensions;

namespace Categorise.Controllers
{
    /// <summary>
    /// This controller exposes CRUD actions for the Account table.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private CategoriseContext _context;
        private AccountService _accountService;
        private IMapper _mapper;

        /// <summary>
        /// Constructor for the AccountsController.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public AccountsController(CategoriseContext context, IMapper mapper)
        {
            _context = context;
            _accountService = new AccountService(context);
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all Accounts for the specified user. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAccounts()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            Guid? userId = identity.GetUserId();

            if (userId.HasValue)
            {
                return Ok(_accountService.GetAccounts(userId.Value));
            }

            return BadRequest("User ID unable to be retrieved from token.");
        }

        /// <summary>
        /// Returns the specified account for the authenticated user.
        /// </summary>
        /// <param name="accountId">Unique identifier for the requested account.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetAccount(Guid accountId)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            Guid? userId = identity.GetUserId();

            if (userId.HasValue)
            {
                Account account = _accountService.GetAccountById(accountId, userId.Value);

                if (account == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<AccountDto>(account));
            }

            return BadRequest("User ID unable to be retrieved from token.");
        }

        /// <summary>
        /// Creates an account for the authenticated user.
        /// </summary>
        /// <param name="accountDto">Data transfer object for the account to be created.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateAccount(AccountDto accountDto)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            Guid? userId = identity.GetUserId();

            if (userId.HasValue)
            {
                Account account = _accountService.CreateAccount(accountDto, userId.Value);
                return Ok(_mapper.Map<AccountDto>(account));
            }

            return BadRequest("User ID unable to be retrieved from token.");
        }

        /// <summary>
        /// Updates the specified account for the authenticated user.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="accountDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateAccount(Guid accountId, AccountDto accountDto)
        {
            if (accountId != accountDto.Id) return BadRequest();

            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            Guid? userId = identity.GetUserId();

            if (userId.HasValue)
            {
                _accountService.UpdateAccount(accountDto, userId.Value);
                return Ok();
            }

            return BadRequest("User ID unable to be retrieved from token.");
        }

        /// <summary>
        /// Deletes the specified account for the authenticated user.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(Guid accountId)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            Guid? userId = identity.GetUserId();

            if (userId.HasValue)
            {
                _accountService.DeleteAccount(accountId, userId.Value);
                return Ok();
            }

            return BadRequest("User ID unable to be retrieved from token.");
        }
    }
}