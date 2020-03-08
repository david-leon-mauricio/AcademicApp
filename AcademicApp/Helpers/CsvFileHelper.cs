using AcademicApp.Models.DTOs;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace AcademicApp.Helpers
{
    public static class CsvFileHelper
    {
        public static Dictionary<int, Student> Import(string filename)
        {
            using var reader = new StreamReader(filename);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            {
                var students = new Dictionary<int, Student>();

                csvReader.Configuration.Delimiter = ";";
                csvReader.Configuration.PrepareHeaderForMatch = (header, index) => header.ToLower();
                csvReader.Read();
                csvReader.ReadHeader();
                
                while (csvReader.Read())
                {
                    var student = new Student
                    (
                        csvReader.GetField(CsvHeaders.Name),
                        csvReader.GetField<int>(CsvHeaders.PersonalIdentifier),
                        csvReader.GetField<char>(CsvHeaders.Gender),
                        csvReader.GetField(CsvHeaders.Type),
                        DateTime.ParseExact(csvReader.GetField(CsvHeaders.Updated), "yyyyMMddHHmmss", CultureInfo.InvariantCulture)
                    );
                    students.Add(student.PersonalIdentifier, student);
                }

                return students;
            }
        }
    }
}
