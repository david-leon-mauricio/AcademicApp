using AcademicApp.Services.Students;
using Microsoft.AspNetCore.Mvc;

namespace AcademicApp.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        // GET: api/Students
        [HttpGet]
        public IActionResult Get()
        {
            var students = _studentsService.Get();

            return Ok(students);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = _studentsService.Get(id);
            if (student != null)
            {
                return Ok(student);
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        public void Post([FromBody] StudentItem student)
        {
            _studentsService.Add(student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] StudentItem student)
        {
            _studentsService.Update(id, student);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _studentsService.Remove(id);
        }
    }
}
