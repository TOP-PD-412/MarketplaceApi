using System.Globalization;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.Migrations
{
    /// <inheritdoc />
    public partial class FixSmthn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<BigInteger>(
                name: "balance",
                table: "users",
                type: "numeric",
                nullable: false,
                defaultValue: BigInteger.Parse("0", NumberFormatInfo.InvariantInfo));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "balance",
                table: "users");
        }
    }
}
