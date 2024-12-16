using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Meetings_App.Models.DTO;
using Meetings_App.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Meetings_App.Data;

[Route("api/[controller]")]


[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly ITokenRepository tokenRepository;
    public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
    {
        this.userManager = userManager;
        this.tokenRepository = tokenRepository;

    }

    // POST: /api/Auth/Register
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        var identityUser = new IdentityUser
        {
            UserName = registerRequestDto.Name,
            Email = registerRequestDto.Email
        };

        var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

        if (identityResult.Succeeded)
        { 

                if (identityResult.Succeeded)
                {
                    return Ok("User was registered! Please login.");
                }
            
        }

        return BadRequest("Something went wrong");
    }

    // POST: /api/Auth/Login
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var user = await userManager.FindByEmailAsync(loginRequestDto.Email);
        

        if (user != null)
        {
            var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (checkPasswordResult)
            {

               //Create Token

                    var authToken = tokenRepository.CreateJWTToken(user);

                    var response = new LoginResponseDto
                    {
                        AuthToken = authToken,
                        Email = user.Email,
                       
                    };

                    return Ok(response);
                
            }
        }

        return BadRequest("Username or password incorrect");
    }
}