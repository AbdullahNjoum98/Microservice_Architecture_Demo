using AutoMapper;
using Domain.Entities;
using Domain.IRepos;
using Domain.VMs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos
{
    public partial class TeacherRepository : IRepos
    {
        private readonly ProjectDBContext dBContext;
        private readonly IMapper mapper;

        public TeacherRepository(ProjectDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.mapper = mapper;
        }
        public int AddTeacher(TeacherVM teacher)
        {
            try
            {
                var entity = mapper.Map<Teacher>(teacher);
                dBContext.Teachers.Add(entity);
                dBContext.SaveChanges();
                return entity.Id;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public Exception DeleteTeacher(int id)
        {
            try
            {
                var teacherToDelete=dBContext.Teachers.Where(e => e.Id == id).FirstOrDefault();
                dBContext.Teachers.Remove(teacherToDelete);
                dBContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<List<TeacherResource>> GetAllTeachers()
        {
            return mapper.Map<List<TeacherResource>>(await dBContext.Teachers.ToListAsync());
        }

        public async Task<TeacherResource> GetTeacher(int Id)
        {
            return mapper.Map<TeacherResource>(await dBContext.Teachers.Where(e=>e.Id == Id).FirstOrDefaultAsync());
        }

        public Exception UpdateTeacher(TeacherVM teacher)
        {
            try
            {
                dBContext.Teachers.Update(mapper.Map<Teacher>(teacher));
                dBContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
