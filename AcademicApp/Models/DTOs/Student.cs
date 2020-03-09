using Newtonsoft.Json;
using System;

namespace AcademicApp.Models.DTOs
{
    public sealed class Student
    {
        public string Name { get; }
        public int PersonalIdentifier { get; } // Like Passport number, DNI, etc
        public char Gender { get; }
        public string Type { get; }
        public DateTime Updated { get; }

        [JsonConstructor]
        public Student(string name, int personalIdentifier, char gender, string type, DateTime updated)
        {
            Name = name;
            PersonalIdentifier = personalIdentifier;
            Gender = gender;
            Type = type;
            Updated = updated;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var student = obj as Student;

            return PersonalIdentifier == student.PersonalIdentifier;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PersonalIdentifier);
        }

        public override string ToString()
        {
            return $"Student({Name}, {PersonalIdentifier}, {Gender}, {Type}, {Updated})";
        }
    }
}
