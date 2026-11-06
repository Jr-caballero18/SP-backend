using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        // Depender de la interfaz permite que el controlador use cualquier implementación registrada.
        private readonly IPostService _titlesService;

        public PostController(IPostService titlesService)
        {
            _titlesService = titlesService;
        }
        [HttpGet]
        public async Task<IEnumerable<PostDto>> Get() => await _titlesService.Get();
    }
}
