using Api.Data;
using Api.Model;
using Api.ModelDto;
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


        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseServer>> RemoveProductById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponseServer
                    {
                        HttpStatus = HttpStatusCode.BadRequest,
                        IsSuccess = false,
                        ErrorMessages = [$"Неверный id {id}"]
                    });
                }

                var product = await DbContext.Products.FindAsync(id);

                if(product == null)
                {
                    return NotFound(new ResponseServer
                    {
                        HttpStatus = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        ErrorMessages = [$"Продукт с Id={id} не найден"]
                    });
                }

                DbContext.Products.Remove(product); 
                await DbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseServer
                    {
                        IsSuccess = false,
                        HttpStatus = HttpStatusCode. BadRequest,
                        ErrorMessages = { $"Проблемы с {id}: {ex.Message}" }
                    });
                
            }
        }
        [HttpPost]
        public async Task<ActionResult<ResponseServer>> AddProduct(ProductCreateDto productCreateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(productCreateDto.Image == null || productCreateDto.Image.Length == 0)
                    {
                        return BadRequest(new ResponseServer
                        {
                            HttpStatus = HttpStatusCode.BadRequest,
                            IsSuccess = false,
                            ErrorMessages = ["Image  не можит быть пусты"]
                        });
                    }
                    else
                    {
                        var product = new Product
                        {
                            Name = productCreateDto.Name,
                            Description = productCreateDto.Description,
                            SpecialTag = productCreateDto.SpecialTag,
                            Category = productCreateDto.Category,
                            Price = productCreateDto.Price,
                            Image = "https://placehold.ru/200"
                        };
                        
                        await DbContext.Products.AddAsync(product);
                        await DbContext.SaveChangesAsync();

                        ResponseServer response = new ResponseServer
                        {
                            HttpStatus = HttpStatusCode.Created,
                            Result = product
                        };

                        return CreatedAtRoute(nameof(GetProductID), new {id = product.Id}, response);
                    }
                }
                else
                {
                    return BadRequest(new ResponseServer
                    {
                        IsSuccess = false,
                        HttpStatus = HttpStatusCode. BadRequest,
                        ErrorMessages = { "Модель данные не подходит" }
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseServer
                    {
                        IsSuccess = false,
                        HttpStatus = HttpStatusCode. BadRequest,
                        ErrorMessages = {$"чтото поломалось {ex}" }
                    }); 
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseServer>> UbdateProduct(int id,ProductUpdateDto productUpdateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(productUpdateDto == null ||  productUpdateDto.Id != id)
                    {
                         return BadRequest(new ResponseServer
                         {
                             IsSuccess = false,
                             HttpStatus = HttpStatusCode.BadRequest,
                             ErrorMessages = {"несоотвествие модели данных"}
                         });
                    }
                    else
                    {
                        Product productFromDb = await DbContext.Products.FindAsync(id);
                        if (productFromDb == null)
                        {
                            return NotFound(new ResponseServer
                            {
                                IsSuccess = false,
                                HttpStatus = HttpStatusCode.NotFound,
                                ErrorMessages = {$"Продукт с таким  Id {id} не найден"}
                            });
                        }

                        productFromDb.Name = productUpdateDto.Name;
                        productFromDb.Description = productUpdateDto.Description;
                        productFromDb.SpecialTag = productUpdateDto.SpecialTag;
                        productFromDb.Category = productUpdateDto.Category;
                        productFromDb.Price = productUpdateDto.Price;
                        productFromDb.Image = productUpdateDto.Image;

                        if (productFromDb.Image != null && productFromDb.Image.Length > 0)
                        {
                            productFromDb.Image = "https://placehold.ru/350";
                        }

                        DbContext.Update(productFromDb);
                        await DbContext.SaveChangesAsync();

                        return Ok(new ResponseServer
                        {
                            HttpStatus = HttpStatusCode.OK,
                            Result = productFromDb
                        });
                    }
                }
                else
                {
                    return BadRequest(new ResponseServer
                         {
                             IsSuccess = false,
                             HttpStatus = HttpStatusCode.BadRequest,
                             ErrorMessages = {"модель не соотвецтвует"}
                         });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseServer
                         {
                             IsSuccess = false,
                             HttpStatus = HttpStatusCode.BadRequest,
                             ErrorMessages = {$" Чтото пошло не так {ex}"}
                         });
            }
        }

        [HttpGet("{id}",Name = nameof(GetProductID))]
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