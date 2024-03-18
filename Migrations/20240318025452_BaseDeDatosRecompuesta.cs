using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompaniaRepuestos.Migrations
{
    /// <inheritdoc />
    public partial class BaseDeDatosRecompuesta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroAuditoria_Usuario_UsuarioidUsuario",
                table: "RegistroAuditoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Rol_RolidRol",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Usuario_UsuarioidUsuario",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_UsuarioidUsuario",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_RegistroAuditoria_UsuarioidUsuario",
                table: "RegistroAuditoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_RolidRol",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rol",
                table: "Rol");

            migrationBuilder.DropColumn(
                name: "UsuarioidUsuario",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "UsuarioidUsuario",
                table: "RegistroAuditoria");

            migrationBuilder.DropColumn(
                name: "RolidRol",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Rol",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "idUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_idUsuario",
                table: "Venta",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAuditoria_idUsuario",
                table: "RegistroAuditoria",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_idRol",
                table: "Usuarios",
                column: "idRol");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroAuditoria_Usuarios_idUsuario",
                table: "RegistroAuditoria",
                column: "idUsuario",
                principalTable: "Usuarios",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_idRol",
                table: "Usuarios",
                column: "idRol",
                principalTable: "Roles",
                principalColumn: "idRol",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Usuarios_idUsuario",
                table: "Venta",
                column: "idUsuario",
                principalTable: "Usuarios",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroAuditoria_Usuarios_idUsuario",
                table: "RegistroAuditoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_idRol",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Usuarios_idUsuario",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_idUsuario",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_RegistroAuditoria_idUsuario",
                table: "RegistroAuditoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_idRol",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Rol");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioidUsuario",
                table: "Venta",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioidUsuario",
                table: "RegistroAuditoria",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RolidRol",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "idUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rol",
                table: "Rol",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_UsuarioidUsuario",
                table: "Venta",
                column: "UsuarioidUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAuditoria_UsuarioidUsuario",
                table: "RegistroAuditoria",
                column: "UsuarioidUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RolidRol",
                table: "Usuario",
                column: "RolidRol");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroAuditoria_Usuario_UsuarioidUsuario",
                table: "RegistroAuditoria",
                column: "UsuarioidUsuario",
                principalTable: "Usuario",
                principalColumn: "idUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Rol_RolidRol",
                table: "Usuario",
                column: "RolidRol",
                principalTable: "Rol",
                principalColumn: "idRol");

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Usuario_UsuarioidUsuario",
                table: "Venta",
                column: "UsuarioidUsuario",
                principalTable: "Usuario",
                principalColumn: "idUsuario");
        }
    }
}
