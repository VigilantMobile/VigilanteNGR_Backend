using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.AppTroopers.Curfew;
using Domain.Entities.AppTroopers.Missing;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.AppTroopers.Subscription;
using Domain.Entities.AppTroopers.Wanted;
using Domain.Entities.CompanyEntities;
using Domain.Entities.Identity;
//using Domain.Entities.Identity.Location;
using Domain.Entities.LocationEntities;
using Infrastructure.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
//using Domain.Entities.Identity.Identity;

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

        public DbSet<DemographicEntitiesCoordinatesJSON> demographicEntitiesCoordinatesJSONs { get; set; }

        //Identity
        public DbSet<CustomClaims> CustomClaims { get; set; }
        //NPF
        //state
        //public DbSet<NPFStateStaff> NPFStateStaff { get; set; }
        //lga
        //town
        //settlement

        //OfficialVigilante
        //public DbSet<OfficialVigilanteStateStaff> OfficialVigilanteStateStaff { get; set; }
        //lga
        //town
        //settlement
        // staff

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
        public DbSet<SourceCategory> SourceCategories { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<SecurityTipStatus> SecurityTipStatuses { get; set; }
        public DbSet<EscalatedTip> EscalatedTips { get; set; }
        
        //Panic
        public DbSet<Panic> PanicRecords { get; set; }
        public DbSet<Commute> CommuteRecords { get; set; }
        public DbSet<TrustedPerson> TrustedPeople { get; set; }

        //Curfew
        public DbSet<Curfew> StateCurfew { get; set; }

        //Missing
        public DbSet<MissingPerson> MissingPerson { get; set; }
        public DbSet<MissingItem> MissingItem { get; set; }

        //Wanted
        public DbSet<WantedPerson> WantedPerson { get; set; }

        //Audit
        public DbSet<Audit> AuditLogs { get; set; }

        //Department
        public DbSet<Department> Departments { get; set; }

        //Comments
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentFlags> CommentFlags { get; set; }

        //Subscriptions
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

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

            //Location Identity External

            //State

            //builder.Entity<ApplicationUser>().HasOne(s => s.CustomerState)
            //.WithMany(g => g.Customers).HasForeignKey(s => s.StateId).OnDelete(DeleteBehavior.Restrict);

            //LGA
            //builder.Entity<ApplicationUser>().HasOne(s => s.CustomerLGA)
            //.WithMany(g => g.Customers).HasForeignKey(s => s.LGAId).OnDelete(DeleteBehavior.Restrict);


            //Settlement
            //builder.Entity<ApplicationUser>().HasOne(s => s.CustomerSettlement)
            // .WithMany(g => g.Customers).HasForeignKey(s => s.SettlementId).OnDelete(DeleteBehavior.Restrict);

            //Location Identity Internal (VGNGA)-----------------------------------------------------------------------

            //State
            builder.Entity<ApplicationUser>()
             .HasMany(x => x.InternalStaffStates)
            .WithMany(x => x.VGNGAStateStaff);

            builder.Entity<ApplicationUser>()
           .HasMany(x => x.InternalStaffLGAs)
           .WithMany(x => x.VGNGALGAStaff);

            //Town
            //builder.Entity<ApplicationUser>().HasOne(s => s.CustomerTown)
            //.WithMany(g => g.Customers).HasForeignKey(s => s.TownId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
            .HasMany(x => x.InternalStaffTowns)
            .WithMany(x => x.VGNGATownStaff);

            builder.Entity<ApplicationUser>()
           .HasMany(x => x.InternalStaffSettlements)
           .WithMany(x => x.VGNGASettlementStaff);

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
            builder.Entity<ApplicationUser>().HasOne(s => s.Department)
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

            //Trusted
            builder.Entity<TrustedPerson>().HasOne(s => s.Owner)
                   .WithMany(g => g.TrustedPeople).HasForeignKey(s => s.OwnerId).OnDelete(DeleteBehavior.Restrict);

            //missing items--------------------------------------------------------------------------------------------------------------------
            //Location
            builder.Entity<MissingItem>().HasOne(s => s.Town)
             .WithMany(g => g.TownMissingItems).HasForeignKey(s => s.TownId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MissingItem>().HasOne(s => s.Settlement)
           .WithMany(g => g.SettlementMissingItems).HasForeignKey(s => s.SettlementId).OnDelete(DeleteBehavior.Restrict);

            //users

            builder.Entity<MissingItem>().HasOne(s => s.ApplicationUser)
            .WithMany(g => g.CustomerMissingItems).HasForeignKey(s => s.LoserId).OnDelete(DeleteBehavior.Restrict);

            //missing persons-------------------------------------------------------------------------------------------------------------------
            // Location
            builder.Entity<MissingPerson>().HasOne(s => s.Town)
             .WithMany(g => g.TownMissingPeople).HasForeignKey(s => s.TownId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MissingPerson>().HasOne(s => s.Settlement)
           .WithMany(g => g.SettlementMissingPeople).HasForeignKey(s => s.SettlementId).OnDelete(DeleteBehavior.Restrict);

            //users
            builder.Entity<MissingPerson>().HasOne(s => s.ApplicationUser)
            .WithMany(g => g.CustomerMissingPeople).HasForeignKey(s => s.LoserId).OnDelete(DeleteBehavior.Restrict);


            //SecurityTips

            builder.Entity<Source>().HasOne(s => s.SourceCategory)
              .WithMany(g => g.Sources).HasForeignKey(s => s.SourceCategoryId).OnDelete(DeleteBehavior.Restrict);


            builder.Entity<SecurityTip>().HasOne(s => s.ApplicationUser)
              .WithMany(g => g.CustomerSecurityTips).HasForeignKey(s => s.BroadcasterId).OnDelete(DeleteBehavior.Restrict);

            //Tip Status
            builder.Entity<SecurityTipStatus>().HasMany(s => s.SecurityTips)
           .WithOne(g => g.SecurityTipStatus).HasForeignKey(s => s.SecurityTipStatusId).OnDelete(DeleteBehavior.Restrict);

            // builder.Entity<SecurityTip>().HasOne(s => s.ApplicationUser)
            //.WithMany(g => g.CustomerSecurityTips).HasForeignKey(s => s.BroadcasterId).OnDelete(DeleteBehavior.Restrict);
            //users
            builder.Entity<SecurityTip>().HasOne(s => s.ExternalInitiator)
            .WithMany(g => g.ExternalStaffIniatedTips).HasForeignKey(s => s.ExternalInitiatorId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SecurityTip>().HasOne(s => s.ExternalAuthorizer)
             .WithMany(g => g.ExternalStaffAuthorizedTips).HasForeignKey(s => s.ExternalAuthorizerId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SecurityTip>().HasMany(s => s.Comments)
            .WithOne(g => g.SecurityTip).HasForeignKey(s => s.SecurityTipId).OnDelete(DeleteBehavior.Restrict);

            //Curfew
            builder.Entity<Curfew>().HasOne(s => s.AdminAuthorizer)
           .WithMany(g => g.AdminAuthorizedCurfews).HasForeignKey(s => s.AdminAuthorizerId).OnDelete(DeleteBehavior.Restrict).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Curfew>().HasOne(s => s.OperatorIniator)
            .WithMany(g => g.OperatorIniatedCurfews).HasForeignKey(s => s.OperatorIniatorId).OnDelete(DeleteBehavior.Restrict).OnDelete(DeleteBehavior.Restrict);

            //BroadcastLevel

            builder.Entity<Town>().HasMany(s => s.TownResidents)
             .WithOne(g => g.CustomerTown).HasForeignKey(s => s.TownId).OnDelete(DeleteBehavior.Restrict);

            //Subscription

            //builder.Entity<Subscription>().HasMany(s => s.SubscribedUsers)
            // .WithOne(g => g.Subscription).HasForeignKey(s => s.SubscriptionId).OnDelete(DeleteBehavior.Restrict);

            //Comments
            builder.Entity<Comment>().HasMany(s => s.CommentFlags)
             .WithOne(g => g.Comment).HasForeignKey(s => s.CommentId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>().HasMany(s => s.CommentFlags)
            .WithOne(g => g.Comment).HasForeignKey(s => s.CommentId).OnDelete(DeleteBehavior.Restrict);

            //Wallet
            builder.Entity<ApplicationUser>().HasOne(e => e.Wallet).WithOne(e => e.Customer).OnDelete(DeleteBehavior.Cascade);


            builder.Entity<ApplicationUser>().HasMany(s => s.Comments)
           .WithOne(g => g.ApplicationUser).HasForeignKey(s => s.CommenterId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>().HasMany(s => s.CommentFlags)
           .WithOne(g => g.ApplicationUser).HasForeignKey(s => s.VoterId).OnDelete(DeleteBehavior.Restrict);

            //Sources
            builder.Entity<Source>().HasMany(s => s.SecurityTips)
          .WithOne(g => g.Source).HasForeignKey(s => s.SourceId).OnDelete(DeleteBehavior.Restrict);

            //Escalated Tips
            builder.Entity<SecurityTip>().HasMany(s => s.EscalatedTips)
          .WithOne(g => g.SecurityTip).HasForeignKey(s => s.SecurityTipId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BroadcastLevel>().HasMany(s => s.EscalatedTips)
          .WithOne(g => g.EscalationBroadcastLevel).HasForeignKey(s => s.EscalationBroadcastLevelId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>().HasMany(s => s.ApprovedEscalatedTips)
          .WithOne(g => g.EscalationAuthorizer).HasForeignKey(s => s.EscalationAuthorizerID).OnDelete(DeleteBehavior.Restrict);

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
                            auditEntry.AuditType = Domain.Common.Enums.AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = Domain.Common.Enums.AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = Domain.Common.Enums.AuditType.Update;
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
