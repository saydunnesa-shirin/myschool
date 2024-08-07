﻿using Api.Data.Entities;
using Api.Domain.AcademicClasses;
using Api.Domain.AcademicSessions;
using Api.Domain.AcademicClassTemplates;
using Api.Domain.Countries;
using Api.Domain.Employees;
using Api.Domain.Enrollments;
using Api.Domain.Students;

namespace Api.Domain.Institutions
{
    public class Institution : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string Address { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<AcademicClassTemplate> AcademicClassTemplates { get; set; }
        public virtual ICollection<AcademicSession> AcademicSessions { get; set; }
        public virtual ICollection<AcademicClass> AcademicClasses { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
