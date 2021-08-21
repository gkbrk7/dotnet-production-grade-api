using System;
using API.DLL.Models.Interfaces;

namespace API.DLL.Models
{
    public class CourseStudent : ISoftDeletable, ITrackable
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}