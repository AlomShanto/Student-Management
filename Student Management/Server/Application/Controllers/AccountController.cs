using System;
using Microsoft.AspNetCore.Mvc;
using Server.BusinessLogic.ILogics;
using Server.Contracts.Models;

namespace Server.Application.Controllers;

[Controller]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountLogic _accountLogic;

    public AccountController(IAccountLogic accountLogic){
        _accountLogic = accountLogic;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]UserForm user){
        try
        {
            await _accountLogic.CreateUser(user);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers([FromQuery]string role)
    {
        return Ok(await _accountLogic.GetUsers(role));
    }
}
