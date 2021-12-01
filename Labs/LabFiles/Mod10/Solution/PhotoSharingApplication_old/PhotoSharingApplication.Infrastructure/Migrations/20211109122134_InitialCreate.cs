using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoSharingApplication.Infrastructure.Migrations;

public partial class InitialCreate : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "Photos",
            columns: table => new {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                PhotoFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                ContentType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_Photos", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "Photos");
    }
}
