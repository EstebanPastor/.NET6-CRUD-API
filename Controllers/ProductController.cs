using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;


using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{

    [EnableCors("CorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly DbapiContext _dbcontext;
        public ProductController(DbapiContext _context)
        {
            _dbcontext = _context;


        }

        [HttpGet]
        [Route("/api/listproduct")]
        public IActionResult List()
        {
            List<Product> list = new List<Product>();

            try
            {
                list = _dbcontext.Products.Include(c => c.oCategory).ToList();

                return StatusCode(StatusCodes.Status200OK, new { msg = "Ok", response = list });

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { msg = ex.Message, response = list });

            }
        }

        [HttpGet]
        [Route("/api/getProduct/{idProduct:int}")]
        public IActionResult GetProduct(int idProduct)
        {
            Product oProduct = _dbcontext.Products.Find(idProduct);

            if (oProduct == null)
            {
                return BadRequest("Product not found");
            }

            try
            {
                oProduct = _dbcontext.Products.Include(c => c.oCategory).Where(p => p.IdProduct == idProduct).FirstOrDefault();



                return StatusCode(StatusCodes.Status200OK, new { msg = "Ok", response = oProduct });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { msg = ex.Message, response = oProduct });

            }

        }

        [HttpPost]
        [Route("/api/saveproduct")]

        public IActionResult SaveProduct([FromBody] Product obj) 
        { 
            try
            {
                _dbcontext.Products.Add(obj);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { msg = "Ok"});
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status200OK, new { msg = ex.Message });
            }
        }


        [HttpPut]
        [Route("/api/editproduct")]

        public IActionResult editproduct([FromBody] Product obj)
        {
            Product oProduct = _dbcontext.Products.Find(obj.IdProduct);

            if (oProduct == null)
            {
                return BadRequest("Product not found");
            }
            try
            {
                oProduct.BarCode = obj.BarCode is null ? oProduct.BarCode : obj.BarCode;
                oProduct.ProdDescription = obj.ProdDescription is null ? oProduct.ProdDescription : obj.ProdDescription;
                oProduct.ProdLabel = obj.ProdLabel is null ? oProduct.ProdLabel : obj.ProdLabel;
                oProduct.IdCategory = obj.IdCategory is null ? oProduct.IdCategory : obj.IdCategory;
                oProduct.Price = obj.Price is null ? oProduct.Price : obj.Price;


                _dbcontext.Products.Update(oProduct);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { msg = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { msg = ex.Message });
            }
        }

        [HttpDelete]
        [Route("/api/deleteproduct/{idProduct:int}")]
        public IActionResult DeleteProd(int idProduct)
        {

            Product oProduct = _dbcontext.Products.Find(idProduct);

            if (oProduct == null)
            {
                return BadRequest("Product not found");
            }
            try
            {
                

                _dbcontext.Products.Remove(oProduct);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { msg = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { msg = ex.Message });
            }
        }


    }
  
}

