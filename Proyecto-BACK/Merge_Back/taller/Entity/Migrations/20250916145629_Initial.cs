using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ModelSecurity");

            migrationBuilder.EnsureSchema(
                name: "Parameters");

            migrationBuilder.EnsureSchema(
                name: "Entities");

            migrationBuilder.CreateTable(
                name: "AuthSession",
                schema: "ModelSecurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActivityAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AbsoluteExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthSession", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "department",
                schema: "Parameters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    daneCode = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "documentType",
                schema: "Parameters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    abbreviation = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "form",
                schema: "ModelSecurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InspectoraReport",
                schema: "Entities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    report_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_fines = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectoraReport", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "module",
                schema: "ModelSecurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "paymentFrequency",
                schema: "Parameters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intervalPage = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    dueDayOfMonth = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentFrequency", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permission",
                schema: "ModelSecurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "refreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    ReplacedByTokenHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rol",
                schema: "ModelSecurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "typeInfraction",
                schema: "Entities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_Infraction = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    numer_smldv = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typeInfraction", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "userNotification",
                schema: "Entities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userNotification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "valueSmldv",
                schema: "Entities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value_smldv = table.Column<double>(type: "float", nullable: false),
                    Current_Year = table.Column<int>(type: "int", nullable: false),
                    minimunWage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_valueSmldv", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "municipality",
                schema: "Parameters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    daneCode = table.Column<int>(type: "int", nullable: false),
                    departmentId = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipality", x => x.id);
                    table.ForeignKey(
                        name: "FK_municipality_department_departmentId",
                        column: x => x.departmentId,
                        principalSchema: "Parameters",
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "formmodule",
                schema: "ModelSecurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    formid = table.Column<int>(type: "int", nullable: false),
                    moduleid = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formmodule", x => x.id);
                    table.ForeignKey(
                        name: "FK_FormModule_Form",
                        column: x => x.formid,
                        principalSchema: "ModelSecurity",
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormModule_Module",
                        column: x => x.moduleid,
                        principalSchema: "ModelSecurity",
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rolformpermission",
                schema: "ModelSecurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rolid = table.Column<int>(type: "int", nullable: false),
                    formid = table.Column<int>(type: "int", nullable: false),
                    permissionid = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolformpermission", x => x.id);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Form",
                        column: x => x.formid,
                        principalSchema: "ModelSecurity",
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Permission",
                        column: x => x.permissionid,
                        principalSchema: "ModelSecurity",
                        principalTable: "permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Rol",
                        column: x => x.rolid,
                        principalSchema: "ModelSecurity",
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FineCalculationDetail",
                schema: "Entities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    formula = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    porcentaje = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    totalCalculation = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    valueSmldvId = table.Column<int>(type: "int", nullable: false),
                    typeInfractionId = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FineCalculationDetail", x => x.id);
                    table.ForeignKey(
                        name: "FK_TypeInfraction_FineCalculationDetail",
                        column: x => x.typeInfractionId,
                        principalSchema: "Entities",
                        principalTable: "typeInfraction",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ValueSmldv_FineCalculationDetail",
                        column: x => x.valueSmldvId,
                        principalSchema: "Entities",
                        principalTable: "valueSmldv",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "person",
                schema: "ModelSecurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    phoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    tipoUsuario = table.Column<int>(type: "int", nullable: false),
                    municipalityId = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.id);
                    table.ForeignKey(
                        name: "FK_person_municipality_municipalityId",
                        column: x => x.municipalityId,
                        principalSchema: "Parameters",
                        principalTable: "municipality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "ModelSecurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PasswordHash = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    documentTypeId = table.Column<int>(type: "int", nullable: true),
                    documentNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EmailVerificationCode = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    EmailVerificationExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Person",
                        column: x => x.PersonId,
                        principalSchema: "ModelSecurity",
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_documentType_documentTypeId",
                        column: x => x.documentTypeId,
                        principalSchema: "Parameters",
                        principalTable: "documentType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roluser",
                schema: "ModelSecurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    rolId = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roluser", x => x.id);
                    table.ForeignKey(
                        name: "FK_RolUser_Rol",
                        column: x => x.rolId,
                        principalSchema: "ModelSecurity",
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolUser_User",
                        column: x => x.userId,
                        principalSchema: "ModelSecurity",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "userInfraction",
                schema: "Entities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateInfraction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    stateInfraction = table.Column<bool>(type: "bit", nullable: false),
                    observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    typeInfractionId = table.Column<int>(type: "int", nullable: false),
                    UserNotificationId = table.Column<int>(type: "int", nullable: false),
                    amountToPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userInfraction", x => x.id);
                    table.ForeignKey(
                        name: "FK_userInfraction_typeInfraction_typeInfractionId",
                        column: x => x.typeInfractionId,
                        principalSchema: "Entities",
                        principalTable: "typeInfraction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userInfraction_userNotification_UserNotificationId",
                        column: x => x.UserNotificationId,
                        principalSchema: "Entities",
                        principalTable: "userNotification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userInfraction_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "ModelSecurity",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "paymentAgreement",
                schema: "Entities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgreementDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userInfractionId = table.Column<int>(type: "int", nullable: false),
                    paymentFrequencyId = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentAgreement", x => x.id);
                    table.ForeignKey(
                        name: "FK_PaymentAgreement_UserInfraction",
                        column: x => x.userInfractionId,
                        principalSchema: "Entities",
                        principalTable: "userInfraction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_paymentAgreement_paymentFrequency_paymentFrequencyId",
                        column: x => x.paymentFrequencyId,
                        principalSchema: "Parameters",
                        principalTable: "paymentFrequency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentInfraction",
                schema: "Entities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    inspectoraReportId = table.Column<int>(type: "int", nullable: false),
                    PaymentAgreementId = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentInfraction", x => x.id);
                    table.ForeignKey(
                        name: "FK_DocumentInfraction_InspectoraReport",
                        column: x => x.inspectoraReportId,
                        principalSchema: "Entities",
                        principalTable: "InspectoraReport",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentInfraction_PaymentAgreement",
                        column: x => x.PaymentAgreementId,
                        principalSchema: "Entities",
                        principalTable: "paymentAgreement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "typePayment",
                schema: "Entities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    paymentAgreementId = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typePayment", x => x.id);
                    table.ForeignKey(
                        name: "FK_TypePayment_PaymentAgreement",
                        column: x => x.paymentAgreementId,
                        principalSchema: "Entities",
                        principalTable: "paymentAgreement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Entities",
                table: "InspectoraReport",
                columns: new[] { "id", "active", "created_date", "is_deleted", "message", "report_date", "total_fines" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "se integra una nueva multa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2m },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "se integra una nueva multa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3m }
                });

            migrationBuilder.InsertData(
                schema: "Parameters",
                table: "department",
                columns: new[] { "id", "active", "created_date", "daneCode", "is_deleted", "name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, false, "Antioquia" },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 25, false, "Cundinamarca" },
                    { 3, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 76, false, "Valle del Cauca" },
                    { 4, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11, false, "Bogotá, D.C." }
                });

            migrationBuilder.InsertData(
                schema: "Parameters",
                table: "documentType",
                columns: new[] { "id", "abbreviation", "active", "created_date", "is_deleted", "name" },
                values: new object[,]
                {
                    { 1, "CC", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Cédula de Ciudadanía" },
                    { 2, "CE", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Cédula de Extranjería" },
                    { 3, "TI", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Tarjeta de Identidad" },
                    { 4, "PAS", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Pasaporte" }
                });

            migrationBuilder.InsertData(
                schema: "ModelSecurity",
                table: "form",
                columns: new[] { "id", "active", "created_date", "description", "is_deleted", "name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Formulario de creacion de acuerdo de pago", false, "Formulario de acuerdo de pago" },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Formulario para agregar nuevas multas", false, "Formulario de creacion de multas" }
                });

            migrationBuilder.InsertData(
                schema: "ModelSecurity",
                table: "module",
                columns: new[] { "id", "active", "created_date", "description", "is_deleted", "name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Módulo para administración general", false, "Módulo de hacienda" },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Módulo encargado de crear nuevas multas", false, "Módulo de inspectora" },
                    { 3, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "modulo encargado para visualizar las multas inspuestas", false, "Modulo de usuario" }
                });

            migrationBuilder.InsertData(
                schema: "Parameters",
                table: "paymentFrequency",
                columns: new[] { "id", "active", "created_date", "dueDayOfMonth", "intervalPage", "is_deleted" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 16, "UNICA", false },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15, "MENSUAL", false },
                    { 3, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "QUINCENAL", false },
                    { 4, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, "BIMESTRAL", false }
                });

            migrationBuilder.InsertData(
                schema: "ModelSecurity",
                table: "permission",
                columns: new[] { "id", "active", "created_date", "description", "is_deleted", "name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "permiso para leer formularios", false, "Leer" },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "permiso para crear formularios", false, "Crear" },
                    { 3, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "permiso para editar formularios", false, "Editar" },
                    { 4, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "permiso para eliminar lógicamente formularios", false, "Eliminar" },
                    { 5, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "permiso para ver formularios eliminados", false, "VerEliminados" },
                    { 6, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "permiso para recuperar formularios eliminados", false, "Recuperar" }
                });

            migrationBuilder.InsertData(
                schema: "ModelSecurity",
                table: "rol",
                columns: new[] { "id", "active", "created_date", "description", "is_deleted", "name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rol con todos los permisos", false, "Administrador" },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rol estándar para usuarios normales", false, "Usuario" }
                });

            migrationBuilder.InsertData(
                schema: "Entities",
                table: "typeInfraction",
                columns: new[] { "id", "active", "created_date", "description", "is_deleted", "numer_smldv", "type_Infraction" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lanzar basura en un lugar publico", false, 2, "infraccion de tipo uno" },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hacer mucho ruido en un sitio publico", false, 4, "infraccion de tipo dos" },
                    { 3, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Portar armas, elementos cortantes, punzantes, o sustancias peligrosas en áreas comunes o lugares abiertos al público.", false, 8, "infraccion de tipo Tres" },
                    { 4, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Agresión a la autoridad: Agredir o lanzar objetos a las autoridades de policía. ", false, 16, "infraccion de tipo Cuatro" }
                });

            migrationBuilder.InsertData(
                schema: "Entities",
                table: "userNotification",
                columns: new[] { "id", "active", "created_date", "is_deleted", "message", "shippingDate" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "tienes una infraccion por favor acercate antes del 12 de marzo para sucdazanar tu multa o podria iniciar un cobro coativo luego del plazo", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "tienes una infraccion por favor acercate antes del 12 de julio para sucdazanar tu multa o podria iniciar un cobro coativo luego del plazo", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "Entities",
                table: "valueSmldv",
                columns: new[] { "id", "Current_Year", "active", "created_date", "is_deleted", "minimunWage", "value_smldv" },
                values: new object[,]
                {
                    { 1, 2024, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 1300000m, 43.5 },
                    { 2, 2022, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 1100000m, 43.5 }
                });

            migrationBuilder.InsertData(
                schema: "Entities",
                table: "FineCalculationDetail",
                columns: new[] { "id", "active", "created_date", "formula", "is_deleted", "porcentaje", "totalCalculation", "typeInfractionId", "valueSmldvId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "salario minimo * dias = smdlv", false, 0.5m, 100000m, 1, 1 },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "salario minimo * dias = smdlv", false, 0.0m, 150.000m, 2, 2 }
                });

            migrationBuilder.InsertData(
                schema: "ModelSecurity",
                table: "formmodule",
                columns: new[] { "id", "active", "created_date", "formid", "is_deleted", "moduleid" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, false, 1 },
                    { 2, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, false, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Parameters",
                table: "municipality",
                columns: new[] { "id", "active", "created_date", "daneCode", "departmentId", "is_deleted", "name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5001, 1, false, "Medellín" },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 76001, 3, false, "Cali" },
                    { 3, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11001, 4, false, "Bogotá" },
                    { 4, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 25754, 2, false, "Soacha" }
                });

            migrationBuilder.InsertData(
                schema: "ModelSecurity",
                table: "rolformpermission",
                columns: new[] { "id", "formid", "permissionid", "rolid", "active", "created_date", "is_deleted" },
                values: new object[,]
                {
                    { 1, 1, 1, 2, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 2, 1, 2, 2, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 3, 1, 3, 2, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 4, 1, 4, 2, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 5, 1, 1, 1, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 6, 1, 2, 1, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 7, 1, 3, 1, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 8, 1, 4, 1, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 9, 1, 5, 1, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 10, 1, 6, 1, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false }
                });

            migrationBuilder.InsertData(
                schema: "ModelSecurity",
                table: "person",
                columns: new[] { "id", "active", "address", "created_date", "firstName", "is_deleted", "lastName", "municipalityId", "phoneNumber", "tipoUsuario" },
                values: new object[,]
                {
                    { 1, true, "Carrera 10", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Juan", false, "Pérez", 1, "1234567890", 3 },
                    { 2, true, "Carrera 11", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sara", false, "Sofía", 4, "312312314", 3 }
                });

            migrationBuilder.InsertData(
                schema: "ModelSecurity",
                table: "user",
                columns: new[] { "id", "EmailVerificationCode", "EmailVerificationExpiresAt", "EmailVerified", "EmailVerifiedAt", "PasswordHash", "PersonId", "active", "created_date", "documentNumber", "documentTypeId", "email", "is_deleted" },
                values: new object[,]
                {
                    { 1, null, null, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin123", 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "123456789", 1, "camiloandreslosada901@gmail.com", false },
                    { 2, null, null, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "sara12312", 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "0123432121", 2, "sarita@gmail.com", false }
                });

            migrationBuilder.InsertData(
                schema: "ModelSecurity",
                table: "roluser",
                columns: new[] { "id", "rolId", "userId", "active", "created_date", "is_deleted" },
                values: new object[,]
                {
                    { 1, 1, 1, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 2, 2, 2, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false }
                });

            migrationBuilder.InsertData(
                schema: "Entities",
                table: "userInfraction",
                columns: new[] { "id", "UserId", "UserNotificationId", "active", "amountToPay", "created_date", "dateInfraction", "is_deleted", "observations", "stateInfraction", "typeInfractionId" },
                values: new object[,]
                {
                    { 1, 1, 1, true, 0m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "la persona no opuso resistencia a la infraccion", false, 1 },
                    { 2, 2, 2, true, 0m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "la persona se encontraba en estado de embriagues", false, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Entities",
                table: "paymentAgreement",
                columns: new[] { "id", "AgreementDescription", "active", "address", "created_date", "is_deleted", "neighborhood", "paymentFrequencyId", "userInfractionId" },
                values: new object[,]
                {
                    { 1, "se realizará a 4 cuotas de 200.000 los 15 de cada mes desde este momento", true, "carrera 10", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "eduardo santos", 1, 1 },
                    { 2, "se realizará a 2 cuotas de 50.000 los 12 de cada mes desde este momento", true, "carrera 1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "panamá", 2, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Entities",
                table: "DocumentInfraction",
                columns: new[] { "id", "PaymentAgreementId", "active", "created_date", "inspectoraReportId", "is_deleted" },
                values: new object[,]
                {
                    { 1, 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, false },
                    { 2, 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, false }
                });

            migrationBuilder.InsertData(
                schema: "Entities",
                table: "typePayment",
                columns: new[] { "id", "active", "created_date", "is_deleted", "name", "paymentAgreementId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "efectivo", 1 },
                    { 2, true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "nequi", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthSession_SessionId",
                schema: "ModelSecurity",
                table: "AuthSession",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_department_daneCode",
                schema: "Parameters",
                table: "department",
                column: "daneCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_department_name",
                schema: "Parameters",
                table: "department",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentInfraction_inspectoraReportId",
                schema: "Entities",
                table: "DocumentInfraction",
                column: "inspectoraReportId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentInfraction_PaymentAgreementId",
                schema: "Entities",
                table: "DocumentInfraction",
                column: "PaymentAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_documentType_abbreviation",
                schema: "Parameters",
                table: "documentType",
                column: "abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_documentType_name",
                schema: "Parameters",
                table: "documentType",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_FineCalculationDetail_typeInfractionId",
                schema: "Entities",
                table: "FineCalculationDetail",
                column: "typeInfractionId");

            migrationBuilder.CreateIndex(
                name: "IX_FineCalculationDetail_valueSmldvId",
                schema: "Entities",
                table: "FineCalculationDetail",
                column: "valueSmldvId");

            migrationBuilder.CreateIndex(
                name: "IX_form_name",
                schema: "ModelSecurity",
                table: "form",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_formmodule_formid",
                schema: "ModelSecurity",
                table: "formmodule",
                column: "formid");

            migrationBuilder.CreateIndex(
                name: "IX_formmodule_moduleid",
                schema: "ModelSecurity",
                table: "formmodule",
                column: "moduleid");

            migrationBuilder.CreateIndex(
                name: "IX_module_name",
                schema: "ModelSecurity",
                table: "module",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_municipality_daneCode",
                schema: "Parameters",
                table: "municipality",
                column: "daneCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_municipality_departmentId_name",
                schema: "Parameters",
                table: "municipality",
                columns: new[] { "departmentId", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_municipality_name",
                schema: "Parameters",
                table: "municipality",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_paymentAgreement_paymentFrequencyId",
                schema: "Entities",
                table: "paymentAgreement",
                column: "paymentFrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_paymentAgreement_userInfractionId",
                schema: "Entities",
                table: "paymentAgreement",
                column: "userInfractionId");

            migrationBuilder.CreateIndex(
                name: "IX_paymentFrequency_intervalPage",
                schema: "Parameters",
                table: "paymentFrequency",
                column: "intervalPage");

            migrationBuilder.CreateIndex(
                name: "IX_permission_name",
                schema: "ModelSecurity",
                table: "permission",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_person_municipalityId",
                schema: "ModelSecurity",
                table: "person",
                column: "municipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_person_phoneNumber",
                schema: "ModelSecurity",
                table: "person",
                column: "phoneNumber",
                unique: true,
                filter: "[phoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_rol_name",
                schema: "ModelSecurity",
                table: "rol",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rolformpermission_formid",
                schema: "ModelSecurity",
                table: "rolformpermission",
                column: "formid");

            migrationBuilder.CreateIndex(
                name: "IX_rolformpermission_permissionid",
                schema: "ModelSecurity",
                table: "rolformpermission",
                column: "permissionid");

            migrationBuilder.CreateIndex(
                name: "IX_RolFormPermission_Unique",
                schema: "ModelSecurity",
                table: "rolformpermission",
                columns: new[] { "rolid", "formid", "permissionid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roluser_rolId",
                schema: "ModelSecurity",
                table: "roluser",
                column: "rolId");

            migrationBuilder.CreateIndex(
                name: "IX_roluser_userId",
                schema: "ModelSecurity",
                table: "roluser",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_typeInfraction_type_Infraction",
                schema: "Entities",
                table: "typeInfraction",
                column: "type_Infraction",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_typePayment_name",
                schema: "Entities",
                table: "typePayment",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_typePayment_paymentAgreementId",
                schema: "Entities",
                table: "typePayment",
                column: "paymentAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_user_documentTypeId",
                schema: "ModelSecurity",
                table: "user",
                column: "documentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                schema: "ModelSecurity",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_PersonId",
                schema: "ModelSecurity",
                table: "user",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_userInfraction_typeInfractionId",
                schema: "Entities",
                table: "userInfraction",
                column: "typeInfractionId");

            migrationBuilder.CreateIndex(
                name: "IX_userInfraction_UserId",
                schema: "Entities",
                table: "userInfraction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_userInfraction_UserNotificationId",
                schema: "Entities",
                table: "userInfraction",
                column: "UserNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_valueSmldv_Current_Year",
                schema: "Entities",
                table: "valueSmldv",
                column: "Current_Year",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthSession",
                schema: "ModelSecurity");

            migrationBuilder.DropTable(
                name: "DocumentInfraction",
                schema: "Entities");

            migrationBuilder.DropTable(
                name: "FineCalculationDetail",
                schema: "Entities");

            migrationBuilder.DropTable(
                name: "formmodule",
                schema: "ModelSecurity");

            migrationBuilder.DropTable(
                name: "refreshTokens");

            migrationBuilder.DropTable(
                name: "rolformpermission",
                schema: "ModelSecurity");

            migrationBuilder.DropTable(
                name: "roluser",
                schema: "ModelSecurity");

            migrationBuilder.DropTable(
                name: "typePayment",
                schema: "Entities");

            migrationBuilder.DropTable(
                name: "InspectoraReport",
                schema: "Entities");

            migrationBuilder.DropTable(
                name: "valueSmldv",
                schema: "Entities");

            migrationBuilder.DropTable(
                name: "module",
                schema: "ModelSecurity");

            migrationBuilder.DropTable(
                name: "form",
                schema: "ModelSecurity");

            migrationBuilder.DropTable(
                name: "permission",
                schema: "ModelSecurity");

            migrationBuilder.DropTable(
                name: "rol",
                schema: "ModelSecurity");

            migrationBuilder.DropTable(
                name: "paymentAgreement",
                schema: "Entities");

            migrationBuilder.DropTable(
                name: "userInfraction",
                schema: "Entities");

            migrationBuilder.DropTable(
                name: "paymentFrequency",
                schema: "Parameters");

            migrationBuilder.DropTable(
                name: "typeInfraction",
                schema: "Entities");

            migrationBuilder.DropTable(
                name: "userNotification",
                schema: "Entities");

            migrationBuilder.DropTable(
                name: "user",
                schema: "ModelSecurity");

            migrationBuilder.DropTable(
                name: "person",
                schema: "ModelSecurity");

            migrationBuilder.DropTable(
                name: "documentType",
                schema: "Parameters");

            migrationBuilder.DropTable(
                name: "municipality",
                schema: "Parameters");

            migrationBuilder.DropTable(
                name: "department",
                schema: "Parameters");
        }
    }
}
