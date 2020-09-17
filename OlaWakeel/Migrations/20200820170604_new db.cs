using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaWakeel.Migrations
{
    public partial class newdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaseCategories",
                columns: table => new
                {
                    CaseCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    VectorIcon = table.Column<string>(nullable: true),
                    CaseCategoryId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseCategories", x => x.CaseCategoryId);
                    table.ForeignKey(
                        name: "FK_CaseCategories_CaseCategories_CaseCategoryId1",
                        column: x => x.CaseCategoryId1,
                        principalTable: "CaseCategories",
                        principalColumn: "CaseCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DegreeTypes",
                columns: table => new
                {
                    DegreeTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegreeTypes", x => x.DegreeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LicenseDistricts",
                columns: table => new
                {
                    LicenseDistrictId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseDistricts", x => x.LicenseDistrictId);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    SpecializationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecializationName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.SpecializationId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    ProfilePic = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lawyers",
                columns: table => new
                {
                    LawyerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Cnic = table.Column<string>(nullable: true),
                    Biography = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Rating = table.Column<float>(nullable: false),
                    VirtualChargesPkr = table.Column<float>(nullable: false),
                    VirtualChargesUs = table.Column<float>(nullable: false),
                    TotalExperience = table.Column<int>(nullable: false),
                    OnlineStatus = table.Column<string>(nullable: true),
                    ProfilePic = table.Column<string>(nullable: true),
                    CnicFrontPic = table.Column<string>(nullable: true),
                    CnicBackPic = table.Column<string>(nullable: true),
                    RecentDegreePic = table.Column<string>(nullable: true),
                    AppUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lawyers", x => x.LawyerId);
                    table.ForeignKey(
                        name: "FK_Lawyers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    DegreeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PreRequisite = table.Column<int>(nullable: false),
                    EligibleAfter = table.Column<string>(nullable: true),
                    DegreeTypeId = table.Column<int>(nullable: false),
                    DegreeStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.DegreeId);
                    table.ForeignKey(
                        name: "FK_Degrees_DegreeTypes_DegreeTypeId",
                        column: x => x.DegreeTypeId,
                        principalTable: "DegreeTypes",
                        principalColumn: "DegreeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LicenseCities",
                columns: table => new
                {
                    LicenseCityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(nullable: true),
                    LicenseExist = table.Column<bool>(nullable: false),
                    LicenseDistrictId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseCities", x => x.LicenseCityId);
                    table.ForeignKey(
                        name: "FK_LicenseCities_LicenseDistricts_LicenseDistrictId",
                        column: x => x.LicenseDistrictId,
                        principalTable: "LicenseDistricts",
                        principalColumn: "LicenseDistrictId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LawyerAddresses",
                columns: table => new
                {
                    LawyerAddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(nullable: true),
                    Xcoordinate = table.Column<double>(nullable: true),
                    Ycoordinate = table.Column<double>(nullable: true),
                    LawyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerAddresses", x => x.LawyerAddressId);
                    table.ForeignKey(
                        name: "FK_LawyerAddresses_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LawyerCaseCategories",
                columns: table => new
                {
                    LawyerCaseCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseCategoryId = table.Column<int>(nullable: false),
                    LawyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerCaseCategories", x => x.LawyerCaseCategoryId);
                    table.ForeignKey(
                        name: "FK_LawyerCaseCategories_CaseCategories_CaseCategoryId",
                        column: x => x.CaseCategoryId,
                        principalTable: "CaseCategories",
                        principalColumn: "CaseCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LawyerCaseCategories_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LawyerCertificatePics",
                columns: table => new
                {
                    LawyerCertificatePicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificatePic = table.Column<string>(nullable: true),
                    LawyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerCertificatePics", x => x.LawyerCertificatePicId);
                    table.ForeignKey(
                        name: "FK_LawyerCertificatePics_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LawyerClients",
                columns: table => new
                {
                    LawyerClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(nullable: true),
                    LawyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerClients", x => x.LawyerClientId);
                    table.ForeignKey(
                        name: "FK_LawyerClients_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LawyerExperiences",
                columns: table => new
                {
                    LawyerExperienceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseCategoryId = table.Column<int>(nullable: false),
                    ExperienceYears = table.Column<int>(nullable: false),
                    LawyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerExperiences", x => x.LawyerExperienceId);
                    table.ForeignKey(
                        name: "FK_LawyerExperiences_CaseCategories_CaseCategoryId",
                        column: x => x.CaseCategoryId,
                        principalTable: "CaseCategories",
                        principalColumn: "CaseCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LawyerExperiences_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LawyerLanguages",
                columns: table => new
                {
                    LawyerLanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(nullable: true),
                    LawyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerLanguages", x => x.LawyerLanguageId);
                    table.ForeignKey(
                        name: "FK_LawyerLanguages_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LawyerSpecializations",
                columns: table => new
                {
                    LawyerSpecializationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndYear = table.Column<DateTime>(nullable: false),
                    SpecializationId = table.Column<int>(nullable: false),
                    LawyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerSpecializations", x => x.LawyerSpecializationId);
                    table.ForeignKey(
                        name: "FK_LawyerSpecializations_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LawyerSpecializations_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "SpecializationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LawyerQualifications",
                columns: table => new
                {
                    LawyerQualificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletionYear = table.Column<string>(nullable: true),
                    LawyerId = table.Column<int>(nullable: false),
                    DegreeTypeId = table.Column<int>(nullable: false),
                    DegreeId = table.Column<int>(nullable: false),
                    Check = table.Column<bool>(nullable: false),
                    SpecializationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerQualifications", x => x.LawyerQualificationId);
                    table.ForeignKey(
                        name: "FK_LawyerQualifications_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "DegreeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LawyerQualifications_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LawyerQualifications_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "SpecializationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerLicenses",
                columns: table => new
                {
                    LawyerLicenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictBar = table.Column<int>(nullable: false),
                    CityBar = table.Column<int>(nullable: false),
                    Check = table.Column<bool>(nullable: false),
                    LicenseCityId = table.Column<int>(nullable: false),
                    LawyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerLicenses", x => x.LawyerLicenseId);
                    table.ForeignKey(
                        name: "FK_LawyerLicenses_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LawyerLicenses_LicenseCities_LicenseCityId",
                        column: x => x.LicenseCityId,
                        principalTable: "LicenseCities",
                        principalColumn: "LicenseCityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppoinmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LawyerAddressId = table.Column<int>(nullable: true),
                    TimeFrom = table.Column<string>(nullable: true),
                    TimeTo = table.Column<string>(nullable: true),
                    CaseCharges = table.Column<float>(nullable: false),
                    ScheduleDate = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<float>(nullable: false),
                    AppoinmentType = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    LawyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppoinmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_LawyerAddresses_LawyerAddressId",
                        column: x => x.LawyerAddressId,
                        principalTable: "LawyerAddresses",
                        principalColumn: "LawyerAddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LawyerTimings",
                columns: table => new
                {
                    LawyerTimingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(nullable: true),
                    SlotType = table.Column<string>(nullable: true),
                    AppoinmentFee = table.Column<string>(nullable: true),
                    LawyerAddressId = table.Column<int>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Charges = table.Column<float>(nullable: false),
                    Check = table.Column<bool>(nullable: false),
                    Check2 = table.Column<bool>(nullable: false),
                    TimeFrom = table.Column<string>(nullable: true),
                    TimeTo = table.Column<string>(nullable: true),
                    LawyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerTimings", x => x.LawyerTimingId);
                    table.ForeignKey(
                        name: "FK_LawyerTimings_LawyerAddresses_LawyerAddressId",
                        column: x => x.LawyerAddressId,
                        principalTable: "LawyerAddresses",
                        principalColumn: "LawyerAddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LawyerTimings_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_LawyerAddressId",
                table: "Appointments",
                column: "LawyerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_LawyerId",
                table: "Appointments",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CaseCategories_CaseCategoryId1",
                table: "CaseCategories",
                column: "CaseCategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AppUserId",
                table: "Customers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_DegreeTypeId",
                table: "Degrees",
                column: "DegreeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerAddresses_LawyerId",
                table: "LawyerAddresses",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerCaseCategories_CaseCategoryId",
                table: "LawyerCaseCategories",
                column: "CaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerCaseCategories_LawyerId",
                table: "LawyerCaseCategories",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerCertificatePics_LawyerId",
                table: "LawyerCertificatePics",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerClients_LawyerId",
                table: "LawyerClients",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerExperiences_CaseCategoryId",
                table: "LawyerExperiences",
                column: "CaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerExperiences_LawyerId",
                table: "LawyerExperiences",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerLanguages_LawyerId",
                table: "LawyerLanguages",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerLicenses_LawyerId",
                table: "LawyerLicenses",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerLicenses_LicenseCityId",
                table: "LawyerLicenses",
                column: "LicenseCityId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerQualifications_DegreeId",
                table: "LawyerQualifications",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerQualifications_LawyerId",
                table: "LawyerQualifications",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerQualifications_SpecializationId",
                table: "LawyerQualifications",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Lawyers_AppUserId",
                table: "Lawyers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerSpecializations_LawyerId",
                table: "LawyerSpecializations",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerSpecializations_SpecializationId",
                table: "LawyerSpecializations",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerTimings_LawyerAddressId",
                table: "LawyerTimings",
                column: "LawyerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerTimings_LawyerId",
                table: "LawyerTimings",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseCities_LicenseDistrictId",
                table: "LicenseCities",
                column: "LicenseDistrictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LawyerCaseCategories");

            migrationBuilder.DropTable(
                name: "LawyerCertificatePics");

            migrationBuilder.DropTable(
                name: "LawyerClients");

            migrationBuilder.DropTable(
                name: "LawyerExperiences");

            migrationBuilder.DropTable(
                name: "LawyerLanguages");

            migrationBuilder.DropTable(
                name: "LawyerLicenses");

            migrationBuilder.DropTable(
                name: "LawyerQualifications");

            migrationBuilder.DropTable(
                name: "LawyerSpecializations");

            migrationBuilder.DropTable(
                name: "LawyerTimings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CaseCategories");

            migrationBuilder.DropTable(
                name: "LicenseCities");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "LawyerAddresses");

            migrationBuilder.DropTable(
                name: "LicenseDistricts");

            migrationBuilder.DropTable(
                name: "DegreeTypes");

            migrationBuilder.DropTable(
                name: "Lawyers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
