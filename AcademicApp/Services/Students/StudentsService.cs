using AcademicApp.Helpers;
using AcademicApp.Models.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AcademicApp.Services.Students
{
    public class StudentsService : IStudentsService
    {
        private IDictionary<int, Student> _students;

        private readonly string _csvFileName;
        private readonly ILogger _logger;

        public StudentsService(IConfiguration configuration)
        {
            var csvConfiguration = configuration.GetSection("Csv");
            _csvFileName = csvConfiguration.GetValue<string>("FileName");
            _students = GetStudentsFromCsvFile();
        }        

        public List<Student> Get()
        {
            return _students.Values.ToList();
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
            }
        }

        public void Update(int personalIdentifier, StudentItem student)
        {
            _students[personalIdentifier] = new Student(student.Name, student.PersonalIdentifier, student.Gender, student.Type, student.Updated);
        }

        public void Remove(int personalIdentifier)
        {
            _students.Remove(personalIdentifier);
        }

        private Dictionary<int, Student> GetStudentsFromCsvFile()
        {
            var students = new Dictionary<int, Student>();

            try
            {
                students = CsvFileHelper.Import(_csvFileName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to get CSV file");
            }

            return students;
        }
    }
}
