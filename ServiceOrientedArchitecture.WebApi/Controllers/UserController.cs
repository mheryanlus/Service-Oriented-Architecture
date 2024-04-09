using Microsoft.AspNetCore.Mvc;
using ServiceOrientedArchitecture.Dtos;
using ServiceOrientedArchitecture.Common.Models;
using ServiceOrientedArchitecture.Services;

namespace ServiceOrientedArchitecture.Controllers;

[ApiController]
[Route("v1/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        try
        {
            var result = await _userService.CreateAsync(request.firstName, request.lastName);
            return Ok(result);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser([FromQuery] int userId)
    {
        await _userService.DeleteAsync(userId);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserEntityDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();

        return Ok(users);
    }
}