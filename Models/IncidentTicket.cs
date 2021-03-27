using System;
using System.Collections.Generic;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class IncidentTicket
    {
        public IncidentTicket()
        {
            IncidentManagements = new HashSet<IncidentManagement>();
        }

        public int TicId { get; set; }
        public int? Ticket { get; set; }

        public virtual ICollection<IncidentManagement> IncidentManagements { get; set; }
    }
}
