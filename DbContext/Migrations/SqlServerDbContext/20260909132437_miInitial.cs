using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContext.Migrations.SqlServerDbContext
{
    /// <inheritdoc />
    public partial class miInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetAddress = table.Column<string>(type: "varchar(200)", nullable: true),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "varchar(200)", nullable: true),
                    Country = table.Column<string>(type: "varchar(200)", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryType = table.Column<string>(type: "varchar(200)", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Attractions",
                columns: table => new
                {
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttractionName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true),
                    AddressDbMAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attractions", x => x.AttractionId);
                    table.ForeignKey(
                        name: "FK_Attractions_Addresses_AddressDbMAddressId",
                        column: x => x.AddressDbMAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(200)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", nullable: true),
                    AddressDbMAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_AddressDbMAddressId",
                        column: x => x.AddressDbMAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateTable(
                name: "AttractionDbMCategoryDbM",
                columns: table => new
                {
                    AttractionsDbMAttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriesDbMCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionDbMCategoryDbM", x => new { x.AttractionsDbMAttractionId, x.CategoriesDbMCategoryId });
                    table.ForeignKey(
                        name: "FK_AttractionDbMCategoryDbM_Attractions_AttractionsDbMAttractionId",
                        column: x => x.AttractionsDbMAttractionId,
                        principalTable: "Attractions",
                        principalColumn: "AttractionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttractionDbMCategoryDbM_Categories_CategoriesDbMCategoryId",
                        column: x => x.CategoriesDbMCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttractionDbMUserDbM",
                columns: table => new
                {
                    AttractionsDbMAttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersDbMUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionDbMUserDbM", x => new { x.AttractionsDbMAttractionId, x.UsersDbMUserId });
                    table.ForeignKey(
                        name: "FK_AttractionDbMUserDbM_Attractions_AttractionsDbMAttractionId",
                        column: x => x.AttractionsDbMAttractionId,
                        principalTable: "Attractions",
                        principalColumn: "AttractionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttractionDbMUserDbM_Users_UsersDbMUserId",
                        column: x => x.UsersDbMUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "varchar(200)", nullable: false),
                    ReviewGrade = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttractionDbMAttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserDbMUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Attractions_AttractionDbMAttractionId",
                        column: x => x.AttractionDbMAttractionId,
                        principalTable: "Attractions",
                        principalColumn: "AttractionId");
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserDbMUserId",
                        column: x => x.UserDbMUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_City",
                table: "Addresses",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionDbMCategoryDbM_CategoriesDbMCategoryId",
                table: "AttractionDbMCategoryDbM",
                column: "CategoriesDbMCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionDbMUserDbM_UsersDbMUserId",
                table: "AttractionDbMUserDbM",
                column: "UsersDbMUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_AddressDbMAddressId",
                table: "Attractions",
                column: "AddressDbMAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AttractionDbMAttractionId",
                table: "Reviews",
                column: "AttractionDbMAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserDbMUserId",
                table: "Reviews",
                column: "UserDbMUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressDbMAddressId",
                table: "Users",
                column: "AddressDbMAddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttractionDbMCategoryDbM");

            migrationBuilder.DropTable(
                name: "AttractionDbMUserDbM");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Attractions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
