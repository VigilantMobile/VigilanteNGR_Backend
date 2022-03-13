using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTip;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Models.Identity;
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
        
        //Location
        public DbSet<State> States { get; set; }
        public DbSet<LGA> LGAs { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Settlement> Settlements { get; set; }

        //Identity
        public DbSet<CustomClaims> CustomClaims { get; set; }

        //App Troopers
        public DbSet<SecurityTipCategory> SecurityTipCategories { get; set; }
        public DbSet<SecurityTip> SecurityTips { get; set; }
        //
        public DbSet<BroadcasterType> BroadcasterTypes { get; set; }
        public DbSet<BroadcastLevel> BroadcastLevels { get; set; }
        public DbSet<AlertLevel> AlertLevels { get; set; }

        //Audit
        public DbSet<Audit> AuditLogs { get; set; }

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

            //Location Entities with ApplicationUser Relationships
            //State 4
            builder.Entity<State>().Property(p => p.Boundary).HasColumnType("geography");

            builder.Entity<State>().HasMany(t => t.NPFAdmins)
                   .WithOne(g => g.NPFAdminState)
                   .HasForeignKey(g => g.NPFAdminStateId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<State>().HasMany(t => t.NPFOperators)
                    .WithOne(g => g.NPFOperatorState)
                    .HasForeignKey(g => g.NPFOperatorStateId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<State>().HasMany(t => t.VigilanteAdmins)
                    .WithOne(g => g.VigilanteAdminState)
                    .HasForeignKey(g => g.VigilanteAdminStateId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<State>().HasMany(t => t.VigilanteOperators)
                 .WithOne(g => g.VigilanteOperatorState)
                 .HasForeignKey(g => g.VigilanteOperatorStateId).OnDelete(DeleteBehavior.Restrict);

            //LGA
            builder.Entity<LGA>().Property(p => p.Boundary).HasColumnType("geography");



            builder.Entity<LGA>().HasMany(t => t.NPFAdmins)
                  .WithOne(g => g.NPFAdminLGA)
                  .HasForeignKey(g => g.NPFAdminLGAId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LGA>().HasMany(t => t.NPFOperators)
                    .WithOne(g => g.NPFOperatorLGA)
                    .HasForeignKey(g => g.NPFOperatorLGAId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LGA>().HasMany(t => t.VigilanteAdmins)
                    .WithOne(g => g.VigilanteAdminLGA)
                    .HasForeignKey(g => g.VigilanteAdminLGAId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LGA>().HasMany(t => t.VigilanteOperators)
                     .WithOne(g => g.VigilanteOperatorLGA)
                     .HasForeignKey(g => g.VigilanteOperatorLGAId).OnDelete(DeleteBehavior.Restrict);

            //Town
            builder.Entity<Town>().HasMany(t => t.NPFAdmins)
                  .WithOne(g => g.NPFAdminTown)
                  .HasForeignKey(g => g.NPFAdminTownId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Town>().HasMany(t => t.NPFOperators)
                    .WithOne(g => g.NPFOperatorTown)
                    .HasForeignKey(g => g.NPFOperatorTownId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Town>().HasMany(t => t.VigilanteAdmins)
                    .WithOne(g => g.VigilanteAdminTown)
                    .HasForeignKey(g => g.VigilanteAdminTownId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Town>().HasMany(t => t.VigilanteOperators)
                    .WithOne(g => g.VigilanteOperatorTown)
                    .HasForeignKey(g => g.VigilanteOperatorTownId).OnDelete(DeleteBehavior.Restrict);

            //Settlement
            builder.Entity<Settlement>().HasMany(t => t.NPFAdmins)
                  .WithOne(g => g.NPFAdminSettlement)
                  .HasForeignKey(g => g.NPFAdminSettlementId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Settlement>().HasMany(t => t.NPFOperators)
                    .WithOne(g => g.NPFOperatorSettlement)
                    .HasForeignKey(g => g.NPFOperatorSettlementId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Settlement>().HasMany(t => t.VigilanteAdmins)
                    .WithOne(g => g.VigilanteAdminSettlement)
                    .HasForeignKey(g => g.VigilanteAdminSettlementId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Settlement>().HasMany(t => t.VigilanteOperators)
                    .WithOne(g => g.VigilanteOperatorSettlement)
                    .HasForeignKey(g => g.VigilanteOperatorSettlementId).OnDelete(DeleteBehavior.Restrict);

            //Security Tip -> App User
            builder.Entity<SecurityTip>().HasOne(t => t.AdminAuthorizer)
                 .WithMany(g => g.AdminAuthorizedTips)
                 .HasForeignKey(g => g.AdminAuthorizerID).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SecurityTip>().HasOne(t => t.VGNGAAuthorizer)
                 .WithMany(g => g.VGNGAAuthorizedTips)
                 .HasForeignKey(g => g.VGNGAAuthorizerID).OnDelete(DeleteBehavior.Restrict);


            builder.Entity<SecurityTip>().HasOne(t => t.Broadcaster)
                 .WithMany(g => g.BroadcasterTips)
                 .HasForeignKey(g => g.BroadcasterUserId).OnDelete(DeleteBehavior.Restrict);


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
