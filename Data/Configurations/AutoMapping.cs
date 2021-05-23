using AutoMapper;
using Domain.Entities;
using Domain.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data.Configurations
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            this.CreateMap<Teacher, TeacherVM>().ReverseMap().MaxDepth(3);
            this.CreateMap<Teacher, TeacherResource>().ReverseMap().MaxDepth(3);

        }
    }
}
