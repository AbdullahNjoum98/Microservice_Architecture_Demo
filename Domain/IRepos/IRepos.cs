using Domain.VMs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepos
{
    public interface IRepos
    {
        #region Teacher 
        public Task<List<TeacherResource>> GetAllTeachers();
        public Task<TeacherResource> GetTeacher(int id);
        public Exception AddTeacher(TeacherVM teacher);
        public Exception UpdateTeacher(TeacherVM employee);
        public Exception DeleteTeacher(int id);
        #endregion

    }
}
