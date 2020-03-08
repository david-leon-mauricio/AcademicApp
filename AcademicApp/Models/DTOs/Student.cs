﻿using System;

namespace AcademicApp.Models.DTOs
{
    public sealed class Student
    {
        public string Name { get; }
        public int PersonalIdentifier { get; } // This was required to avoid insert duplicated values
        public char Gender { get; }
        public string Type { get; }
        public DateTime Modified { get; }

        public Student(string name, int personalIdentifier, char gender, string type, DateTime modified)
        {
            Name = name;
            PersonalIdentifier = personalIdentifier;
            Gender = gender;
            Type = type;
            Modified = modified;
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
            return $"Student({Name}, {PersonalIdentifier}, {Gender}, {Type}, {Modified})";
        }
    }
}
