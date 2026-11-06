using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        [HttpGet]
        public decimal Add(decimal a, decimal b)
        // agregar valores para que los sume al http://localhost:5141/api/operation?a=3&b=8
        {
            return a + b;
        }

          [HttpPost] // enviar parametros 
        public decimal AddDecimal(Numbers numbers , [FromHeader] string Host,
        [FromHeader(Name ="Content-Length")] string ContentLength,
        [FromHeader(Name ="X-Some")] string Some )
        // agregar valores para que los reste al http://localhost:5141/api/operation?a=3&b=8
        {
            Console.WriteLine(Host);
            Console.WriteLine(ContentLength);
            Console.WriteLine(Some);
            return numbers.A - numbers.B;
        }
// crear un nuevo controlador con nombre 
            [HttpPut]
        public decimal AddDivide(decimal a, decimal b)
        // agregar valores para que los reste al http://localhost:5141/api/operation?a=3&b=8
        {
            return a / b;
        }

            [HttpDelete]
        public decimal AddMultiplique(decimal a, decimal b)
        // agregar valores para que los reste al http://localhost:5141/api/operation?a=3&b=8
        {
            return a * b;
        }



    }
    public class Numbers
    {
        public decimal A {get;set;}
         public decimal B {get;set;}

    }


}