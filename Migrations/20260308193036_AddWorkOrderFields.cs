using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanopyViewer.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkOrderFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionTaken",
                table: "WorkOrders",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AssignedBy",
                table: "WorkOrders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextOccurrence",
                table: "WorkOrders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecurCount",
                table: "WorkOrders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecurrenceInterval",
                table: "WorkOrders",
                type: "TEXT",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecurrenceType",
                table: "WorkOrders",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "WorkOrders",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionTaken",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "AssignedBy",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "NextOccurrence",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "RecurCount",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "RecurrenceInterval",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "RecurrenceType",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "WorkOrders");
        }
    }
}
