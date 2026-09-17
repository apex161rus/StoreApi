using Api.Data;
using Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Api.Controllers
{
    public class ProductController : StoreController
    {
         public ProductController(AppDbContext dbContext)
            : base(dbContext)
        {
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseServer>> GetProductID(int id)
        {
            if(id <= 0)
            {
                return BadRequest(new ResponseServer
                {
                    HttpStatus = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = [$"Неверный id{id}"]
                });
            }

            var product = await DbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if(product == null)
            {
                return NotFound(new ResponseServer
                {
                     HttpStatus = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = [$"Продукт с Id={id} не найден"]
                });
            }
            
            return Ok(new ResponseServer
            {
                HttpStatus = HttpStatusCode.OK,
                Result = product
            });
        }

        [HttpGet]
        public async Task<ActionResult<ResponseServer>> GetProduct()
        {
            ResponseServer response = new ResponseServer
            {
              HttpStatus = HttpStatusCode.OK,
              Result = await DbContext.Products.ToListAsync()
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return Ok(await Task.FromResult<string>("hello Sto"));
        }    

        
    }
}