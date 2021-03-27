using System;
using System.Collections.Generic;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class Voicemessage
    {
        public int VoiId { get; set; }
        public string Voice { get; set; }
        public string VoiceName { get; set; }
        public int? IncidentManagement { get; set; }
        public DateTime? VoiceEnteredby { get; set; }

        public virtual IncidentManagement IncidentManagementNavigation { get; set; }
    }
}
