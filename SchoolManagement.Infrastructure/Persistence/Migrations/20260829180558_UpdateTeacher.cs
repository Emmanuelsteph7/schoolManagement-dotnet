using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Teachers",
                newName: "EmploymentStatus"
            );

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Teachers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTimeOffset.UtcNow
            );

            migrationBuilder.AddColumn<int>(
                name: "EmailAccountStatus",
                table: "Teachers",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EmailAccountVerifiedDate",
                table: "Teachers",
                type: "timestamp with time zone",
                nullable: true
            );

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Teachers",
                type: "timestamp with time zone",
                nullable: true
            );

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Students",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTimeOffset.UtcNow
            );

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Students",
                type: "timestamp with time zone",
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CreatedAt", table: "Teachers");

            migrationBuilder.DropColumn(name: "EmailAccountStatus", table: "Teachers");

            migrationBuilder.DropColumn(name: "EmailAccountVerifiedDate", table: "Teachers");

            migrationBuilder.DropColumn(name: "UpdatedAt", table: "Teachers");

            migrationBuilder.DropColumn(name: "CreatedAt", table: "Students");

            migrationBuilder.DropColumn(name: "UpdatedAt", table: "Students");

            migrationBuilder.RenameColumn(
                name: "EmploymentStatus",
                table: "Teachers",
                newName: "Status"
            );
        }
    }
}
