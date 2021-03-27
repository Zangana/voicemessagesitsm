using System;
using System.Collections.Generic;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class IncidentCategory
    {
        public IncidentCategory()
        {
            IncidentManagements = new HashSet<IncidentManagement>();
        }

        public int CatId { get; set; }
        public int? Category { get; set; }

        public virtual ICollection<IncidentManagement> IncidentManagements { get; set; }
    }
}
