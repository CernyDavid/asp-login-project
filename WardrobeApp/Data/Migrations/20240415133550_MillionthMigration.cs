using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WardrobeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MillionthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("9ac796a2-bfc0-4495-8737-a7ad0ad2a01b"), new Guid("2530cada-64a9-43ec-9024-d984f8529f1e") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9ac796a2-bfc0-4495-8737-a7ad0ad2a01b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2530cada-64a9-43ec-9024-d984f8529f1e"));

            migrationBuilder.CreateTable(
                name: "Outfits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateOnly>(type: "date", nullable: false),
                    HeadwearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BottomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FootwearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outfits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Outfits_ClothingItems_AccessoryId",
                        column: x => x.AccessoryId,
                        principalTable: "ClothingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Outfits_ClothingItems_BottomId",
                        column: x => x.BottomId,
                        principalTable: "ClothingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Outfits_ClothingItems_FootwearId",
                        column: x => x.FootwearId,
                        principalTable: "ClothingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Outfits_ClothingItems_HeadwearId",
                        column: x => x.HeadwearId,
                        principalTable: "ClothingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Outfits_ClothingItems_TopId",
                        column: x => x.TopId,
                        principalTable: "ClothingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Outfits_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("7567dcf4-7f67-4bbf-8196-682d49c6ed8a"), null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfCreation", "Email", "EmailConfirmed", "Gender", "LockoutEnabled", "LockoutEnd", "Nick", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e8b9e310-3acb-41c9-9ea5-a5d58e151dde"), 0, "89b568a6-836a-428a-af52-b2ab78270454", new DateOnly(2024, 4, 15), "admin@email.com", true, "Not specified", false, null, "Admin", "ADMIN@EMAIL.COM", "ADMIN@EMAIL.COM", "AQAAAAIAAYagAAAAEB0JMSWBuQKVVTt06Ng4HvYXBBe+QClSirFs3RMjNMMZBoN3aOURuelb2mbJbl3ijA==", null, false, "something", false, "admin@email.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("7567dcf4-7f67-4bbf-8196-682d49c6ed8a"), new Guid("e8b9e310-3acb-41c9-9ea5-a5d58e151dde") });

            migrationBuilder.CreateIndex(
                name: "IX_Outfits_AccessoryId",
                table: "Outfits",
                column: "AccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Outfits_BottomId",
                table: "Outfits",
                column: "BottomId");

            migrationBuilder.CreateIndex(
                name: "IX_Outfits_FootwearId",
                table: "Outfits",
                column: "FootwearId");

            migrationBuilder.CreateIndex(
                name: "IX_Outfits_HeadwearId",
                table: "Outfits",
                column: "HeadwearId");

            migrationBuilder.CreateIndex(
                name: "IX_Outfits_OwnerId",
                table: "Outfits",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Outfits_TopId",
                table: "Outfits",
                column: "TopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Outfits");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7567dcf4-7f67-4bbf-8196-682d49c6ed8a"), new Guid("e8b9e310-3acb-41c9-9ea5-a5d58e151dde") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7567dcf4-7f67-4bbf-8196-682d49c6ed8a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e8b9e310-3acb-41c9-9ea5-a5d58e151dde"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("9ac796a2-bfc0-4495-8737-a7ad0ad2a01b"), null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfCreation", "Email", "EmailConfirmed", "Gender", "LockoutEnabled", "LockoutEnd", "Nick", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("2530cada-64a9-43ec-9024-d984f8529f1e"), 0, "8a5cdf35-4747-4a48-8ccd-b1e9d3ca9728", new DateOnly(2024, 4, 12), "admin@email.com", true, "Not specified", false, null, "Admin", "ADMIN@EMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEK3QQPkmhnwS1mrT/+bp/XOZS4GhHzJ/vFNGprcFaiuB5YtIfZsd35tNcb5Ln/uCEQ==", null, false, "something", false, "admin@email.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("9ac796a2-bfc0-4495-8737-a7ad0ad2a01b"), new Guid("2530cada-64a9-43ec-9024-d984f8529f1e") });
        }
    }
}
