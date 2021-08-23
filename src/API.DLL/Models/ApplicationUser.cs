using System;
using API.DLL.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.DLL.Models
{
    public class ApplicationUser : IdentityUser, ITrackable, ISoftDeletable
    {
        public string FullName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}