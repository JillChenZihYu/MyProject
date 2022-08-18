using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class VMAdministrator
    {
        public int AdministratorID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Authorize { get; set; }
    }
}