using System.Net;
using Api.Data;
using Api.Model;
using Api.ModelDto;
using Api.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class Authcontroller : StoreController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public Authcontroller(
            AppDbContext dbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            JwtTokenGenerator jwtTokenGenerator) 
            : base(dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
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

        [HttpPost]
        public async Task<ActionResult<ResponseServer>> Login(LoginRequestDto loginRequestDto)
        {
            var userFromDb = await DbContext.AppUsers.FirstOrDefaultAsync(t => t.Email.ToLower() == loginRequestDto.Email.ToLower());
            if (userFromDb == null || !await _userManager.CheckPasswordAsync(userFromDb, loginRequestDto.Password))
            {
                return BadRequest(
                    new ResponseServer
                    {
                        IsSuccess = false,
                        HttpStatus = HttpStatusCode.BadRequest,
                        ErrorMessages = {"Что-то пошло не так"}
                        
                    }
                );
            }

            var roles = await _userManager.GetRolesAsync(userFromDb);
            var token = _jwtTokenGenerator.GenerateJwtToken(userFromDb,roles);

            return Ok(new ResponseServer
            {
               HttpStatus = HttpStatusCode.OK,
               Result = new LoginResponseDto
               {
                   Email = userFromDb.Email,
                   Token = token
               }
            });
        }
    }
}