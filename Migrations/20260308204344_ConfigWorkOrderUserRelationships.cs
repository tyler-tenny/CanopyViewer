using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanopyViewer.Migrations
{
    /// <inheritdoc />
    public partial class ConfigWorkOrderUserRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedBy",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "WorkOrders");

            migrationBuilder.AddColumn<int>(
                name: "AssignedById",
                table: "WorkOrders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedToId",
                table: "WorkOrders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_AssignedById",
                table: "WorkOrders",
                column: "AssignedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_AssignedToId",
                table: "WorkOrders",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Users_AssignedById",
                table: "WorkOrders",
                column: "AssignedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Users_AssignedToId",
                table: "WorkOrders",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Users_AssignedById",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Users_AssignedToId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_AssignedById",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_AssignedToId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "AssignedById",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "WorkOrders");

            migrationBuilder.AddColumn<string>(
                name: "AssignedBy",
                table: "WorkOrders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "WorkOrders",
                type: "TEXT",
                nullable: true);
        }
    }
}
