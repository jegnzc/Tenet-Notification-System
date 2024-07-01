using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TenetTest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Base : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerEmail = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Recipient = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    NotificationChannelId = table.Column<int>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationChannels_NotificationChannelId",
                        column: x => x.NotificationChannelId,
                        principalTable: "NotificationChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NotificationChannelId = table.Column<int>(type: "INTEGER", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Body = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTemplates_NotificationChannels_NotificationChannelId",
                        column: x => x.NotificationChannelId,
                        principalTable: "NotificationChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    NotificationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationContents_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NotificationChannels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Email" },
                    { 1, "SMS" },
                    { 2, "Push" }
                });

            migrationBuilder.InsertData(
                table: "NotificationTemplates",
                columns: new[] { "Id", "Body", "Name", "NotificationChannelId", "Subject" },
                values: new object[,]
                {
                    { new Guid("183a09c2-ba42-469d-891b-e4ae83fe4fa3"), "Hello, @Model[\"FirstName\"], check out our special promotion!", "PromotionSMS", 1, null },
                    { new Guid("18d059b8-ea89-447e-8737-770bfc39c460"), "Hello, @Model[\"FirstName\"], you can reset your password using this link.", "PasswordResetSMS", 1, null },
                    { new Guid("2e681b67-2920-4e33-8ed0-da492b70aa21"), "Hello, @Model[\"FirstName\"], please verify your account using this link.", "AccountVerificationPush", 2, "Verify Your Account" },
                    { new Guid("46d5edfa-ab20-4be5-87c9-d2299f8ff034"), "\r\n    <!DOCTYPE html>\r\n    <html>\r\n    <head>\r\n        <meta charset='utf-8'>\r\n        <style>\r\n            body { font-family: Arial, sans-serif; }\r\n            .container { padding: 20px; }\r\n            .header, .footer { text-align: center; }\r\n            .header { background-color: #f8f8f8; padding: 10px; }\r\n            .footer { margin-top: 20px; color: #aaa; }\r\n        </style>\r\n    </head>\r\n    <body>\r\n        <div class='container'>\r\n            <div class='header'>\r\n                <img src='https://via.placeholder.com/150' alt='Company Logo' />\r\n            </div>\r\n            <div class='content'>\r\n                \r\n                <h1>Account Verification</h1>\r\n                <p>Hello, @Model[\"FirstName\"],</p>\r\n                <p>Please verify your account using this <a href=\"#\" target=\"_blank\">link</a>.</p>\r\n                <p>Thank you for joining us!</p>\r\n            </div>\r\n            <div class='footer'>\r\n                <p>Company Name | <a href='#' target='_blank'>Unsubscribe</a></p>\r\n            </div>\r\n        </div>\r\n    </body>\r\n    </html>", "AccountVerificationEmail", 0, "Verify Your Account" },
                    { new Guid("558df13a-dcbf-4f07-b127-36dbe5307e22"), "Hello, @Model[\"FirstName\"], welcome to our service!", "WelcomeSMS", 1, null },
                    { new Guid("9b31debf-45c4-4d98-8811-898d9c1b938f"), "Hello, @Model[\"FirstName\"], check out our special promotion!", "PromotionPush", 2, "Special Promotion" },
                    { new Guid("a93c032a-22f1-419d-a659-8af289e30c0d"), "\r\n    <!DOCTYPE html>\r\n    <html>\r\n    <head>\r\n        <meta charset='utf-8'>\r\n        <style>\r\n            body { font-family: Arial, sans-serif; }\r\n            .container { padding: 20px; }\r\n            .header, .footer { text-align: center; }\r\n            .header { background-color: #f8f8f8; padding: 10px; }\r\n            .footer { margin-top: 20px; color: #aaa; }\r\n        </style>\r\n    </head>\r\n    <body>\r\n        <div class='container'>\r\n            <div class='header'>\r\n                <img src='https://via.placeholder.com/150' alt='Company Logo' />\r\n            </div>\r\n            <div class='content'>\r\n                \r\n                <h1>Special Promotion Just for You!</h1>\r\n                <p>Hello, @Model[\"FirstName\"],</p>\r\n                <p>We are offering a special promotion exclusively for you. Don't miss out!</p>\r\n                <table border='1' cellspacing='0' cellpadding='10'>\r\n                    <tr>\r\n                        <th>Product</th>\r\n                        <th>Discount</th>\r\n                        <th>Link</th>\r\n                    </tr>\r\n                    <tr>\r\n                        <td>Product A</td>\r\n                        <td>20% off</td>\r\n                        <td><a href='#' target='_blank'>Buy Now</a></td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td>Product B</td>\r\n                        <td>30% off</td>\r\n                        <td><a href='#' target='_blank'>Buy Now</a></td>\r\n                    </tr>\r\n                </table>\r\n            </div>\r\n            <div class='footer'>\r\n                <p>Company Name | <a href='#' target='_blank'>Unsubscribe</a></p>\r\n            </div>\r\n        </div>\r\n    </body>\r\n    </html>", "PromotionEmail", 0, "Special Promotion" },
                    { new Guid("b8619fd4-0acf-4937-8f1a-573519f22ee0"), "Hello, @Model[\"FirstName\"], welcome to our service!", "WelcomePush", 2, "Welcome to Our Service" },
                    { new Guid("bd51fa5f-5476-4d14-84ec-498d6a8f530d"), "\r\n    <!DOCTYPE html>\r\n    <html>\r\n    <head>\r\n        <meta charset='utf-8'>\r\n        <style>\r\n            body { font-family: Arial, sans-serif; }\r\n            .container { padding: 20px; }\r\n            .header, .footer { text-align: center; }\r\n            .header { background-color: #f8f8f8; padding: 10px; }\r\n            .footer { margin-top: 20px; color: #aaa; }\r\n        </style>\r\n    </head>\r\n    <body>\r\n        <div class='container'>\r\n            <div class='header'>\r\n                <img src='https://via.placeholder.com/150' alt='Company Logo' />\r\n            </div>\r\n            <div class='content'>\r\n                \r\n                <h1>Password Reset</h1>\r\n                <p>Hello, @Model[\"FirstName\"],</p>\r\n                <p>You can reset your password using this <a href=\"#\" target=\"_blank\">link</a>.</p>\r\n                <p>If you did not request a password reset, please ignore this email.</p>\r\n            </div>\r\n            <div class='footer'>\r\n                <p>Company Name | <a href='#' target='_blank'>Unsubscribe</a></p>\r\n            </div>\r\n        </div>\r\n    </body>\r\n    </html>", "PasswordResetEmail", 0, "Reset Your Password" },
                    { new Guid("cbc3395c-e3bb-4d55-908c-4c7ce53c0478"), "\r\n    <!DOCTYPE html>\r\n    <html>\r\n    <head>\r\n        <meta charset='utf-8'>\r\n        <style>\r\n            body { font-family: Arial, sans-serif; }\r\n            .container { padding: 20px; }\r\n            .header, .footer { text-align: center; }\r\n            .header { background-color: #f8f8f8; padding: 10px; }\r\n            .footer { margin-top: 20px; color: #aaa; }\r\n        </style>\r\n    </head>\r\n    <body>\r\n        <div class='container'>\r\n            <div class='header'>\r\n                <img src='https://via.placeholder.com/150' alt='Company Logo' />\r\n            </div>\r\n            <div class='content'>\r\n                \r\n                <h1>Welcome, @Model[\"FirstName\"]!</h1>\r\n                <p>We are excited to have you on board. Explore our services and enjoy the benefits we offer.</p>\r\n            </div>\r\n            <div class='footer'>\r\n                <p>Company Name | <a href='#' target='_blank'>Unsubscribe</a></p>\r\n            </div>\r\n        </div>\r\n    </body>\r\n    </html>", "WelcomeEmail", 0, "Welcome to Our Service" },
                    { new Guid("e31d8c06-4ab0-4934-bb5a-8b460d00de24"), "\r\n    <!DOCTYPE html>\r\n    <html>\r\n    <head>\r\n        <meta charset='utf-8'>\r\n        <style>\r\n            body { font-family: Arial, sans-serif; }\r\n            .container { padding: 20px; }\r\n            .header, .footer { text-align: center; }\r\n            .header { background-color: #f8f8f8; padding: 10px; }\r\n            .footer { margin-top: 20px; color: #aaa; }\r\n        </style>\r\n    </head>\r\n    <body>\r\n        <div class='container'>\r\n            <div class='header'>\r\n                <img src='https://via.placeholder.com/150' alt='Company Logo' />\r\n            </div>\r\n            <div class='content'>\r\n                \r\n                <h1>Order Confirmation</h1>\r\n                <p>Hello, @Model[\"FirstName\"],</p>\r\n                <p>Your order with order number @Model[\"OrderNumber\"] has been placed successfully.</p>\r\n                <p>We will notify you once the order is shipped.</p>\r\n                <p>Thank you for shopping with us!</p>\r\n            </div>\r\n            <div class='footer'>\r\n                <p>Company Name | <a href='#' target='_blank'>Unsubscribe</a></p>\r\n            </div>\r\n        </div>\r\n    </body>\r\n    </html>", "OrderPlacedEmail", 0, "Order Confirmation" },
                    { new Guid("fcc69863-e92f-40d8-9e3d-18bc5af18845"), "Hello, @Model[\"FirstName\"], please verify your account using this link.", "AccountVerificationSMS", 1, null },
                    { new Guid("fe883949-da99-4892-84ff-0686da1adbf0"), "Hello, @Model[\"FirstName\"], you can reset your password using this link.", "PasswordResetPush", 2, "Reset Your Password" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationContents_NotificationId",
                table: "NotificationContents",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedAt",
                table: "Notifications",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationChannelId",
                table: "Notifications",
                column: "NotificationChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Recipient",
                table: "Notifications",
                column: "Recipient");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Status",
                table: "Notifications",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_NotificationChannelId",
                table: "NotificationTemplates",
                column: "NotificationChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerEmail",
                table: "Orders",
                column: "CustomerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationContents");

            migrationBuilder.DropTable(
                name: "NotificationTemplates");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationChannels");
        }
    }
}
