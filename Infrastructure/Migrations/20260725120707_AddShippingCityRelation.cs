using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShippingCityRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingCity",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "ShippingCityId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingCityId",
                table: "Orders",
                column: "ShippingCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Lookups_ShippingCityId",
                table: "Orders",
                column: "ShippingCityId",
                principalTable: "Lookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Lookups_ShippingCityId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingCityId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingCityId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ShippingCity",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
