﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OlaWakeel.Data;

namespace OlaWakeel.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200820170604_new db")]
    partial class newdb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OlaWakeel.Data.ApplicationUser.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("OlaWakeel.Data.ApplicationUser.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("OlaWakeel.Data.ApplicationUser.AppUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("OlaWakeel.Models.Appointment", b =>
                {
                    b.Property<int>("AppoinmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppoinmentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("CaseCharges")
                        .HasColumnType("real");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("LawyerAddressId")
                        .HasColumnType("int");

                    b.Property<int>("LawyerId")
                        .HasColumnType("int");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<DateTime>("ScheduleDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TimeFrom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeTo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppoinmentId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LawyerAddressId");

                    b.HasIndex("LawyerId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("OlaWakeel.Models.CaseCategory", b =>
                {
                    b.Property<int>("CaseCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CaseCategoryId1")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("VectorIcon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseCategoryId");

                    b.HasIndex("CaseCategoryId1");

                    b.ToTable("CaseCategories");
                });

            modelBuilder.Entity("OlaWakeel.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("CustomerId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("OlaWakeel.Models.Degree", b =>
                {
                    b.Property<int>("DegreeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("DegreeStatus")
                        .HasColumnType("bit");

                    b.Property<int>("DegreeTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EligibleAfter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PreRequisite")
                        .HasColumnType("int");

                    b.HasKey("DegreeId");

                    b.HasIndex("DegreeTypeId");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("OlaWakeel.Models.DegreeType", b =>
                {
                    b.Property<int>("DegreeTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DegreeTypeId");

                    b.ToTable("DegreeTypes");
                });

            modelBuilder.Entity("OlaWakeel.Models.Lawyer", b =>
                {
                    b.Property<int>("LawyerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cnic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CnicBackPic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CnicFrontPic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OnlineStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("RecentDegreePic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalExperience")
                        .HasColumnType("int");

                    b.Property<float>("VirtualChargesPkr")
                        .HasColumnType("real");

                    b.Property<float>("VirtualChargesUs")
                        .HasColumnType("real");

                    b.HasKey("LawyerId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Lawyers");
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerAddress", b =>
                {
                    b.Property<int>("LawyerAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LawyerId")
                        .HasColumnType("int");

                    b.Property<double?>("Xcoordinate")
                        .HasColumnType("float");

                    b.Property<double?>("Ycoordinate")
                        .HasColumnType("float");

                    b.HasKey("LawyerAddressId");

                    b.HasIndex("LawyerId");

                    b.ToTable("LawyerAddresses");
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerCaseCategory", b =>
                {
                    b.Property<int>("LawyerCaseCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("LawyerId")
                        .HasColumnType("int");

                    b.HasKey("LawyerCaseCategoryId");

                    b.HasIndex("CaseCategoryId");

                    b.HasIndex("LawyerId");

                    b.ToTable("LawyerCaseCategories");
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerCertificatePic", b =>
                {
                    b.Property<int>("LawyerCertificatePicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CertificatePic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LawyerId")
                        .HasColumnType("int");

                    b.HasKey("LawyerCertificatePicId");

                    b.HasIndex("LawyerId");

                    b.ToTable("LawyerCertificatePics");
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerClient", b =>
                {
                    b.Property<int>("LawyerClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LawyerId")
                        .HasColumnType("int");

                    b.HasKey("LawyerClientId");

                    b.HasIndex("LawyerId");

                    b.ToTable("LawyerClients");
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerExperience", b =>
                {
                    b.Property<int>("LawyerExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ExperienceYears")
                        .HasColumnType("int");

                    b.Property<int>("LawyerId")
                        .HasColumnType("int");

                    b.HasKey("LawyerExperienceId");

                    b.HasIndex("CaseCategoryId");

                    b.HasIndex("LawyerId");

                    b.ToTable("LawyerExperiences");
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerLanguage", b =>
                {
                    b.Property<int>("LawyerLanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LawyerId")
                        .HasColumnType("int");

                    b.HasKey("LawyerLanguageId");

                    b.HasIndex("LawyerId");

                    b.ToTable("LawyerLanguages");
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerLicense", b =>
                {
                    b.Property<int>("LawyerLicenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Check")
                        .HasColumnType("bit");

                    b.Property<int>("CityBar")
                        .HasColumnType("int");

                    b.Property<int>("DistrictBar")
                        .HasColumnType("int");

                    b.Property<int>("LawyerId")
                        .HasColumnType("int");

                    b.Property<int>("LicenseCityId")
                        .HasColumnType("int");

                    b.HasKey("LawyerLicenseId");

                    b.HasIndex("LawyerId");

                    b.HasIndex("LicenseCityId");

                    b.ToTable("LawyerLicenses");
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerQualification", b =>
                {
                    b.Property<int>("LawyerQualificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Check")
                        .HasColumnType("bit");

                    b.Property<string>("CompletionYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DegreeId")
                        .HasColumnType("int");

                    b.Property<int>("DegreeTypeId")
                        .HasColumnType("int");

                    b.Property<int>("LawyerId")
                        .HasColumnType("int");

                    b.Property<int?>("SpecializationId")
                        .HasColumnType("int");

                    b.HasKey("LawyerQualificationId");

                    b.HasIndex("DegreeId");

                    b.HasIndex("LawyerId");

                    b.HasIndex("SpecializationId");

                    b.ToTable("LawyerQualifications");
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerSpecialization", b =>
                {
                    b.Property<int>("LawyerSpecializationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndYear")
                        .HasColumnType("datetime2");

                    b.Property<int>("LawyerId")
                        .HasColumnType("int");

                    b.Property<int>("SpecializationId")
                        .HasColumnType("int");

                    b.HasKey("LawyerSpecializationId");

                    b.HasIndex("LawyerId");

                    b.HasIndex("SpecializationId");

                    b.ToTable("LawyerSpecializations");
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerTiming", b =>
                {
                    b.Property<int>("LawyerTimingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppoinmentFee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Charges")
                        .HasColumnType("real");

                    b.Property<bool>("Check")
                        .HasColumnType("bit");

                    b.Property<bool>("Check2")
                        .HasColumnType("bit");

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LawyerAddressId")
                        .HasColumnType("int");

                    b.Property<int>("LawyerId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SlotType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeFrom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeTo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LawyerTimingId");

                    b.HasIndex("LawyerAddressId");

                    b.HasIndex("LawyerId");

                    b.ToTable("LawyerTimings");
                });

            modelBuilder.Entity("OlaWakeel.Models.LicenseCity", b =>
                {
                    b.Property<int>("LicenseCityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LicenseDistrictId")
                        .HasColumnType("int");

                    b.Property<bool>("LicenseExist")
                        .HasColumnType("bit");

                    b.HasKey("LicenseCityId");

                    b.HasIndex("LicenseDistrictId");

                    b.ToTable("LicenseCities");
                });

            modelBuilder.Entity("OlaWakeel.Models.LicenseDistrict", b =>
                {
                    b.Property<int>("LicenseDistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DistrictName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LicenseDistrictId");

                    b.ToTable("LicenseDistricts");
                });

            modelBuilder.Entity("OlaWakeel.Models.Specialization", b =>
                {
                    b.Property<int>("SpecializationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecializationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpecializationId");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("OlaWakeel.Data.ApplicationUser.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("OlaWakeel.Data.ApplicationUser.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("OlaWakeel.Data.ApplicationUser.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("OlaWakeel.Data.ApplicationUser.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Data.ApplicationUser.AppUserRole", b =>
                {
                    b.HasOne("OlaWakeel.Data.ApplicationUser.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OlaWakeel.Data.ApplicationUser.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.Appointment", b =>
                {
                    b.HasOne("OlaWakeel.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OlaWakeel.Models.LawyerAddress", "LawyerAddress")
                        .WithMany()
                        .HasForeignKey("LawyerAddressId");

                    b.HasOne("OlaWakeel.Models.Lawyer", "Lawyer")
                        .WithMany()
                        .HasForeignKey("LawyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.CaseCategory", b =>
                {
                    b.HasOne("OlaWakeel.Models.CaseCategory", null)
                        .WithMany("Children")
                        .HasForeignKey("CaseCategoryId1");
                });

            modelBuilder.Entity("OlaWakeel.Models.Customer", b =>
                {
                    b.HasOne("OlaWakeel.Data.ApplicationUser.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.Degree", b =>
                {
                    b.HasOne("OlaWakeel.Models.DegreeType", "DegreeTypes")
                        .WithMany()
                        .HasForeignKey("DegreeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.Lawyer", b =>
                {
                    b.HasOne("OlaWakeel.Data.ApplicationUser.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerAddress", b =>
                {
                    b.HasOne("OlaWakeel.Models.Lawyer", "Lawyer")
                        .WithMany("LawyerAddresses")
                        .HasForeignKey("LawyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerCaseCategory", b =>
                {
                    b.HasOne("OlaWakeel.Models.CaseCategory", "CaseCategory")
                        .WithMany()
                        .HasForeignKey("CaseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OlaWakeel.Models.Lawyer", "Lawyer")
                        .WithMany("LawyerCaseCategories")
                        .HasForeignKey("LawyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerCertificatePic", b =>
                {
                    b.HasOne("OlaWakeel.Models.Lawyer", "Lawyer")
                        .WithMany("LawyerCertificatePics")
                        .HasForeignKey("LawyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerClient", b =>
                {
                    b.HasOne("OlaWakeel.Models.Lawyer", "Lawyer")
                        .WithMany("LawyerClients")
                        .HasForeignKey("LawyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerExperience", b =>
                {
                    b.HasOne("OlaWakeel.Models.CaseCategory", "CaseCategory")
                        .WithMany()
                        .HasForeignKey("CaseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OlaWakeel.Models.Lawyer", "Lawyer")
                        .WithMany("LawyerExperiences")
                        .HasForeignKey("LawyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerLanguage", b =>
                {
                    b.HasOne("OlaWakeel.Models.Lawyer", "Lawyer")
                        .WithMany("lawyerLanguages")
                        .HasForeignKey("LawyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerLicense", b =>
                {
                    b.HasOne("OlaWakeel.Models.Lawyer", "Lawyer")
                        .WithMany("LawyerLicenses")
                        .HasForeignKey("LawyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OlaWakeel.Models.LicenseCity", "LicenseCity")
                        .WithMany()
                        .HasForeignKey("LicenseCityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerQualification", b =>
                {
                    b.HasOne("OlaWakeel.Models.Degree", "Degree")
                        .WithMany()
                        .HasForeignKey("DegreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OlaWakeel.Models.Lawyer", "Lawyer")
                        .WithMany("LawyerQualifications")
                        .HasForeignKey("LawyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OlaWakeel.Models.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId");
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerSpecialization", b =>
                {
                    b.HasOne("OlaWakeel.Models.Lawyer", "Lawyer")
                        .WithMany()
                        .HasForeignKey("LawyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OlaWakeel.Models.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.LawyerTiming", b =>
                {
                    b.HasOne("OlaWakeel.Models.LawyerAddress", "LawyerAddress")
                        .WithMany()
                        .HasForeignKey("LawyerAddressId");

                    b.HasOne("OlaWakeel.Models.Lawyer", "Lawyer")
                        .WithMany("LawerTimings")
                        .HasForeignKey("LawyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OlaWakeel.Models.LicenseCity", b =>
                {
                    b.HasOne("OlaWakeel.Models.LicenseDistrict", "LicenseDistrict")
                        .WithMany()
                        .HasForeignKey("LicenseDistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
