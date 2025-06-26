using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FixTranslationSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a765c5a2-95c2-43c2-9eac-96df226f4fd6", "AQAAAAIAAYagAAAAEG4VXFUlfTmn999PhHgicu45Pk68XA64iyYuGq8qSvzH0R4/igl3MBuCsLS++U3VXg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 6, 26, 1, 11, 53, 376, DateTimeKind.Local).AddTicks(2994));

            migrationBuilder.InsertData(
                table: "CategoryTranslations",
                columns: new[] { "Id", "CategoryId", "LanguageId", "Name", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
            { 1, 1, "vi-VN", "Áo nam", "ao-nam", "Sản phẩm áo thời trang nam", "Sản phẩm áo thời trang nam" },
            { 2, 1, "en-US", "Men Shirt", "men-shirt", "The shirt products for men", "The shirt products for men" },
            { 3, 2, "vi-VN", "Áo nữ", "ao-nu", "Sản phẩm áo thời trang nữ", "Sản phẩm áo thời trang women" },
            { 4, 2, "en-US", "Women Shirt", "women-shirt", "The shirt products for women", "The shirt products for women" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5b169172-e297-48e6-93db-4cb1b266b437", "AQAAAAIAAYagAAAAEIuU5XsmwM99XMseKFDeDM+HNxpQ7Z7WMbCtgtG/wn+ia9Enp3KedRM/7za7Qrpn3A==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 6, 20, 22, 1, 29, 538, DateTimeKind.Local).AddTicks(5093));

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });
        }
    }
}
