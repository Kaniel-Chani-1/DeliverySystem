using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Repositories.Models
{
    public partial class DeliverySystemContext : DbContext
    {
        public DeliverySystemContext()
        {
        }

        public DeliverySystemContext(DbContextOptions<DeliverySystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<DetailsInlay> DetailsInlay { get; set; }
        public virtual DbSet<DetailsOfShifts> DetailsOfShifts { get; set; }
        public virtual DbSet<DetailsOfTheContentsOfReservation> DetailsOfTheContentsOfReservation { get; set; }
        public virtual DbSet<DifferentShippingAddress> DifferentShippingAddress { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Inlays> Inlays { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PackingTypes> PackingTypes { get; set; }
        public virtual DbSet<Shifts> Shifts { get; set; }
        public virtual DbSet<StatusOrder> StatusOrder { get; set; }
        public virtual DbSet<WeekDays> WeekDays { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\DB\\DeliverySystem.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>(entity =>
            {
                entity.HasKey(e => e.IdEmployee);

                entity.Property(e => e.IdEmployee)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.Cars)
                    .HasForeignKey<Cars>(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_ToTable");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCardNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("E-mail")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber1)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber2)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAdress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdCityAdressNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.IdCityAdress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Clients__IdCityA__6AEFE058");
            });

            modelBuilder.Entity<DetailsInlay>(entity =>
            {
                entity.HasKey(e => new { e.IdEmployee, e.IdOrder, e.IdInlay });

                entity.Property(e => e.IdEmployee)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.DetailsInlay)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetailsIn__IdEmp__0E04126B");

                entity.HasOne(d => d.IdInlayNavigation)
                    .WithMany(p => p.DetailsInlay)
                    .HasForeignKey(d => d.IdInlay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetailsIn__IdInl__0FEC5ADD");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.DetailsInlay)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetailsIn__IdOrd__0EF836A4");
            });

            modelBuilder.Entity<DetailsOfShifts>(entity =>
            {
                entity.Property(e => e.IdEmployee)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.DetailsOfShifts)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetailsOf__IdEmp__3BFFE745");

                entity.HasOne(d => d.IdShiftNavigation)
                    .WithMany(p => p.DetailsOfShifts)
                    .HasForeignKey(d => d.IdShift)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetailsOf__IdShi__3CF40B7E");

                entity.HasOne(d => d.IdWeekDayNavigation)
                    .WithMany(p => p.DetailsOfShifts)
                    .HasForeignKey(d => d.IdWeekDay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetailsOf__IdWee__3B0BC30C");
            });

            modelBuilder.Entity<DetailsOfTheContentsOfReservation>(entity =>
            {
                entity.HasKey(e => new { e.IdOrder, e.IdPackingTypes });

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.DetailsOfTheContentsOfReservation)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetailsOf__IdOrd__56B3DD81");

                entity.HasOne(d => d.IdPackingTypesNavigation)
                    .WithMany(p => p.DetailsOfTheContentsOfReservation)
                    .HasForeignKey(d => d.IdPackingTypes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetailsOf__IdPac__793DFFAF");
            });

            modelBuilder.Entity<DifferentShippingAddress>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK__Differen__C38F300960D990AC");

                entity.Property(e => e.IdOrder).ValueGeneratedNever();

                entity.Property(e => e.StreetAdress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdCityAdressNavigation)
                    .WithMany(p => p.DifferentShippingAddress)
                    .HasForeignKey(d => d.IdCityAdress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Different__IdCit__40058253");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithOne(p => p.DifferentShippingAddress)
                    .HasForeignKey<DifferentShippingAddress>(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Different__IdOrd__55BFB948");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("E-mail")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber1)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber2)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inlays>(entity =>
            {
                entity.Property(e => e.ArrivalDateOrder).HasColumnType("date");

                entity.Property(e => e.DateInlay).HasColumnType("date");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.ArrivalDate).HasColumnType("date");

                entity.Property(e => e.IdClient)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__IdClient__54CB950F");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__Status__6BAEFA67");
            });

            modelBuilder.Entity<PackingTypes>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Shifts>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StatusOrder>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WeekDays>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
