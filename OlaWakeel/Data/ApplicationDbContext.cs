using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data.ApplicationUser;
using OlaWakeel.Models;

namespace OlaWakeel.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>,
    AppUserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<LawyerTiming> LawyerTimings { get; set; }
        public DbSet<LawyerSpecialization> LawyerSpecializations { get; set; }
        public DbSet<LawyerQualification> LawyerQualifications { get; set; }
        public DbSet<LawyerClient> LawyerClients { get; set; }
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<LawyerExperience> LawyerExperiences { get; set; }
        public DbSet<CaseCategory> CaseCategories { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<LawyerCaseCategory> LawyerCaseCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LawyerLanguage> LawyerLanguages { get; set; }
        public DbSet<LawyerLicense> LawyerLicenses { get; set; }
        public DbSet<LicenseDistrict> LicenseDistricts { get; set; }
        public DbSet<LicenseCity> LicenseCities { get; set; }
        public DbSet<DegreeType> DegreeTypes { get; set; }
        public DbSet<LawyerAddress> LawyerAddresses { get; set; }
        public DbSet<LawyerCertificatePic> LawyerCertificatePics { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletHistory> WalletHistories { get; set; }
        public DbSet<FavouriteLawyer> FavouriteLawyers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<RescheduleAppoint> RescheduleAppoints { get; set; }
        public DbSet<PaymentMethods> PaymentMethods { get; set; }
        public DbSet<Withdraw> Withdraws { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}
