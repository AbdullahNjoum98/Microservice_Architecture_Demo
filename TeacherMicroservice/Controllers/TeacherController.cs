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
            var Id=repository.AddTeacher(teacher);
            if (Id != 0)
            {
                var newTeacher = repository.GetTeacher(Id);
                var jsonObject = new { id = newTeacher.Result.Id, process="add" };
                var jsonString = JsonSerializer.Serialize(jsonObject);
                var bytesObject = Encoding.UTF8.GetBytes(jsonString);
                HelperMethods.Producer(bytesObject);
            }
            return repository.GetTeacher(Id);
        }
        [HttpPut]
        public Task<TeacherResource> UpdateTeacher([FromBody] TeacherVM teacher)
        {
            if (repository.UpdateTeacher(teacher) == null)
            {
                var jsonObject = new { id = teacher.Id, process = "update" };
                var jsonString = JsonSerializer.Serialize(jsonObject);
                var bytesObject = Encoding.UTF8.GetBytes(jsonString);
                HelperMethods.Producer(bytesObject);
            }
            return repository.GetTeacher((int)teacher.Id);
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteTeacher(int id)
        {
            Exception exception = repository.DeleteTeacher(id);
            if ( exception == null)
            {
                var jsonObject = new { id = id, process = "delete" };
                var jsonString = JsonSerializer.Serialize(jsonObject);
                var bytesObject = Encoding.UTF8.GetBytes(jsonString);
                HelperMethods.Producer(bytesObject);
                return Ok();
            }
            else
                return BadRequest(HelperMethods.getException(exception));
            
        }
    }
}
