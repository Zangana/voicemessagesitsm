using System;
using System.Collections.Generic;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class IncidentResolution
    {
        public IncidentResolution()
        {
            IncidentManagements = new HashSet<IncidentManagement>();
        }

        public int ResId { get; set; }
        public int? Resolution { get; set; }

        public virtual ICollection<IncidentManagement> IncidentManagements { get; set; }
    }
}
