using System;
using System.Collections.Generic;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class Description
    {
        public int DesId { get; set; }
        public string Describe { get; set; }
        public int? IncidentManagement { get; set; }
        public DateTime? DesEnteredby { get; set; }
        public string OperatorsRespond { get; set; }

        public virtual IncidentManagement IncidentManagementNavigation { get; set; }
    }
}
