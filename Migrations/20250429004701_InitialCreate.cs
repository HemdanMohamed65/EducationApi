using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__D54EE9B456306F9F", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    parent_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    otp = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    otp_expiration_time = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Parents__F2A60819CB14ED86", x => x.parent_id);
                });

            migrationBuilder.CreateTable(
                name: "Alphabets",
                columns: table => new
                {
                    letter_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    letter = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    audio_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    photo_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Alphabet__A6F3C31C44939557", x => x.letter_id);
                    table.ForeignKey(
                        name: "FK__Alphabets__categ__412EB0B6",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    animal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    animal_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    audio_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    photo_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Animals__DE680F9270C62114", x => x.animal_id);
                    table.ForeignKey(
                        name: "FK__Animals__categor__48CFD27E",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Numbers",
                columns: table => new
                {
                    number_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    number_value = table.Column<int>(type: "int", nullable: false),
                    audio_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    photo_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Numbers__4BE613D35F479957", x => x.number_id);
                    table.ForeignKey(
                        name: "FK__Numbers__categor__44FF419A",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    story_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    video_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    photo_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stories__66339C5643A8DF6C", x => x.story_id);
                    table.ForeignKey(
                        name: "FK__Stories__categor__4BAC3F29",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    child_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parent_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Children__015ADC0512E1D134", x => x.child_id);
                    table.ForeignKey(
                        name: "FK__Children__parent__3D5E1FD2",
                        column: x => x.parent_id,
                        principalTable: "Parents",
                        principalColumn: "parent_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Child_Content",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    child_id = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    content_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Child_Co__3213E83F4D9B9CC0", x => x.id);
                    table.ForeignKey(
                        name: "FK__Child_Con__categ__4F7CD00D",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Child_Con__child__4E88ABD4",
                        column: x => x.child_id,
                        principalTable: "Children",
                        principalColumn: "child_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alphabets_category_id",
                table: "Alphabets",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Alphabet__853DC438315AEE15",
                table: "Alphabets",
                column: "letter",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_category_id",
                table: "Animals",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Animals__FB9F6D4C77F05DB0",
                table: "Animals",
                column: "animal_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Category__5189E255AC09005F",
                table: "Category",
                column: "category_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Child_Content_category_id",
                table: "Child_Content",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Child_Content_child_id",
                table: "Child_Content",
                column: "child_id");

            migrationBuilder.CreateIndex(
                name: "IX_Children_parent_id",
                table: "Children",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Numbers_category_id",
                table: "Numbers",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Numbers__2C8F03EC91AC93AB",
                table: "Numbers",
                column: "number_value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Parents__AB6E616432BAB267",
                table: "Parents",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stories_category_id",
                table: "Stories",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alphabets");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Child_Content");

            migrationBuilder.DropTable(
                name: "Numbers");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Parents");
        }
    }
}
