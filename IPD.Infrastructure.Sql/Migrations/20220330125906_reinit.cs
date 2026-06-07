using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class reinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    AllergiesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllergiesName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.AllergiesID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosis",
                columns: table => new
                {
                    DiagnosisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagnosisName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosis", x => x.DiagnosisID);
                });

            migrationBuilder.CreateTable(
                name: "DiagonosisExaminations",
                columns: table => new
                {
                    DigonosisExaminationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DigonosisExaminationsName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagonosisExaminations", x => x.DigonosisExaminationID);
                });

            migrationBuilder.CreateTable(
                name: "Directions",
                columns: table => new
                {
                    DirectionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DirectionDetails = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directions", x => x.DirectionID);
                });

            migrationBuilder.CreateTable(
                name: "DischargeStatuses",
                columns: table => new
                {
                    DischargeStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DischargeStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DischargeStatuses", x => x.DischargeStatusID);
                });

            migrationBuilder.CreateTable(
                name: "ICDDigonosisCodes",
                columns: table => new
                {
                    DiseaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ICDCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ParentsID = table.Column<int>(type: "int", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICDDigonosisCodes", x => x.DiseaseID);
                });

            migrationBuilder.CreateTable(
                name: "Intervals",
                columns: table => new
                {
                    IntervalID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IntervalName = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervals", x => x.IntervalID);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    LanguageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageID);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    MedicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.MedicationID);
                });

            migrationBuilder.CreateTable(
                name: "Ncds",
                columns: table => new
                {
                    NcdsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NcdName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ncds", x => x.NcdsID);
                });

            migrationBuilder.CreateTable(
                name: "Procedure",
                columns: table => new
                {
                    ProcedureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcedureName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure", x => x.ProcedureID);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionID);
                });

            migrationBuilder.CreateTable(
                name: "SurgeryTypes",
                columns: table => new
                {
                    SurgeryTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgeryTypes", x => x.SurgeryTypeID);
                });

            migrationBuilder.CreateTable(
                name: "SurgicalProcedures",
                columns: table => new
                {
                    SurgicalProcedureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcedureName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgicalProcedures", x => x.SurgicalProcedureID);
                });

            migrationBuilder.CreateTable(
                name: "Tinkhundla",
                columns: table => new
                {
                    TinkhundlaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tinkhundla", x => x.TinkhundlaID);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityID);
                    table.ForeignKey(
                        name: "FK_Facilities_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    UserAccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DOB = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Sex = table.Column<byte>(type: "tinyint", nullable: false),
                    CellphoneCountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LandPhoneCountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    LandPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ContactAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityID = table.Column<int>(type: "int", nullable: false),
                    IsAdministrator = table.Column<bool>(type: "bit", nullable: false),
                    IsAccountActive = table.Column<bool>(type: "bit", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.UserAccountID);
                    table.ForeignKey(
                        name: "FK_UserAccounts_Facilities_FacilityID",
                        column: x => x.FacilityID,
                        principalTable: "Facilities",
                        principalColumn: "FacilityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecoveryRequests",
                columns: table => new
                {
                    RecoveryRequestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CellphoneCountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateRequested = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    IsTicketOpen = table.Column<bool>(type: "bit", nullable: false),
                    UserAccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecoveryRequests", x => x.RecoveryRequestID);
                    table.ForeignKey(
                        name: "FK_RecoveryRequests_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "UserAccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRights",
                columns: table => new
                {
                    UserRightID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Module = table.Column<byte>(type: "tinyint", nullable: false),
                    UserAccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRights", x => x.UserRightID);
                    table.ForeignKey(
                        name: "FK_UserRights_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "UserAccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admissions",
                columns: table => new
                {
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionDate = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    AdmissionTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    AssaignDoctor = table.Column<string>(type: "nvarchar(92)", maxLength: 92, nullable: false),
                    NextOfKin = table.Column<string>(type: "nvarchar(95)", maxLength: 95, nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(95)", maxLength: 95, nullable: false),
                    ContactAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CellphoneCountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IsDischarged = table.Column<bool>(type: "bit", nullable: false),
                    PatientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => x.AdmissionID);
                });

            migrationBuilder.CreateTable(
                name: "Chiefdoms",
                columns: table => new
                {
                    ChiefdomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    TinkhundlaID = table.Column<int>(type: "int", nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chiefdoms", x => x.ChiefdomID);
                    table.ForeignKey(
                        name: "FK_Chiefdoms_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID");
                    table.ForeignKey(
                        name: "FK_Chiefdoms_Tinkhundla_TinkhundlaID",
                        column: x => x.TinkhundlaID,
                        principalTable: "Tinkhundla",
                        principalColumn: "TinkhundlaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    ComplaintID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplaintName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ComplaintHistory = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SystemsReview = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Diabetes = table.Column<byte>(type: "tinyint", nullable: false),
                    Hypertention = table.Column<byte>(type: "tinyint", nullable: false),
                    Epilepsy = table.Column<byte>(type: "tinyint", maxLength: 1000, nullable: false),
                    SpecialNote = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.ComplaintID);
                    table.ForeignKey(
                        name: "FK_Complaints_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeathCertificates",
                columns: table => new
                {
                    DeathCertificateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Indvuna = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateOfDeath = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    TimeOfDeath = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    CauseOfDeath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OtherReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Interval = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SpecialInvistigation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    HandOn = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    HandOver = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Resident = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeathCertificates", x => x.DeathCertificateID);
                    table.ForeignKey(
                        name: "FK_DeathCertificates_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiabeticProfiles",
                columns: table => new
                {
                    DiabeticProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCollected = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    TimeCollected = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    BloodSuger = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UrinSuger = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UrinKetones = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsulinDose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiabeticProfiles", x => x.DiabeticProfileID);
                    table.ForeignKey(
                        name: "FK_DiabeticProfiles_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discharges",
                columns: table => new
                {
                    DischargeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DischargeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DischargeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Advice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DietNutritionAdvice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicationAdvice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalDiagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DischargeStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DischargeStatusesDischargeStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discharges", x => x.DischargeID);
                    table.ForeignKey(
                        name: "FK_Discharges_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Discharges_DischargeStatuses_DischargeStatusesDischargeStatusID",
                        column: x => x.DischargeStatusesDischargeStatusID,
                        principalTable: "DischargeStatuses",
                        principalColumn: "DischargeStatusID");
                });

            migrationBuilder.CreateTable(
                name: "DoctorsNotes",
                columns: table => new
                {
                    DoctorsNoteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfNote = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    TimeOfNote = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TestRequest = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsNotes", x => x.DoctorsNoteID);
                    table.ForeignKey(
                        name: "FK_DoctorsNotes_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterDepartmentReferrals",
                columns: table => new
                {
                    InterDepartmentReferralsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferralTo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Date = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Time = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReasonOfReferral = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ReferralOfficer = table.Column<string>(type: "nvarchar(92)", maxLength: 92, nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ConsultingOfficer = table.Column<string>(type: "nvarchar(92)", maxLength: 92, nullable: true),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterDepartmentReferrals", x => x.InterDepartmentReferralsID);
                    table.ForeignKey(
                        name: "FK_InterDepartmentReferrals_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterDepartmentReferrals_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalReferrals",
                columns: table => new
                {
                    LocalReferralID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phalala = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CivilServent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmploymentNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReferralType = table.Column<byte>(type: "tinyint", nullable: false),
                    ReferringSpecialist = table.Column<string>(type: "nvarchar(92)", maxLength: 92, nullable: false),
                    PracticeNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Discipline = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReasonReferral = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ShortHistory = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Investigation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ContactDetails = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Date = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Time = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    PatientsTransferApparatus = table.Column<byte>(type: "tinyint", nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcedureID = table.Column<int>(type: "int", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalReferrals", x => x.LocalReferralID);
                    table.ForeignKey(
                        name: "FK_LocalReferrals_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalReferrals_Procedure_ProcedureID",
                        column: x => x.ProcedureID,
                        principalTable: "Procedure",
                        principalColumn: "ProcedureID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationPlans",
                columns: table => new
                {
                    MedicationPlanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dose = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Durations = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    MedicationsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IntervalsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DirectionsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationPlans", x => x.MedicationPlanID);
                    table.ForeignKey(
                        name: "FK_MedicationPlans_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationPlans_Directions_DirectionsID",
                        column: x => x.DirectionsID,
                        principalTable: "Directions",
                        principalColumn: "DirectionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationPlans_Intervals_IntervalsID",
                        column: x => x.IntervalsID,
                        principalTable: "Intervals",
                        principalColumn: "IntervalID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationPlans_Medications_MedicationsID",
                        column: x => x.MedicationsID,
                        principalTable: "Medications",
                        principalColumn: "MedicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NursingCares",
                columns: table => new
                {
                    NursingCareID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfCare = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    TimeOfCare = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Problem = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Objective = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Intervension = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Rational = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Evaluation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FacilityCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingCares", x => x.NursingCareID);
                    table.ForeignKey(
                        name: "FK_NursingCares_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientDetails",
                columns: table => new
                {
                    PatientDetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Relegion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Employer = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChronicIllness = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Medication = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDetails", x => x.PatientDetailsID);
                    table.ForeignKey(
                        name: "FK_PatientDetails_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientDetails_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientDiagnosis",
                columns: table => new
                {
                    PatientDiagnosisID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnosisNote = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DiseaseID = table.Column<int>(type: "int", nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ICDDigonosisCodesDiseaseID = table.Column<int>(type: "int", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDiagnosis", x => x.PatientDiagnosisID);
                    table.ForeignKey(
                        name: "FK_PatientDiagnosis_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientDiagnosis_ICDDigonosisCodes_ICDDigonosisCodesDiseaseID",
                        column: x => x.ICDDigonosisCodesDiseaseID,
                        principalTable: "ICDDigonosisCodes",
                        principalColumn: "DiseaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientExaminations",
                columns: table => new
                {
                    PatientExaminationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Findings = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DigonosisExaminationID = table.Column<int>(type: "int", nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientExaminations", x => x.PatientExaminationID);
                    table.ForeignKey(
                        name: "FK_PatientExaminations_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientExaminations_DiagonosisExaminations_DigonosisExaminationID",
                        column: x => x.DigonosisExaminationID,
                        principalTable: "DiagonosisExaminations",
                        principalColumn: "DigonosisExaminationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Surgeries",
                columns: table => new
                {
                    SurgeryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurgeryDate = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    SurgeryTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    HasPatientsConcent = table.Column<bool>(type: "bit", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AnaesthetistAssessment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    OtherSurgeryType = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SurgeryTeam = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProcedureIndication = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SurgeryTypeID = table.Column<int>(type: "int", nullable: false),
                    SurgicalProcedureID = table.Column<int>(type: "int", nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surgeries", x => x.SurgeryID);
                    table.ForeignKey(
                        name: "FK_Surgeries_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surgeries_SurgeryTypes_SurgeryTypeID",
                        column: x => x.SurgeryTypeID,
                        principalTable: "SurgeryTypes",
                        principalColumn: "SurgeryTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surgeries_SurgicalProcedures_SurgicalProcedureID",
                        column: x => x.SurgicalProcedureID,
                        principalTable: "SurgicalProcedures",
                        principalColumn: "SurgicalProcedureID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentPlans",
                columns: table => new
                {
                    TreatmentPlanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatementPlanDetails = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentPlans", x => x.TreatmentPlanID);
                    table.ForeignKey(
                        name: "FK_TreatmentPlans_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vitals",
                columns: table => new
                {
                    VitalID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCollected = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    TimeCollected = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MUAC = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Systolic = table.Column<short>(type: "smallint", nullable: false),
                    Diastolic = table.Column<short>(type: "smallint", nullable: false),
                    RespiratoryRate = table.Column<short>(type: "smallint", nullable: true),
                    Pulse = table.Column<short>(type: "smallint", nullable: false),
                    OxygenSaturation = table.Column<short>(type: "smallint", nullable: true),
                    BMI = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NutritionalStatus = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: true),
                    OtherVitals = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FacilityCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitals", x => x.VitalID);
                    table.ForeignKey(
                        name: "FK_Vitals_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UHID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NationalID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DOB = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Sex = table.Column<byte>(type: "tinyint", nullable: false),
                    MaritalStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    ContactAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PostalAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CellphoneCountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LandPhoneCountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    LandPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    IsDeceased = table.Column<bool>(type: "bit", nullable: false),
                    DateDeceased = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    ChiefdomID = table.Column<int>(type: "int", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientID);
                    table.ForeignKey(
                        name: "FK_Patients_Chiefdoms_ChiefdomID",
                        column: x => x.ChiefdomID,
                        principalTable: "Chiefdoms",
                        principalColumn: "ChiefdomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientAllergies",
                columns: table => new
                {
                    PatientAllergiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllergiesID = table.Column<int>(type: "int", nullable: false),
                    ComplaintID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAllergies", x => x.PatientAllergiesID);
                    table.ForeignKey(
                        name: "FK_PatientAllergies_Allergies_AllergiesID",
                        column: x => x.AllergiesID,
                        principalTable: "Allergies",
                        principalColumn: "AllergiesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientAllergies_Complaints_ComplaintID",
                        column: x => x.ComplaintID,
                        principalTable: "Complaints",
                        principalColumn: "ComplaintID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientsNcds",
                columns: table => new
                {
                    PatientNcdsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NcdsID = table.Column<int>(type: "int", nullable: false),
                    ComplaintID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsNcds", x => x.PatientNcdsID);
                    table.ForeignKey(
                        name: "FK_PatientsNcds_Complaints_ComplaintID",
                        column: x => x.ComplaintID,
                        principalTable: "Complaints",
                        principalColumn: "ComplaintID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientsNcds_Ncds_NcdsID",
                        column: x => x.NcdsID,
                        principalTable: "Ncds",
                        principalColumn: "NcdsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientTransferApparatuses",
                columns: table => new
                {
                    PatientTransferApparatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocalReferralID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApparatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTransferApparatuses", x => x.PatientTransferApparatusId);
                    table.ForeignKey(
                        name: "FK_PatientTransferApparatuses_LocalReferrals_LocalReferralID",
                        column: x => x.LocalReferralID,
                        principalTable: "LocalReferrals",
                        principalColumn: "LocalReferralID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostSurgeries",
                columns: table => new
                {
                    PostSurgeryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurgeryDetails = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Findings = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PostSurgeryPlan = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PatientsCondition = table.Column<byte>(type: "tinyint", nullable: false),
                    SurgeryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSurgeries", x => x.PostSurgeryID);
                    table.ForeignKey(
                        name: "FK_PostSurgeries_Surgeries_SurgeryID",
                        column: x => x.SurgeryID,
                        principalTable: "Surgeries",
                        principalColumn: "SurgeryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_PatientID",
                table: "Admissions",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Chiefdoms_AdmissionID",
                table: "Chiefdoms",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Chiefdoms_TinkhundlaID",
                table: "Chiefdoms",
                column: "TinkhundlaID");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_AdmissionID",
                table: "Complaints",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_DeathCertificates_AdmissionID",
                table: "DeathCertificates",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_DiabeticProfiles_AdmissionID",
                table: "DiabeticProfiles",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Discharges_AdmissionID",
                table: "Discharges",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Discharges_DischargeStatusesDischargeStatusID",
                table: "Discharges",
                column: "DischargeStatusesDischargeStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsNotes_AdmissionID",
                table: "DoctorsNotes",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_RegionID",
                table: "Facilities",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_InterDepartmentReferrals_AdmissionID",
                table: "InterDepartmentReferrals",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_InterDepartmentReferrals_DepartmentID",
                table: "InterDepartmentReferrals",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_LocalReferrals_AdmissionID",
                table: "LocalReferrals",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_LocalReferrals_ProcedureID",
                table: "LocalReferrals",
                column: "ProcedureID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPlans_AdmissionID",
                table: "MedicationPlans",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPlans_DirectionsID",
                table: "MedicationPlans",
                column: "DirectionsID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPlans_IntervalsID",
                table: "MedicationPlans",
                column: "IntervalsID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPlans_MedicationsID",
                table: "MedicationPlans",
                column: "MedicationsID");

            migrationBuilder.CreateIndex(
                name: "IX_NursingCares_AdmissionID",
                table: "NursingCares",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAllergies_AllergiesID",
                table: "PatientAllergies",
                column: "AllergiesID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAllergies_ComplaintID",
                table: "PatientAllergies",
                column: "ComplaintID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDetails_AdmissionID",
                table: "PatientDetails",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDetails_RegionID",
                table: "PatientDetails",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiagnosis_AdmissionID",
                table: "PatientDiagnosis",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiagnosis_ICDDigonosisCodesDiseaseID",
                table: "PatientDiagnosis",
                column: "ICDDigonosisCodesDiseaseID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminations_AdmissionID",
                table: "PatientExaminations",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminations_DigonosisExaminationID",
                table: "PatientExaminations",
                column: "DigonosisExaminationID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ChiefdomID",
                table: "Patients",
                column: "ChiefdomID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CountryID",
                table: "Patients",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsNcds_ComplaintID",
                table: "PatientsNcds",
                column: "ComplaintID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsNcds_NcdsID",
                table: "PatientsNcds",
                column: "NcdsID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTransferApparatuses_LocalReferralID",
                table: "PatientTransferApparatuses",
                column: "LocalReferralID");

            migrationBuilder.CreateIndex(
                name: "IX_PostSurgeries_SurgeryID",
                table: "PostSurgeries",
                column: "SurgeryID");

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryRequests_UserAccountID",
                table: "RecoveryRequests",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeries_AdmissionID",
                table: "Surgeries",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeries_SurgeryTypeID",
                table: "Surgeries",
                column: "SurgeryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeries_SurgicalProcedureID",
                table: "Surgeries",
                column: "SurgicalProcedureID");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPlans_AdmissionID",
                table: "TreatmentPlans",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_FacilityID",
                table: "UserAccounts",
                column: "FacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRights_UserAccountID",
                table: "UserRights",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Vitals_AdmissionID",
                table: "Vitals",
                column: "AdmissionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Patients_PatientID",
                table: "Admissions",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Patients_PatientID",
                table: "Admissions");

            migrationBuilder.DropTable(
                name: "DeathCertificates");

            migrationBuilder.DropTable(
                name: "DiabeticProfiles");

            migrationBuilder.DropTable(
                name: "Diagnosis");

            migrationBuilder.DropTable(
                name: "Discharges");

            migrationBuilder.DropTable(
                name: "DoctorsNotes");

            migrationBuilder.DropTable(
                name: "InterDepartmentReferrals");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "MedicationPlans");

            migrationBuilder.DropTable(
                name: "NursingCares");

            migrationBuilder.DropTable(
                name: "PatientAllergies");

            migrationBuilder.DropTable(
                name: "PatientDetails");

            migrationBuilder.DropTable(
                name: "PatientDiagnosis");

            migrationBuilder.DropTable(
                name: "PatientExaminations");

            migrationBuilder.DropTable(
                name: "PatientsNcds");

            migrationBuilder.DropTable(
                name: "PatientTransferApparatuses");

            migrationBuilder.DropTable(
                name: "PostSurgeries");

            migrationBuilder.DropTable(
                name: "RecoveryRequests");

            migrationBuilder.DropTable(
                name: "TreatmentPlans");

            migrationBuilder.DropTable(
                name: "UserRights");

            migrationBuilder.DropTable(
                name: "Vitals");

            migrationBuilder.DropTable(
                name: "DischargeStatuses");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Directions");

            migrationBuilder.DropTable(
                name: "Intervals");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "ICDDigonosisCodes");

            migrationBuilder.DropTable(
                name: "DiagonosisExaminations");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "Ncds");

            migrationBuilder.DropTable(
                name: "LocalReferrals");

            migrationBuilder.DropTable(
                name: "Surgeries");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropTable(
                name: "Procedure");

            migrationBuilder.DropTable(
                name: "SurgeryTypes");

            migrationBuilder.DropTable(
                name: "SurgicalProcedures");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Chiefdoms");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Admissions");

            migrationBuilder.DropTable(
                name: "Tinkhundla");
        }
    }
}
