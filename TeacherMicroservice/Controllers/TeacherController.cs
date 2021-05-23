using Domain.IRepos;
using Domain.VMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskAPI;

namespace TeacherMicroservice.Controllers
{
    [ApiController]
    [Route("Teachers")]
    public class TeacherController : Controller
    {
        private readonly IRepos repository;
        public TeacherController(IRepos repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public Task<List<TeacherResource>> GetAllTeachers()
        {
            return repository.GetAllTeachers();
        }
        [HttpGet("{Id}")]
        public Task<TeacherResource> GetTeacher(int Id)
        {
            return repository.GetTeacher(Id);
        }
        [HttpPost]
        public Task<TeacherResource> AddTeacher([FromBody] TeacherVM teacher)
        {
            var jsonString = JsonSerializer.Serialize(teacher);
            var bytesObject = Encoding.UTF8.GetBytes(jsonString);
            HelperMethods.Producer(bytesObject);
            repository.AddTeacher(teacher);
            return repository.GetTeacher((int)teacher.Id);
        }
        [HttpPut]
        public Task<TeacherResource> UpdateEmployee([FromBody] TeacherVM teacher)
        {
            repository.UpdateTeacher(teacher);
            return repository.GetTeacher((int)teacher.Id);
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteTeacher(int id)
        {
            if (repository.DeleteTeacher(id) == null)
                return Ok();
            else
            {
                string exception = HelperMethods.getException(repository.DeleteTeacher(id));
                return BadRequest(exception);
            }
        }
    }
}
