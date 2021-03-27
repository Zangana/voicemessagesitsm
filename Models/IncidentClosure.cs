using System;
using System.Collections.Generic;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class IncidentClosure
    {
        public IncidentClosure()
        {
            IncidentManagements = new HashSet<IncidentManagement>();
        }

        public int CloId { get; set; }
        public int? Closure { get; set; }

        public virtual ICollection<IncidentManagement> IncidentManagements { get; set; }
    }
}
