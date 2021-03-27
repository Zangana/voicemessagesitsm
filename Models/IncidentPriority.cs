using System;
using System.Collections.Generic;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class IncidentPriority
    {
        public IncidentPriority()
        {
            IncidentManagements = new HashSet<IncidentManagement>();
        }

        public int PriId { get; set; }
        public int? Priority { get; set; }

        public virtual ICollection<IncidentManagement> IncidentManagements { get; set; }
    }
}
