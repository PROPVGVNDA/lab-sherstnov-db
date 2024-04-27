using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace lab_sherstnov_db.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pg_catalog.adminpack", ",,");

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    date_joined = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("members_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trainers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    specialization = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("trainers_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    trainer_id = table.Column<int>(type: "integer", nullable: false),
                    class_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    maximum_participants = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("classes_pkey", x => x.id);
                    table.ForeignKey(
                        name: "classes_trainer_id_fkey",
                        column: x => x.trainer_id,
                        principalTable: "trainers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "membertrainer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    trainer_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("membertrainer_pkey", x => x.id);
                    table.ForeignKey(
                        name: "membertrainer_member_id_fkey",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "membertrainer_trainer_id_fkey",
                        column: x => x.trainer_id,
                        principalTable: "trainers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "classregistrations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    class_id = table.Column<int>(type: "integer", nullable: false),
                    registration_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("classregistrations_pkey", x => x.id);
                    table.ForeignKey(
                        name: "classregistrations_class_id_fkey",
                        column: x => x.class_id,
                        principalTable: "classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "classregistrations_member_id_fkey",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_classes_trainer_id",
                table: "classes",
                column: "trainer_id");

            migrationBuilder.CreateIndex(
                name: "IX_classregistrations_class_id",
                table: "classregistrations",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_classregistrations_member_id",
                table: "classregistrations",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "members_email_key",
                table: "members",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "members_phone_key",
                table: "members",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_membertrainer_member_id",
                table: "membertrainer",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_membertrainer_trainer_id",
                table: "membertrainer",
                column: "trainer_id");

            migrationBuilder.CreateIndex(
                name: "trainers_email_key",
                table: "trainers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "trainers_phone_key",
                table: "trainers",
                column: "phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "classregistrations");

            migrationBuilder.DropTable(
                name: "membertrainer");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "trainers");
        }
    }
}
