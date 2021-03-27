using System;
using System.Collections.Generic;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class Image
    {
        public int ImgId { get; set; }
        public string Image1 { get; set; }
        public string ImageName { get; set; }
        public int? IncidentManagement { get; set; }
        public DateTime? ImgEnteredby { get; set; }

        public virtual IncidentManagement IncidentManagementNavigation { get; set; }
    }
}
