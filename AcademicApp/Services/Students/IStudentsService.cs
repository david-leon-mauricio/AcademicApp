using AcademicApp.Models.DTOs;
using System.Collections.Generic;

namespace AcademicApp.Services.Students
{
    public interface IStudentsService
    {
        List<Student> Get();
        Student Get(int personalIdentifier);
        void Add(StudentItem student);
        void Update(int personalIdentifier, StudentItem student);
        void Remove(int personalIdentifier);
    }
}
