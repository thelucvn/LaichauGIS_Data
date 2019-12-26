namespace Models.Framework
{
    using System.Data.Entity;

    public partial class LaichauDBContext : DbContext
    {
        public LaichauDBContext()
            : base("LaichauDBContext")
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataType>()
                .Property(e => e.mUnit)
                .IsFixedLength();

            modelBuilder.Entity<DataType>()
                .HasMany(e => e.MeasurementDatas)
                .WithRequired(e => e.DataType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataType>()
                .HasMany(e => e.WarningSettings)
                .WithRequired(e => e.DataType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Wards)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MeasurementLocation>()
                .HasMany(e => e.MeasurementDatas)
                .WithRequired(e => e.MeasurementLocation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MeasurementLocation>()
                .HasMany(e => e.Photos)
                .WithRequired(e => e.MeasurementLocation)
                .HasForeignKey(e => e.locationID)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.MeasurementDatas)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.supplierID)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.Messages1)
                .WithRequired(e => e.UserAccount1)
                .HasForeignKey(e => e.reiceiverID)
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
        }
        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
