using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FixProductTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                 table: "ProductTranslations",
                 columns: new[]
                 {
                    "Id", "ProductId", "LanguageId", "Name", "SeoAlias",
                    "SeoDescription", "SeoTitle", "Description", "Details"
                 },
                 values: new object[,]
                 {
                    {
                        1, 1, "vi-VN", "Áo sơ mi nam trắng Việt Tiến", "ao-so-mi-nam-trang-viet-tien",
                        "Áo sơ mi nam trắng Việt Tiến", "Áo sơ mi nam trắng Việt Tiến",
                        "Áo sơ mi nam trắng Việt Tiến", "Áo sơ mi nam trắng Việt Tiến"
                    },
                    {
                        2, 1, "en-US", "Viet Tien Men T-Shirt", "viet-tien-men-t-shirt",
                        "Viet Tien Men T-Shirt", "Viet Tien Men T-Shirt",
                        "Viet Tien Men T-Shirt", "Viet Tien Men T-Shirt"
                    }
                 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2 });
        }
    }
}
