using System.Net;
using Api.Data;
using Api.Model;
using Api.ModelDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class Authcontroller : StoreController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Authcontroller(AppDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) : base(dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            if (registerRequestDto == null)
            {
                return BadRequest(new ResponseServer
                {
                   IsSuccess = false,
                   HttpStatus = HttpStatusCode.BadRequest,
                   ErrorMessages = {"Некорректная модель запроса"}
                });
            }

            var userFromDb = await DbContext.AppUsers.FirstOrDefaultAsync(x => x.Email.ToLower() == registerRequestDto.Email.ToLower());

            if (userFromDb != null)
            {
                return BadRequest(new ResponseServer
                {
                   IsSuccess = false,
                   HttpStatus = HttpStatusCode.BadRequest,
                   ErrorMessages = {$"Такой пользователь есть {userFromDb.Email}"}
                });
            }

            var newAppUser = new AppUser{
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.Email,
                NormalizedEmail = registerRequestDto.Email.ToUpper(),
                FirstName = registerRequestDto.UserName
                };
            
            var result = await _userManager.CreateAsync(newAppUser,registerRequestDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ResponseServer
                {
                    IsSuccess = false,
                    HttpStatus = HttpStatusCode.BadRequest,
                    ErrorMessages = {"Ошибка регестрацыи"}
                });
            }

            // var newRoleAppUser = registerRequestDto.Role.Equals(SharedData.Roles.Consumer, StringComparison.OrdinalIgnoreCase);

            await _userManager.AddToRoleAsync(newAppUser, "consumer");

            return Ok(new ResponseServer{
                HttpStatus = HttpStatusCode.OK,
                Result = "Регистрацыя завершина"
            });
        }
    }
}