using System;
using System.Collections.Generic;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class IncidentManagement
    {
        public IncidentManagement()
        {
            Descriptions = new HashSet<Description>();
            Images = new HashSet<Image>();
            Voicemessages = new HashSet<Voicemessage>();
        }

        public int IncidentId { get; set; }
        public int? UserId { get; set; }
        public DateTime? IncidentDate { get; set; }
        public int? IncidentTicket { get; set; }
        public int? IncidentLoggings { get; set; }
        public int? IncidentResolutions { get; set; }
        public int? IncidentClosures { get; set; }
        public int? IncidentPriorities { get; set; }
        public int? IncidentCategories { get; set; }

        public virtual IncidentCategory IncidentCategoriesNavigation { get; set; }
        public virtual IncidentClosure IncidentClosuresNavigation { get; set; }
        public virtual IncidentLogging IncidentLoggingsNavigation { get; set; }
        public virtual IncidentPriority IncidentPrioritiesNavigation { get; set; }
        public virtual IncidentResolution IncidentResolutionsNavigation { get; set; }
        public virtual IncidentTicket IncidentTicketNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Description> Descriptions { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Voicemessage> Voicemessages { get; set; }
    }
}
