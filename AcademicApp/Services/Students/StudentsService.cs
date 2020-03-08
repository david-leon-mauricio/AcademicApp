using AcademicApp.Helpers;
using AcademicApp.Models.DTOs;
using AcademicApp.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AcademicApp.Services.Students
{
    public class StudentsService : IStudentsService
    {
        private IDictionary<int, Student> _students;
        private readonly ICache _localMemoryCache;
        private const string StudentsKey = "Students";

        public StudentsService(ICache localMemoryCache)
        {
            _localMemoryCache = localMemoryCache;
            _students = localMemoryCache.Get<Dictionary<int, Student>>(StudentsKey);
        }        

        public List<Student> Get(SearchBy searchBy, string studentName, string studentType, char studentGender)
        {
            var students = _students.Values.ToList();

            switch (searchBy)
            {
                case SearchBy.Name:
                    students = students
                        .Where(s => s.Name.Contains(studentName, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(s => s.Name)
                        .ToList();
                    break;
                case SearchBy.Type:
                    students = students
                        .Where(s => s.Type.Equals(studentType, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(s => s.Type)
                        .ThenByDescending(s => s.Updated)
                        .ToList();
                    break;
                case SearchBy.GenderAndType:
                    students = students
                        .Where(s => char.ToLower(s.Gender).Equals(char.ToLower(studentGender)) && s.Type.Equals(studentType, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(s => s.Gender)
                        .ThenBy(s => s.Type)
                        .ThenByDescending(s => s.Updated)
                        .ToList();
                    break;
                default:
                    break;
            }

            return students;
        }

        public Student Get(int personalIdentifier)
        {
            Student student = null;
            if (_students.TryGetValue(personalIdentifier, out var value))
            {
                student = value;
            }

            return student;
        }

        public void Add(StudentItem student)
        {
            if (!_students.ContainsKey(student.PersonalIdentifier))
            {
                _students.Add(student.PersonalIdentifier, new Student(student.Name, student.PersonalIdentifier, student.Gender, student.Type, student.Updated));
                EnsureData();
            }
        }

        public void Update(int personalIdentifier, StudentItem student)
        {
            _students[personalIdentifier] = new Student(student.Name, student.PersonalIdentifier, student.Gender, student.Type, student.Updated);
            EnsureData();
        }

        public void Remove(int personalIdentifier)
        {
            _students.Remove(personalIdentifier);
            EnsureData();
        }

        private void EnsureData()
        {
            _localMemoryCache.Set(StudentsKey, _students);
        }
    }
}
