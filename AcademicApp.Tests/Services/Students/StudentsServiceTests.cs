using AcademicApp.Helpers;
using AcademicApp.Models.DTOs;
using AcademicApp.Services.Students;
using AcademicApp.Storage;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AcademicApp.Tests.Services.Students
{
    public class StudentsServiceTests
    {
        private readonly StudentsService _subject;

        private const string StudentsKey = "Students";

        public StudentsServiceTests()
        {
            var localMemoryCache = new Mock<ICache>();
            localMemoryCache.Setup(x => x.Get<Dictionary<int, Student>>(StudentsKey)).Returns(() => new Dictionary<int, Student>
            {
                { 1, new Student("Leia", 1, 'F', "Kinder", new DateTime(2013, 12, 31, 14, 59, 34)) },
                { 2, new Student("Luke", 2, 'M', "University", new DateTime(2013, 11, 25, 12, 59, 34)) },
                { 3, new Student("Maria", 3, 'F', "High", new DateTime(2014, 10, 20, 11, 59, 34)) },
                { 4, new Student("Charles", 4, 'M', "Elementary", new DateTime(2015, 01, 15, 10, 59, 34)) },
                { 5, new Student("Marcela", 5, 'F', "High", new DateTime(2016, 03, 05, 19, 59, 34)) },
                { 6, new Student("Peter", 6, 'M', "University", new DateTime(2015, 10, 20, 12, 59, 34)) }
            });

            _subject = new StudentsService(localMemoryCache.Object);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_SearchByStudentName_StudentFound()
        {
            var studentName = "Charles";
            var students = _subject.Get(SearchBy.Name, studentName, null, ' ');

            var expectedStudent = new Student("Charles", 4, 'M', "Elementary", new DateTime(2015, 01, 15, 10, 59, 34));
            var actualStudent = students.First();

            Assert.Equal(expectedStudent, actualStudent);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_SearchByStudentType_StudentsFoundAndOrdered()
        {
            var studentType = "High";
            var students = _subject.Get(SearchBy.Type, null, studentType, ' ');

            var expectedStudentMarcela = new Student("Marcela", 5, 'F', "High", new DateTime(2016, 03, 05, 19, 59, 34));
            var expectedStudentMaria = new Student("Maria", 3, 'F', "High", new DateTime(2014, 10, 20, 11, 59, 34));

            Assert.NotEmpty(students);
            Assert.Collection(students,
                student => Assert.Equal(expectedStudentMarcela, student),
                student => Assert.Equal(expectedStudentMaria, student));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_SearchByStudentGenderAndType_StudentsFoundAndOrdered()
        {
            var studentGender = 'M';
            var studentType = "University";
            var students = _subject.Get(SearchBy.GenderAndType, null, studentType, studentGender);

            var expectedStudentPeter = new Student("Peter", 6, 'M', "University", new DateTime(2015, 10, 20, 12, 59, 34));
            var expectedStudentLuke = new Student("Luke", 2, 'M', "University", new DateTime(2013, 11, 25, 12, 59, 34));

            Assert.NotEmpty(students);
            Assert.Collection(students,
                student => Assert.Equal(expectedStudentPeter, student),
                student => Assert.Equal(expectedStudentLuke, student));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Add_NewStudent_StudentAdded()
        {
            var student = new StudentItem 
            { 
                Name = "John",
                PersonalIdentifier = 7,
                Gender = 'M',
                Type = "University"
            };

            _subject.Add(student);
            var actualStudentAdded = _subject.Get(7);
            var expectedStudentAdded = new Student("John", 7, 'F', "University", DateTime.UtcNow);

            Assert.Equal(expectedStudentAdded, actualStudentAdded);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Update_StudentName_StudentUpdated()
        {
            var personalIdentifier = 6;
            var student = new StudentItem
            {
                Name = "Peeteer",
                PersonalIdentifier = personalIdentifier,
                Gender = 'M',
                Type = "University"
            };

            _subject.Update(personalIdentifier, student);
            var actualStudentAdded = _subject.Get(personalIdentifier);
            var expectedStudentAdded = new Student("Peeteer", 6, 'M', "University", DateTime.UtcNow);

            Assert.Equal(expectedStudentAdded, actualStudentAdded);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Remove_ByPersonalIdentifier_StudentRemoved()
        {
            var numberOfStudentsBeforeToRemove = _subject.Get(SearchBy.None, "", "", ' ').Count;

            var personalIdentifier = 6;
            _subject.Remove(personalIdentifier);

            var student = _subject.Get(personalIdentifier);
            var actualnumberOfStudents = _subject.Get(SearchBy.None, "", "", ' ').Count;

            Assert.Null(student);
            Assert.True(numberOfStudentsBeforeToRemove - 1 == actualnumberOfStudents);
        }
    }
}
