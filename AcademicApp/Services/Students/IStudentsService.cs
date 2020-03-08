using AcademicApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicApp.Services.Students
{
    public interface IStudentsService
    {
        List<Student> Get();
        Student Get(int id);
        void Add(Student student);
        void Remove(int id);
    }
}
