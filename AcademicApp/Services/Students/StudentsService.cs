using AcademicApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicApp.Services.Students
{
    public class StudentsService : IStudentsService
    {
        private IDictionary<int, Student> _students;

        public StudentsService()
        {
        }

        public List<Student> Get()
        {
            return null;
        }

        public Student Get(int id)
        {
            return null;
        }

        public void Add(Student student)
        {
            //return true;
        }

        public void Remove(int id)
        {
            //return true;
        }
    }
}
