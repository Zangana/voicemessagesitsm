using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ItstmVoiceMessages.Models
{
    public partial class voicemsgitsmContext : DbContext
    {
        public voicemsgitsmContext()
        {
        }

        public voicemsgitsmContext(DbContextOptions<voicemsgitsmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Description> Descriptions { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<IncidentCategory> IncidentCategories { get; set; }
        public virtual DbSet<IncidentClosure> IncidentClosures { get; set; }
        public virtual DbSet<IncidentLogging> IncidentLoggings { get; set; }
        public virtual DbSet<IncidentManagement> IncidentManagements { get; set; }
        public virtual DbSet<IncidentPriority> IncidentPriorities { get; set; }
        public virtual DbSet<IncidentResolution> IncidentResolutions { get; set; }
        public virtual DbSet<IncidentTicket> IncidentTickets { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Voicemessage> Voicemessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-VITDCEF3\\SQLEXPRESS;Database=voicemsgitsm;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Description>(entity =>
            {
                entity.HasKey(e => e.DesId)
                    .HasName("PK__descript__92CD1D5218A0B59E");

                entity.ToTable("descriptions");

                entity.Property(e => e.DesId).HasColumnName("des_id");

                entity.Property(e => e.DesEnteredby)
                    .HasColumnType("datetime")
                    .HasColumnName("des_enteredby");

                entity.Property(e => e.Describe)
                    .HasMaxLength(255)
                    .HasColumnName("describe");

                entity.Property(e => e.IncidentManagement).HasColumnName("incident_management");

                entity.Property(e => e.OperatorsRespond)
                    .HasMaxLength(255)
                    .HasColumnName("operators_respond");

                entity.HasOne(d => d.IncidentManagementNavigation)
                    .WithMany(p => p.Descriptions)
                    .HasForeignKey(d => d.IncidentManagement)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__descripti__incid__4BAC3F29");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.ImgId)
                    .HasName("PK__images__6F16A71CBCB431F0");

                entity.ToTable("images");

                entity.Property(e => e.ImgId).HasColumnName("img_id");

                entity.Property(e => e.Image1)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.ImageName)
                    .HasMaxLength(255)
                    .HasColumnName("image_name");

                entity.Property(e => e.ImgEnteredby)
                    .HasColumnType("datetime")
                    .HasColumnName("img_enteredby");

                entity.Property(e => e.IncidentManagement).HasColumnName("incident_management");

                entity.HasOne(d => d.IncidentManagementNavigation)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.IncidentManagement)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__images__incident__4AB81AF0");
            });

            modelBuilder.Entity<IncidentCategory>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK__incident__DD5DDDBDE8D37055");

                entity.ToTable("incident_categories");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.Category).HasColumnName("category");
            });

            modelBuilder.Entity<IncidentClosure>(entity =>
            {
                entity.HasKey(e => e.CloId)
                    .HasName("PK__incident__C76D6D86B8FBD5F6");

                entity.ToTable("incident_closures");

                entity.Property(e => e.CloId).HasColumnName("clo_id");

                entity.Property(e => e.Closure).HasColumnName("closure");
            });

            modelBuilder.Entity<IncidentLogging>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__incident__9E2397E059D5E153");

                entity.ToTable("incident_loggings");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.Logging).HasColumnName("logging");
            });

            modelBuilder.Entity<IncidentManagement>(entity =>
            {
                entity.HasKey(e => e.IncidentId)
                    .HasName("PK__incident__E6C40DA318A5303D");

                entity.ToTable("incident_management");

                entity.Property(e => e.IncidentId).HasColumnName("incident_id");

                entity.Property(e => e.IncidentCategories).HasColumnName("incident_categories");

                entity.Property(e => e.IncidentClosures).HasColumnName("incident_closures");

                entity.Property(e => e.IncidentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("incident_date");

                entity.Property(e => e.IncidentLoggings).HasColumnName("incident_loggings");

                entity.Property(e => e.IncidentPriorities).HasColumnName("incident_priorities");

                entity.Property(e => e.IncidentResolutions).HasColumnName("incident_resolutions");

                entity.Property(e => e.IncidentTeams).HasColumnName("incident_teams");

                entity.Property(e => e.IncidentTicket).HasColumnName("incident_ticket");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.IncidentCategoriesNavigation)
                    .WithMany(p => p.IncidentManagements)
                    .HasForeignKey(d => d.IncidentCategories)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__incident___incid__4E88ABD4");

                entity.HasOne(d => d.IncidentClosuresNavigation)
                    .WithMany(p => p.IncidentManagements)
                    .HasForeignKey(d => d.IncidentClosures)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__incident___incid__52593CB8");

                entity.HasOne(d => d.IncidentLoggingsNavigation)
                    .WithMany(p => p.IncidentManagements)
                    .HasForeignKey(d => d.IncidentLoggings)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__incident___incid__4F7CD00D");

                entity.HasOne(d => d.IncidentPrioritiesNavigation)
                    .WithMany(p => p.IncidentManagements)
                    .HasForeignKey(d => d.IncidentPriorities)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__incident___incid__534D60F1");

                entity.HasOne(d => d.IncidentResolutionsNavigation)
                    .WithMany(p => p.IncidentManagements)
                    .HasForeignKey(d => d.IncidentResolutions)
                    .HasConstraintName("FK_incident_management_incident_resolutions1");

                entity.HasOne(d => d.IncidentTicketNavigation)
                    .WithMany(p => p.IncidentManagements)
                    .HasForeignKey(d => d.IncidentTicket)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__incident___incid__5070F446");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IncidentManagements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__incident___user___4CA06362");
            });

            modelBuilder.Entity<IncidentPriority>(entity =>
            {
                entity.HasKey(e => e.PriId)
                    .HasName("PK__incident__7D64E46421255B90");

                entity.ToTable("incident_priorities");

                entity.Property(e => e.PriId).HasColumnName("pri_id");

                entity.Property(e => e.Priority).HasColumnName("priority");
            });

            modelBuilder.Entity<IncidentResolution>(entity =>
            {
                entity.HasKey(e => e.ResId)
                    .HasName("PK__incident__2090B50D26653329");

                entity.ToTable("incident_resolutions");

                entity.Property(e => e.ResId).HasColumnName("res_id");

                entity.Property(e => e.Resolution).HasColumnName("resolution");
            });

            modelBuilder.Entity<IncidentTicket>(entity =>
            {
                entity.HasKey(e => e.TicId)
                    .HasName("PK__incident__CD5CD6A43A146B1F");

                entity.ToTable("incident_ticket");

                entity.Property(e => e.TicId).HasColumnName("tic_id");

                entity.Property(e => e.Ticket).HasColumnName("ticket");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeamsId);

                entity.ToTable("team");

                entity.Property(e => e.TeamsId)
                    .ValueGeneratedNever()
                    .HasColumnName("teams_id");

                entity.Property(e => e.TeamsName)
                    .HasMaxLength(50)
                    .HasColumnName("teams_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserType)
                    .HasMaxLength(255)
                    .HasColumnName("user_type");
            });

            modelBuilder.Entity<Voicemessage>(entity =>
            {
                entity.HasKey(e => e.VoiId)
                    .HasName("PK__voicemes__4A24FB5C450DB5E7");

                entity.ToTable("voicemessages");

                entity.Property(e => e.VoiId).HasColumnName("voi_id");

                entity.Property(e => e.IncidentManagement).HasColumnName("incident_management");

                entity.Property(e => e.Voice)
                    .IsUnicode(false)
                    .HasColumnName("voice");

                entity.Property(e => e.VoiceEnteredby)
                    .HasColumnType("datetime")
                    .HasColumnName("voice_enteredby");

                entity.Property(e => e.VoiceName)
                    .HasMaxLength(255)
                    .HasColumnName("voice_name");

                entity.HasOne(d => d.IncidentManagementNavigation)
                    .WithMany(p => p.Voicemessages)
                    .HasForeignKey(d => d.IncidentManagement)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__voicemess__incid__4D94879B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
