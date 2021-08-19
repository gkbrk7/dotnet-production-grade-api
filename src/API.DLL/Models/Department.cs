using System;
using System.Collections.Generic;
using API.DLL.Models.Interfaces;

namespace API.DLL.Models
{
    public class Department : ISoftDeletable, ITrackable
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}