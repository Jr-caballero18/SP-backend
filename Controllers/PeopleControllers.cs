using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;

namespace Backend.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase

    {
        private IPeopleService _peopleService;
        public PeopleController([FromKeyedServices("people2Service")]IPeopleService peopleService)
        {
            _peopleService = peopleService;
            


        }


        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;
        // buscar id 
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var people = Repository.People.FirstOrDefault(p => p.Id == id);

            if (people == null)
            {
                return NotFound();
            }

            return Ok(people);
        }
        // agregar una persona en el metodo post 

        [HttpPost]
         public IActionResult Add(People people)
        {
            if (!_peopleService.Validate(people))
            {
                return BadRequest();
            }
            Repository.People.Add(people);
            return NoContent();

        }



        // busqueda por nombre 
        [HttpGet("search/{search}")]
        public List<People> Get(string search) =>
       Repository.People
           .Where(p => p.Name.ToUpper().Contains(search.ToUpper()))
           .ToList();


        // si no existe por id si no existe va hacer un not found erroe 404 y cuanod si encuentra un 200 y un ok 


        public class Repository
        {
            public static List<People> People = new List<People>
       {
           new People(){Id=1, Name= "pedro", Birthdate=new DateTime(1993,11,11)},
           new People(){Id=2, Name= "oscar", Birthdate=new DateTime(2005,02,19)},
           new People(){Id=3, Name= "frida", Birthdate=new DateTime(2001,09,01)},
           new People(){Id=4, Name= "karina", Birthdate=new DateTime(2005,04,19)}

        // buscar con id 

       };
        }

        public class People
        {
            public int Id { get; set; }
            // Valor inicial para que Name nunca quede null en objetos recién construidos.
            public string Name { get; set; } = string.Empty;

            public DateTime Birthdate { get; set; }


        }
    }
}
