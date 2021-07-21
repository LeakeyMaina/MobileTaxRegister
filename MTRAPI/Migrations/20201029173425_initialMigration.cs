using Microsoft.EntityFrameworkCore.Migrations;

namespace MTR.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxPayers",
                columns: table => new
                {
                    TaxPayerID = table.Column<string>(maxLength: 36, nullable: false),
                    RegistrationDate = table.Column<string>(nullable: true),
                    KRAPin = table.Column<string>(maxLength: 15, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Telephone = table.Column<string>(maxLength: 20, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 50, nullable: false),
                    CurrentMonthToDateSalesAmount = table.Column<double>(nullable: false),
                    CurrentMonthToDateVATAmount = table.Column<double>(nullable: false),
                    CurrentYearToDateSalesAmount = table.Column<double>(nullable: false),
                    CurrentYearToDateVATAmount = table.Column<double>(nullable: false),
                    CurrentDaySalesAmount = table.Column<double>(nullable: false),
                    CurrentDayVATAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxPayers", x => x.TaxPayerID);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<string>(nullable: false),
                    LRNumber = table.Column<string>(nullable: true),
                    Building = table.Column<string>(nullable: true),
                    StreetRoad = table.Column<string>(nullable: true),
                    CityTown = table.Column<string>(nullable: false),
                    County = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    POBox = table.Column<string>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    TaxPayerID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Address_TaxPayers_TaxPayerID",
                        column: x => x.TaxPayerID,
                        principalTable: "TaxPayers",
                        principalColumn: "TaxPayerID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ETRs",
                columns: table => new
                {
                    ETRID = table.Column<string>(maxLength: 30, nullable: false),
                    RegistrationDate = table.Column<string>(nullable: false),
                    ConfirmationCode = table.Column<string>(nullable: false),
                    Telephone = table.Column<string>(nullable: false),
                    TaxPayerID = table.Column<string>(nullable: false),
                    CurrentDaySalesAmount = table.Column<double>(nullable: false),
                    CurrentDayVATAmount = table.Column<double>(nullable: false),
                    CurrentMonthToDateSalesAmount = table.Column<double>(nullable: false),
                    CurrentMonthToDateVATAmount = table.Column<double>(nullable: false),
                    CurrentYearToDateSalesAmount = table.Column<double>(nullable: false),
                    CurrentYearToDateVATAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETRs", x => x.ETRID);
                    table.ForeignKey(
                        name: "FK_ETRs_TaxPayers_TaxPayerID",
                        column: x => x.TaxPayerID,
                        principalTable: "TaxPayers",
                        principalColumn: "TaxPayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleID = table.Column<string>(nullable: false),
                    SaleDate = table.Column<string>(nullable: false),
                    SaleAmount = table.Column<double>(nullable: false),
                    VATAmount = table.Column<double>(nullable: false),
                    ETRID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleID);
                    table.ForeignKey(
                        name: "FK_Sales_ETRs_ETRID",
                        column: x => x.ETRID,
                        principalTable: "ETRs",
                        principalColumn: "ETRID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ETRReceipts",
                columns: table => new
                {
                    ETRReceiptID = table.Column<string>(nullable: false),
                    SaleID = table.Column<string>(nullable: false),
                    ETRID = table.Column<string>(nullable: false),
                    SaleDate = table.Column<string>(nullable: false),
                    SaleAmount = table.Column<double>(nullable: false),
                    VATAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETRReceipts", x => x.ETRReceiptID);
                    table.ForeignKey(
                        name: "FK_ETRReceipts_ETRs_ETRID",
                        column: x => x.ETRID,
                        principalTable: "ETRs",
                        principalColumn: "ETRID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ETRReceipts_Sales_SaleID",
                        column: x => x.SaleID,
                        principalTable: "Sales",
                        principalColumn: "SaleID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_TaxPayerID",
                table: "Address",
                column: "TaxPayerID",
                unique: true,
                filter: "[TaxPayerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ETRReceipts_ETRID",
                table: "ETRReceipts",
                column: "ETRID");

            migrationBuilder.CreateIndex(
                name: "IX_ETRReceipts_SaleID",
                table: "ETRReceipts",
                column: "SaleID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ETRs_TaxPayerID",
                table: "ETRs",
                column: "TaxPayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ETRID",
                table: "Sales",
                column: "ETRID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "ETRReceipts");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "ETRs");

            migrationBuilder.DropTable(
                name: "TaxPayers");
        }
    }
}
