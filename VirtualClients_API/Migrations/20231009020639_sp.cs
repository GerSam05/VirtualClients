using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualClients_API.Migrations
{
    public partial class sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                create proc sp_ListarClientes
                as
                begin
                Select * from Cliente
                end");

            migrationBuilder.Sql(@"
                create proc sp_ObtenerCliente
                @id int
                as
                begin
                Select * from Cliente where Id = @id
                end");

            migrationBuilder.Sql(@"
                create proc sp_GuardarCliente
	            @nombre nvarchar(max),
	            @apellido nvarchar(max),
	            @estatus int,
	            @id int Output
                as
                begin
                set Nocount on;

                insert into Cliente(Nombre, Apellido, Estatus)
                values(@nombre, @apellido, @estatus)
                Select @id = SCOPE_IDENTITY()
                end
                ");

            migrationBuilder.Sql(@"
                create proc sp_ActualizarCliente
                @id int,
	            @nombre nvarchar(max),
	            @apellido nvarchar(max),
	            @estatus int
                as
                begin

                update Cliente set Nombre = @nombre,
	            Apellido = @apellido,
	            Estatus = @estatus
                where Id = @id
                end
                ");

            migrationBuilder.Sql(@"
	            create proc sp_EliminarCliente
	            @id int
                as
                begin
                Delete from Cliente where Id = @id
                end");

            migrationBuilder.Sql(@"
	            create proc sp_InformacionTotal
	            @id int
	            as
                begin
	            Select Nombre, Apellido, Co.Estatus as Estatus from Cliente as C 
                inner join Condicion as Co on C.Estatus = Co.Id
	            end");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE sp_ListarClientes");
            migrationBuilder.Sql("DROP PROCEDURE sp_ObtenerCliente");
            migrationBuilder.Sql("DROP PROCEDURE sp_GuardarCliente");
            migrationBuilder.Sql("DROP PROCEDURE sp_ActualizarCliente");
            migrationBuilder.Sql("DROP PROCEDURE sp_EliminarCliente");
            migrationBuilder.Sql("DROP PROCEDURE sp_InformacionTotal");
        }
    }
}