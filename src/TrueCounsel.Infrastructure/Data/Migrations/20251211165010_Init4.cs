using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrueCounsel.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_CaseCategories_CaseCategoryId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_CaseTypes_CaseTypeId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Clients_ClientId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Courts_CourtId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Lawyers_LawyerId",
                table: "Cases");

            migrationBuilder.AddColumn<int>(
                name: "CaseTypeId1",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourtId1",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CaseTypeId1",
                table: "Cases",
                column: "CaseTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CourtId1",
                table: "Cases",
                column: "CourtId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_CaseCategories_CaseCategoryId",
                table: "Cases",
                column: "CaseCategoryId",
                principalTable: "CaseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_CaseTypes_CaseTypeId",
                table: "Cases",
                column: "CaseTypeId",
                principalTable: "CaseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_CaseTypes_CaseTypeId1",
                table: "Cases",
                column: "CaseTypeId1",
                principalTable: "CaseTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Clients_ClientId",
                table: "Cases",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Courts_CourtId",
                table: "Cases",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Courts_CourtId1",
                table: "Cases",
                column: "CourtId1",
                principalTable: "Courts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Lawyers_LawyerId",
                table: "Cases",
                column: "LawyerId",
                principalTable: "Lawyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_CaseCategories_CaseCategoryId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_CaseTypes_CaseTypeId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_CaseTypes_CaseTypeId1",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Clients_ClientId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Courts_CourtId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Courts_CourtId1",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Lawyers_LawyerId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_CaseTypeId1",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_CourtId1",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "CaseTypeId1",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "CourtId1",
                table: "Cases");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_CaseCategories_CaseCategoryId",
                table: "Cases",
                column: "CaseCategoryId",
                principalTable: "CaseCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_CaseTypes_CaseTypeId",
                table: "Cases",
                column: "CaseTypeId",
                principalTable: "CaseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Clients_ClientId",
                table: "Cases",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Courts_CourtId",
                table: "Cases",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Lawyers_LawyerId",
                table: "Cases",
                column: "LawyerId",
                principalTable: "Lawyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}