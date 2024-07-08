using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiEchoTune.Migrations
{
    /// <inheritdoc />
    public partial class bd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Musica",
                columns: table => new
                {
                    IdMusica = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    Duracao = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    NomeArtista = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    NomeAlbum = table.Column<string>(type: "VARCHAR(60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musica", x => x.IdMusica);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioMidia",
                columns: table => new
                {
                    IdUsuarioMidia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlobName = table.Column<string>(type: "VARCHAR(60)", nullable: true),
                    Foto = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioMidia", x => x.IdUsuarioMidia);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    IdUsuarioMidia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioMidiaIdUsuarioMidia = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_UsuarioMidia_UsuarioMidiaIdUsuarioMidia",
                        column: x => x.UsuarioMidiaIdUsuarioMidia,
                        principalTable: "UsuarioMidia",
                        principalColumn: "IdUsuarioMidia");
                });

            migrationBuilder.CreateTable(
                name: "MusicaUsuario",
                columns: table => new
                {
                    IdMusicaUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMusica = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Favoritada = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicaUsuario", x => x.IdMusicaUsuario);
                    table.ForeignKey(
                        name: "FK_MusicaUsuario_Musica_IdMusica",
                        column: x => x.IdMusica,
                        principalTable: "Musica",
                        principalColumn: "IdMusica",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicaUsuario_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayList",
                columns: table => new
                {
                    IdPlaylist = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    Tema = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayList", x => x.IdPlaylist);
                    table.ForeignKey(
                        name: "FK_PlayList_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayListMusica",
                columns: table => new
                {
                    IdPlayListMusica = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMusica = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPlayList = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayListMusica", x => x.IdPlayListMusica);
                    table.ForeignKey(
                        name: "FK_PlayListMusica_Musica_IdMusica",
                        column: x => x.IdMusica,
                        principalTable: "Musica",
                        principalColumn: "IdMusica",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayListMusica_PlayList_IdPlayList",
                        column: x => x.IdPlayList,
                        principalTable: "PlayList",
                        principalColumn: "IdPlaylist",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicaUsuario_IdMusica",
                table: "MusicaUsuario",
                column: "IdMusica");

            migrationBuilder.CreateIndex(
                name: "IX_MusicaUsuario_IdUsuario",
                table: "MusicaUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PlayList_IdUsuario",
                table: "PlayList",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PlayListMusica_IdMusica",
                table: "PlayListMusica",
                column: "IdMusica");

            migrationBuilder.CreateIndex(
                name: "IX_PlayListMusica_IdPlayList",
                table: "PlayListMusica",
                column: "IdPlayList");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UsuarioMidiaIdUsuarioMidia",
                table: "Usuario",
                column: "UsuarioMidiaIdUsuarioMidia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicaUsuario");

            migrationBuilder.DropTable(
                name: "PlayListMusica");

            migrationBuilder.DropTable(
                name: "Musica");

            migrationBuilder.DropTable(
                name: "PlayList");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "UsuarioMidia");
        }
    }
}
