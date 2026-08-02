using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddresseditCountery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Country",
                table: "orders",
                newName: "ShippingAddress_Countery");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Countery",
                table: "orders",
                newName: "ShippingAddress_Country");
        }
    }
}
