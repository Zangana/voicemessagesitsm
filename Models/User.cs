using System;
using System.Collections.Generic;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class User
    {
        public User()
        {
            IncidentManagements = new HashSet<IncidentManagement>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public int? Active { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ICollection<IncidentManagement> IncidentManagements { get; set; }
    }
}
