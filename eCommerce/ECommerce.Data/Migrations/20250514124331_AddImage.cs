using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6b4929d0-890c-47b0-afca-e7b77993d8c5", "AQAAAAIAAYagAAAAEBzop9LV/rLmq7xQ+CqnW9LHnjZtYvH1SvxeMLIbcapcNRHGzybM3BYTLGGnG7tYIA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 5, 14, 22, 43, 30, 938, DateTimeKind.Local).AddTicks(6727));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "06117c53-c54d-4723-a1a5-80b620e21b63", "AQAAAAIAAYagAAAAENcFwfqCx2EMuBQGsNh5g12ZNgkE3q8qdX4b8tGVSfvf2ux+qAKSC7XgqXrdenTP7Q==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 5, 9, 17, 47, 44, 515, DateTimeKind.Local).AddTicks(2977));
        }
    }
}
