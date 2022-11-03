using Microsoft.EntityFrameworkCore.Migrations;

namespace Mc2.CrudTest.Presentation.Infrustructure.Migrations
{
    public partial class UpdateCustomerValueTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customers_Email",
                schema: "customer",
                table: "customers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "customer",
                table: "customers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountNumber",
                schema: "customer",
                table: "customers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_customers_Email",
                schema: "customer",
                table: "customers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customers_Email",
                schema: "customer",
                table: "customers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "customer",
                table: "customers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountNumber",
                schema: "customer",
                table: "customers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_customers_Email",
                schema: "customer",
                table: "customers",
                column: "Email",
                unique: true);
        }
    }
}
