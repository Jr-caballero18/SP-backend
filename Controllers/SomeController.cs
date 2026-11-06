using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();//mide el tiempo de los hilos
            stopwatch.Start();//inicia
            Thread.Sleep(1000);
            Console.WriteLine("conexión a base de datos terminada");
            Thread.Sleep(1000);
            Console.WriteLine("Envio de mail terminada");
            Console.WriteLine("Todo ha finalizado");
            stopwatch.Stop();//detiene el proceso 
            return Ok(stopwatch.Elapsed);//para ver el timpo que tardo. 
        }
        [HttpGet("async")]
        public async  Task<IActionResult> GetASync()
        {        Stopwatch stopwatch = Stopwatch.StartNew();

        var task1 = new Task<int>(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("cceoneci a base terminada");
            return 1;
        });    
        var task2 = new Task<int>(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("cceoneci a base terminada");
            return 2;
        });    
        task1.Start();
        Console.WriteLine("hago otra cosa");
        var result = await task1;
        Console.WriteLine("todo ha terminado");
        stopwatch.Stop();
        return Ok(stopwatch.Elapsed);
        }
    }
}
