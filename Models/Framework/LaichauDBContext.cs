namespace Models.Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LaichauDBContext : DbContext
    {
        public LaichauDBContext()
            : base("name=LaichauDBConnection")
        {
        }

        public virtual DbSet<DataType> DataTypes { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<MeasurementData> MeasurementDatas { get; set; }
        public virtual DbSet<MeasurementLocation> MeasurementLocations { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageType> MessageTypes { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<WarningSetting> WarningSettings { get; set; }
        public virtual DbSet<PhotoInAlbum> PhotoInAlbums { get; set; }
        public virtual DbSet<PhotoAlbum> PhotoAlbums { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataType>()
                .Property(e => e.mUnit)
                .IsFixedLength();

            modelBuilder.Entity<DataType>()
                .HasMany(e => e.MeasurementDatas);


            modelBuilder.Entity<DataType>()
                .HasMany(e => e.WarningSettings)
                .WithRequired(e => e.DataType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Wards)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MeasurementLocation>()
                .HasMany(e => e.MeasurementDatas);

            modelBuilder.Entity<MeasurementLocation>()
                .HasMany(e => e.WarningSettings)
                .WithRequired(e => e.MeasurementLocation)
                .HasForeignKey(e => e.locationID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MessageType>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.MessageType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserAccounts)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.loginPassword)
                .IsFixedLength();

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.phoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.userPrivateNumber)
                .IsFixedLength();

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.MeasurementLocations)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.supplierID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.senderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.Photos)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.uploadBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.WarningSettings)
                .WithRequired(e => e.UserAccount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ward>()
                .HasMany(e => e.MeasurementLocations)
                .WithRequired(e => e.Ward)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Ward>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Ward)
                .HasForeignKey(e=>e.reiceiverID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ward>()
                .HasMany(e => e.Photos)
                .WithRequired(e => e.Ward)
                .HasForeignKey(e => e.wardID)
                .WillCascadeOnDelete(false);



            modelBuilder.Entity<PhotoAlbum>()
                .HasMany(e => e.PhotoInAlbums);
                
        }

      //  public System.Data.Entity.DbSet<LaichauGIS_Data.Areas.Admin.Models.LoginPasswordUpdateModel> LoginPasswordUpdateModels { get; set; }
    }
}
