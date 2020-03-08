using AcademicApp.Helpers;
using AcademicApp.Models.DTOs;
using System.Collections.Generic;

namespace AcademicApp.Services.Students
{
    public interface IStudentsService
    {
        List<Student> Get(SearchBy searchBy, string studentName, string studentType, char studentGender);
        Student Get(int personalIdentifier);
        void Add(StudentItem student);
        void Update(int personalIdentifier, StudentItem student);
        void Remove(int personalIdentifier);
    }
}
