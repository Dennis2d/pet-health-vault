using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemesterHealtVaultProject.Models
{
    public class HealthDocument
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }

        public DateTime DateCreated { get; set; }
        public IdentityUser User { get; set; }
    }
}
