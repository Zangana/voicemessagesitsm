using System;
using System.Collections.Generic;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class IncidentLogging
    {
        public IncidentLogging()
        {
            IncidentManagements = new HashSet<IncidentManagement>();
        }

        public int LogId { get; set; }
        public int? Logging { get; set; }

        public virtual ICollection<IncidentManagement> IncidentManagements { get; set; }
    }
}
