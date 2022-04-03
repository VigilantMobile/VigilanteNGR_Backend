using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.AppTroopers.Missing;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.AppTroopers.Wanted;
using Domain.Entities.CompanyEntities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Models.AppTroopers.Curfew;
using Infrastructure.Persistence.Models.Identity;
using Infrastructure.Persistence.Models.Identity.Location;
using Infrastructure.Persistence.Models.LocationEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        
        public DbSet<DemographicEntitiesCoordinatesJSON>  demographicEntitiesCoordinatesJSONs { get; set; }

        //Identity
        public DbSet<CustomClaims> CustomClaims { get; set; }
        //state
        public DbSet<NPFStateAdmin> NPFStateAdmins { get; set; }
        public DbSet<NPFStateOperator> NPFStateOperator { get; set; }
        //lga
        public DbSet<NPFLGAAdmin> NPFLGAAdmin { get; set; }
        public DbSet<NPFLGAOperator> NPFLGAOperator { get; set; }
        //town
        public DbSet<NPFTownAdmin> NPFTownAdmin { get; set; }
        public DbSet<NPFTownOperator> NPFTownOperator { get; set; }
        //settlement
        public DbSet<NPFSettlementAdmin> NPFSettlementAdmin { get; set; }
        public DbSet<NPFSettlementOperator> NPFSettlementOperator { get; set; }

        // staff
        public DbSet<VGNGAStaff> VGNGAStaff { get; set; }


        //Location
        public DbSet<State> States { get; set; }
        public DbSet<LGA> LGAs { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Settlement> Settlements { get; set; }

        //App Troopers
        //Security Tip
        public DbSet<SecurityTipCategory> SecurityTipCategories { get; set; }
        public DbSet<SecurityTip> SecurityTips { get; set; }        //
        public DbSet<BroadcasterType> BroadcasterTypes { get; set; }
        public DbSet<BroadcastLevel> BroadcastLevels { get; set; }
        public DbSet<AlertLevel> AlertLevels { get; set; }

        //Panic
        public DbSet<Panic> PanicRecords { get; set; }
        public DbSet<Commute> CommuteRecords { get; set; }

        //Curfew
        public DbSet<StateCurfew> StateCurfew { get; set; }
        public DbSet<LGACurfew> LGACurfew { get; set; }
        public DbSet<TownCurfew> TownCurfew { get; set; }
        public DbSet<SettlementCurfew> SettlementCurfew { get; set; }

        //Missing
        public DbSet<MissingPerson> MissingPerson { get; set; }
        public DbSet<MissingItem> MissingItem { get; set; }

        //Wanted
        public DbSet<WantedPerson> WantedPerson { get; set; }

        //Audit
        public DbSet<Audit> AuditLogs { get; set; }

        //Department
        public DbSet<Department> Departments { get; set; }

        //Test
        public DbSet<Product> Products { get; set; }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken(), string userId = null)
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }

            OnBeforeSaveChanges(userId);
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });


            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });


            builder.Entity<CustomClaims>(entity =>
            {
                entity.ToTable("CustomClaims");
            });






            // Geometry Types 


            //State 

            builder.Entity<State>().Property(p => p.Boundary).HasColumnType("geography");

            //LGA
            builder.Entity<LGA>()
                
                .Property(p => p.Boundary).HasColumnType("geography");

            //Town
            builder.Entity<Town>().Property(p => p.Boundary).HasColumnType("geography");

            //Settlement
            builder.Entity<Settlement>().Property(p => p.Boundary).HasColumnType("geography");

            //RELATIONSHIPS //-------------------------------------------------------------------------------------------------------------


            //Identity

            builder.Entity<ApplicationUser>().HasOne(s => s.State)
             .WithMany(g => g.Customers).HasForeignKey(s => s.StateId).OnDelete(DeleteBehavior.Restrict);



            //State
            builder.Entity<VGNGAStaff>()
             .HasMany(x => x.VGNGAAdminStates)
            .WithMany(x => x.VGNGAStateAdmins);

            builder.Entity<VGNGAStaff>()
            .HasMany(x => x.VGNGAOperatorStates)
           .WithMany(x => x.VGNGAStateOperators);


            //LGA
            builder.Entity<ApplicationUser>().HasOne(s => s.LGA)
            .WithMany(g => g.Customers).HasForeignKey(s => s.LGAId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VGNGAStaff>()
           .HasMany(x => x.VGNGAOperatorLGAs)
           .WithMany(x => x.VGNGALGAOperators);

            builder.Entity<VGNGAStaff>()
              .HasMany(x => x.VGNGAAdminLGAs)
              .WithMany(x => x.VGNGALGAAdmins);

            //Town
            builder.Entity<ApplicationUser>().HasOne(s => s.Town)
            .WithMany(g => g.Customers).HasForeignKey(s => s.TownId).OnDelete(DeleteBehavior.Restrict);


            builder.Entity<VGNGAStaff>()
            .HasMany(x => x.VGNGAAdminTowns)
            .WithMany(x => x.VGNGATownAdmins);

            builder.Entity<VGNGAStaff>()
            .HasMany(x => x.VGNGAOperatorTowns)
            .WithMany(x => x.VGNGATownOperators);

            //Settlement
            builder.Entity<ApplicationUser>().HasOne(s => s.Settlement)
            .WithMany(g => g.Customers).HasForeignKey(s => s.SettlementId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VGNGAStaff>()
           .HasMany(x => x.VGNGAAdminSettlements)
           .WithMany(x => x.VGNGASettlementAdmins);

            builder.Entity<VGNGAStaff>()
            .HasMany(x => x.VGNGAOperatorSettlements)
            .WithMany(x => x.VGNGASettlementOperators);

            builder.Entity<VGNGAStaff>()
            .HasMany(x => x.VGNGAOperatorSettlements)
            .WithMany(x => x.VGNGASettlementOperators);

            //Commute
            //Town Commutes
            builder.Entity<Commute>().HasOne(s => s.DepartureTown)
            .WithMany(g => g.DepartureTownCommutes).HasForeignKey(s => s.DepartureTownId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Commute>().HasOne(s => s.DestinationTown)
           .WithMany(g => g.DestinationTownCommutes).HasForeignKey(s => s.DestinationTownId).OnDelete(DeleteBehavior.Restrict);

            //SettlementCommutes
            builder.Entity<Commute>().HasOne(s => s.DepartureSettlement)
           .WithMany(g => g.DepartureSettlementCommutes).HasForeignKey(s => s.DepartureSettlementId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Commute>().HasOne(s => s.DestinationSettlement)
           .WithMany(g => g.DestinationSettlementCommutes).HasForeignKey(s => s.DestinationSettlementId).OnDelete(DeleteBehavior.Restrict);

            // Departments
            builder.Entity<VGNGAStaff>().HasOne(s => s.Department)
            .WithMany(g => g.VGNGAStaff).HasForeignKey(s => s.DepartmentId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Department>().HasOne(s => s.HOD)
           .WithMany(g => g.HODDepartments).HasForeignKey(s => s.HodId).OnDelete(DeleteBehavior.Restrict).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Department>().HasOne(s => s.Secretary)
            .WithMany(g => g.SecretaryDepartments).HasForeignKey(s => s.SecretaryId).OnDelete(DeleteBehavior.Restrict);



            //wanted
            builder.Entity<WantedPerson>().HasOne(s => s.Town)
           .WithMany(g => g.TownWantedPeople).HasForeignKey(s => s.TownId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WantedPerson>().HasOne(s => s.Settlement)
            .WithMany(g => g.SettlementWantedPeople).HasForeignKey(s => s.SettlementId).OnDelete(DeleteBehavior.Restrict);

            //missing
            builder.Entity<MissingPerson>().HasOne(s => s.Town)
           .WithMany(g => g.TownMissingPeople).HasForeignKey(s => s.TownId).OnDelete(DeleteBehavior.Restrict);


            builder.Entity<MissingPerson>().HasOne(s => s.Settlement)
           .WithMany(g => g.SettlementMissingPeople).HasForeignKey(s => s.SettlementId).OnDelete(DeleteBehavior.Restrict);

            //missing items
            builder.Entity<MissingItem>().HasOne(s => s.Town)
          .WithMany(g => g.TownMissingItems).HasForeignKey(s => s.TownId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MissingItem>().HasOne(s => s.Settlement)
           .WithMany(g => g.SettlementMissingItems).HasForeignKey(s => s.SettlementId).OnDelete(DeleteBehavior.Restrict);

            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
        }

        private void OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = Enums.AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = Enums.AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = Enums.AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }
    }
}
