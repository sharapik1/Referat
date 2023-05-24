using Microsoft.AspNetCore.Mvc;
using Sharapova.data;
using Sharapova.data.mysql;
using Sharapova.model;

namespace Sharapova.Controllers;

[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    private DataProvider _provider = new MySqlDataProvider("localhost",
        "sharapova",
        "sharapova",
        "1234");

    [HttpGet]
    [Route("login")]
    public IActionResult Login(string nickname, string password)
    {
        User? user = _provider.LoadUserByNickName(nickname);
        if (user == null) return NotFound();
        if (user.password != password) return StatusCode(401);
        return Ok(user);
    }

    [HttpGet]
    [Route("reg")]
    public IActionResult Reg(string nickname, string password, string name)
    {
        if (_provider.LoadUserByNickName(nickname) != null)
            return StatusCode(600);

        User user = new User(0, nickname, name, password);
        _provider.SaveUser(user);
        return Ok(user);
    }
}