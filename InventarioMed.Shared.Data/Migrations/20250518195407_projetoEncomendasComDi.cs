using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioMed.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class projetoEncomendasComDi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTag_Orders_OrdersId",
                table: "OrderTag");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTag_Tags_TagsId",
                table: "OrderTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTag",
                table: "OrderTag");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "OrderTag",
                newName: "OrderTags");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTag_TagsId",
                table: "OrderTags",
                newName: "IX_OrderTags_TagsId");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTags",
                table: "OrderTags",
                columns: new[] { "OrdersId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTags_Orders_OrdersId",
                table: "OrderTags",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTags_Tags_TagsId",
                table: "OrderTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTags_Orders_OrdersId",
                table: "OrderTags");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTags_Tags_TagsId",
                table: "OrderTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTags",
                table: "OrderTags");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "OrderTags",
                newName: "OrderTag");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTags_TagsId",
                table: "OrderTag",
                newName: "IX_OrderTag_TagsId");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTag",
                table: "OrderTag",
                columns: new[] { "OrdersId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTag_Orders_OrdersId",
                table: "OrderTag",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTag_Tags_TagsId",
                table: "OrderTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
