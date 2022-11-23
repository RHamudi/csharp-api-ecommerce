
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_ecommerce_dotnet.Data;
using api_ecommerce_dotnet.Models;
using api_ecommerce_dotnet.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api_ecommerce_dotnet.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("/users")]
        public async Task<IActionResult> GetUser(
            [FromServices] DataContext context
        )
        {
            var users = await context.Users.ToListAsync();
            if (users.Count == 0)
                return NotFound("Nenhum usuario encontrado");

            return Ok(users);
        }

        [HttpGet("/users/{id:int}")]
        public async Task<IActionResult> GetUserId(
            [FromRoute] int id,
            [FromServices] DataContext context
        )
        {
            try
            {
                var user = await context
                    .Users
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("/users")]
        public async Task<IActionResult> CreateUser(
            [FromBody] CreateUser model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = new User
            {
                Id = 0,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("/users/login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginUserViewModel model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await context.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, "O usuario não existe");

            if (user.Password != model.Password)
                return StatusCode(401, "Senha invalida");

            try
            {
                return Ok("Usuario logado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ocorreu um erro: {ex.Message}");
            }
        }
    }
}