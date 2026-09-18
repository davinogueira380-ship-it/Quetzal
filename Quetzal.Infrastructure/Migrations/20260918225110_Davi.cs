using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using Quetzal.Domain.Entidades;

#nullable disable

namespace Quetzal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Davi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Identidade_Perfis",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identidade_Perfis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identidade_Usuarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identidade_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identidade_PerfilClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identidade_PerfilClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identidade_PerfilClaims_Identidade_Perfis_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identidade_Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identidade_UsuarioClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identidade_UsuarioClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identidade_UsuarioClaims_Identidade_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Identidade_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identidade_UsuarioLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identidade_UsuarioLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Identidade_UsuarioLogins_Identidade_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Identidade_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identidade_UsuarioPerfis",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identidade_UsuarioPerfis", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Identidade_UsuarioPerfis_Identidade_Perfis_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identidade_Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Identidade_UsuarioPerfis_Identidade_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Identidade_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identidade_UsuarioTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identidade_UsuarioTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_Identidade_UsuarioTokens_Identidade_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Identidade_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjetosC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProjeto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagemUpload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetosC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjetosC_Identidade_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Identidade_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ambientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PortfolioId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjetoCId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ambientes_ProjetosC_ProjetoCId",
                        column: x => x.ProjetoCId,
                        principalTable: "ProjetosC",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProjeto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AmbienteId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagemUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmbienteId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projetos_Ambientes_AmbienteId",
                        column: x => x.AmbienteId,
                        principalTable: "Ambientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projetos_Ambientes_AmbienteId1",
                        column: x => x.AmbienteId1,
                        principalTable: "Ambientes",
                        principalColumn: "Id");
                });

            // =====================
            // SEED: contexto "Design de Interiores" + Identity users & relations
            // =====================

            // Perfis (roles)
            migrationBuilder.InsertData(
                table: "Identidade_Perfis",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", "Administrador", "ADMINISTRADOR", "c1a2b3d4-e5f6-4711-aaaa-000000000001" },
                    { "00000000-0000-0000-0000-000000000002", "Cliente", "CLIENTE", "c1a2b3d4-e5f6-4711-aaaa-000000000002" }
                });

            // Create password hashes using PasswordHasher<ApplicationUser>
            var hasher = new PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser
            {
                Id = "11111111-1111-1111-1111-111111111111",
                UserName = "admin@quetzal.local",
                NormalizedUserName = "ADMIN@QUETZAL.LOCAL",
                Email = "admin@quetzal.local",
                NormalizedEmail = "ADMIN@QUETZAL.LOCAL",
                NomeCompleto = "Administrador Sistema",
                Telefone = "11999990000",
                Ativo = true,
                DataCadastro = new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc)
            };
            var clienteAUser = new ApplicationUser
            {
                Id = "22222222-2222-2222-2222-222222222222",
                UserName = "cliente.alpha@quetzal.local",
                NormalizedUserName = "CLIENTE.ALPHA@QUETZAL.LOCAL",
                Email = "cliente.alpha@quetzal.local",
                NormalizedEmail = "CLIENTE.ALPHA@QUETZAL.LOCAL",
                NomeCompleto = "Cliente Alpha",
                Telefone = "11988880000",
                Ativo = true,
                DataCadastro = new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc)
            };
            var clienteBUser = new ApplicationUser
            {
                Id = "33333333-3333-3333-3333-333333333333",
                UserName = "cliente.beta@quetzal.local",
                NormalizedUserName = "CLIENTE.BETA@QUETZAL.LOCAL",
                Email = "cliente.beta@quetzal.local",
                NormalizedEmail = "CLIENTE.BETA@QUETZAL.LOCAL",
                NomeCompleto = "Cliente Beta",
                Telefone = "11977770000",
                Ativo = true,
                DataCadastro = new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc)
            };

            var defaultPassword = "Admin@123";

            var adminHash = hasher.HashPassword(adminUser, defaultPassword);
            var clienteAHash = hasher.HashPassword(clienteAUser, defaultPassword);
            var clienteBHash = hasher.HashPassword(clienteBUser, defaultPassword);

            // Usuários (3 usuários: 1 admin, 2 clientes)
            migrationBuilder.InsertData(
                table: "Identidade_Usuarios",
                columns: new[]
                {
                    "Id", "NomeCompleto", "Telefone", "SenhaHash", "Ativo", "DataCadastro", "DataAtualizacao", "DataExclusao",
                    "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed",
                    "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed",
                    "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount"
                },
                values: new object[,]
                {
                    {
                        adminUser.Id,
                        adminUser.NomeCompleto,
                        adminUser.Telefone,
                        "", // SenhaHash (campo custom) mantido vazio para evitar truncamento
                        adminUser.Ativo,
                        adminUser.DataCadastro,
                        null,
                        null,
                        adminUser.UserName,
                        adminUser.NormalizedUserName,
                        adminUser.Email,
                        adminUser.NormalizedEmail,
                        true,
                        adminHash,
                        "stamp-admin-0001",
                        "cc-admin-0001",
                        adminUser.Telefone,
                        true,
                        false,
                        null,
                        false,
                        0
                    },
                    {
                        clienteAUser.Id,
                        clienteAUser.NomeCompleto,
                        clienteAUser.Telefone,
                        "",
                        clienteAUser.Ativo,
                        clienteAUser.DataCadastro,
                        null,
                        null,
                        clienteAUser.UserName,
                        clienteAUser.NormalizedUserName,
                        clienteAUser.Email,
                        clienteAUser.NormalizedEmail,
                        true,
                        clienteAHash,
                        "stamp-cliente-0001",
                        "cc-cliente-0001",
                        clienteAUser.Telefone,
                        true,
                        false,
                        null,
                        false,
                        0
                    },
                    {
                        clienteBUser.Id,
                        clienteBUser.NomeCompleto,
                        clienteBUser.Telefone,
                        "",
                        clienteBUser.Ativo,
                        clienteBUser.DataCadastro,
                        null,
                        null,
                        clienteBUser.UserName,
                        clienteBUser.NormalizedUserName,
                        clienteBUser.Email,
                        clienteBUser.NormalizedEmail,
                        true,
                        clienteBHash,
                        "stamp-cliente-0002",
                        "cc-cliente-0002",
                        clienteBUser.Telefone,
                        true,
                        false,
                        null,
                        false,
                        0
                    }
                });

            // Ambientes típicos de design de interiores (5)
            migrationBuilder.InsertData(
                table: "Ambientes",
                columns: new[] { "Id", "Nome", "PortfolioId", "Ativo", "DataCadastro", "DataAtualizacao", "DataExclusao", "ProjetoCId" },
                values: new object[,]
                {
                    { 1, "Sala de Estar", "interiores-sala-estar", true, new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { 2, "Cozinha", "interiores-cozinha", true, new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { 3, "Quarto Principal", "interiores-quarto-principal", true, new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { 4, "Banheiro", "interiores-banheiro", true, new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { 5, "Home Office", "interiores-home-office", true, new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc), null, null, null }
                });

            // Projetos de design vinculados aos ambientes acima (5)
            migrationBuilder.InsertData(
                table: "Projetos",
                columns: new[] { "Id", "NomeProjeto", "AmbienteId", "Descricao", "ImagemUpload", "Ativo", "DataCriacao", "DataAtualizacao", "DataExclusao", "AmbienteId1" },
                values: new object[,]
                {
                    { 1, "Sala Minimalista", 1, "Projeto de sala com estilo minimalista: paleta neutra, móveis enxutos e iluminação indireta.", null, true, new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { 2, "Cozinha Funcional", 2, "Cozinha com ilha central, soluções de armazenamento e bancada em Quartzo.", null, true, new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { 3, "Suite Luxo", 3, "Quarto principal com closet integrado, cabeceira estofada e iluminação cênica.", null, true, new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { 4, "Banheiro SPA", 4, "Banheiro com revestimentos porcelânicos, nichos e cuba dupla.", null, true, new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { 5, "Escritório Ergonômico", 5, "Home office com mobiliário ergonômico, ponto para videochamadas e iluminação direcionada.", null, true, new DateTime(2026, 9, 18, 0, 0, 0, DateTimeKind.Utc), null, null, null }
                });

            // ProjetosC (projetos do cliente) - relacionam-se a usuários (UsuarioId)
            migrationBuilder.InsertData(
                table: "ProjetosC",
                columns: new[] { "Id", "NomeProjeto", "UsuarioId", "Descricao", "ImagemUpload", "Ativo", "DataAtualizacao", "DataExclusao" },
                values: new object[,]
                {
                    { 1, "Reforma Sala Cliente Alpha", clienteAUser.Id, "Reforma completa com mobiliário planejado.", null, true, null, null },
                    { 2, "Renovação Cozinha Cliente Beta", clienteBUser.Id, "Atualização de bancada e eletrodomésticos.", null, true, null, null },
                    { 3, "Projeto Iluminação Admin", adminUser.Id, "Projeto de iluminação para demonstração.", null, true, null, null },
                    { 4, "Closet Cliente Alpha", clienteAUser.Id, "Closet planejado com portas de correr.", null, true, null, null },
                    { 5, "Home Office Cliente Beta", clienteBUser.Id, "Home office com isolamento acústico.", null, true, null, null }
                });

            // Identity relations: user roles (Identidade_UsuarioPerfis)
            migrationBuilder.InsertData(
                table: "Identidade_UsuarioPerfis",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { adminUser.Id, "00000000-0000-0000-0000-000000000001" }, // admin -> Administrador
                    { clienteAUser.Id, "00000000-0000-0000-0000-000000000002" }, // clienteA -> Cliente
                    { clienteBUser.Id, "00000000-0000-0000-0000-000000000002" }  // clienteB -> Cliente
                });

            // Perfil claims: dá permissão extra ao Administrador
            migrationBuilder.InsertData(
                table: "Identidade_PerfilClaims",
                columns: new[] { "Id", "RoleId", "ClaimType", "ClaimValue" },
                values: new object[] { 1, "00000000-0000-0000-0000-000000000001", "Permissao", "AcessoTotal" });

            // Usuario claims: marca admin com claim de gerenciamento
            migrationBuilder.InsertData(
                table: "Identidade_UsuarioClaims",
                columns: new[] { "Id", "UserId", "ClaimType", "ClaimValue" },
                values: new object[] { 1, adminUser.Id, "GerenciarProjetos", "true" });

            // Usuario logins: exemplo de login externo/local
            migrationBuilder.InsertData(
                table: "Identidade_UsuarioLogins",
                columns: new[] { "LoginProvider", "ProviderKey", "ProviderDisplayName", "UserId" },
                values: new object[] { "Local", "admin_local", "Local Login Admin", adminUser.Id });

            // Usuario tokens: exemplo
            migrationBuilder.InsertData(
                table: "Identidade_UsuarioTokens",
                columns: new[] { "UserId", "LoginProvider", "Name", "Value" },
                values: new object[] { adminUser.Id, "App", "RefreshToken", "token-admin-0001" });

            migrationBuilder.CreateIndex(
                name: "IX_Ambientes_ProjetoCId",
                table: "Ambientes",
                column: "ProjetoCId");

            migrationBuilder.CreateIndex(
                name: "IX_Identidade_PerfilClaims_RoleId",
                table: "Identidade_PerfilClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Identidade_Perfis",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Identidade_UsuarioClaims_UserId",
                table: "Identidade_UsuarioClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Identidade_UsuarioLogins_UserId",
                table: "Identidade_UsuarioLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Identidade_UsuarioPerfis_RoleId",
                table: "Identidade_UsuarioPerfis",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Identidade_Usuarios",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Identidade_Usuarios",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_AmbienteId",
                table: "Projetos",
                column: "AmbienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_AmbienteId1",
                table: "Projetos",
                column: "AmbienteId1",
                unique: true,
                filter: "[AmbienteId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetosC_UsuarioId",
                table: "ProjetosC",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove seeded data (reverse order to respect FKs)

            // Identity extras
            migrationBuilder.DeleteData(
                table: "Identidade_UsuarioTokens",
                keyColumns: new[] { "UserId", "LoginProvider", "Name" },
                keyValues: new object[] { "11111111-1111-1111-1111-111111111111", "App", "RefreshToken" });

            migrationBuilder.DeleteData(
                table: "Identidade_UsuarioLogins",
                keyColumns: new[] { "LoginProvider", "ProviderKey" },
                keyValues: new object[] { "Local", "admin_local" });

            migrationBuilder.DeleteData(
                table: "Identidade_UsuarioClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Identidade_PerfilClaims",
                keyColumn: "Id",
                keyValue: 1);

            // User roles
            migrationBuilder.DeleteData(
                table: "Identidade_UsuarioPerfis",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "11111111-1111-1111-1111-111111111111", "00000000-0000-0000-0000-000000000001" });

            migrationBuilder.DeleteData(
                table: "Identidade_UsuarioPerfis",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "22222222-2222-2222-2222-222222222222", "00000000-0000-0000-0000-000000000002" });

            migrationBuilder.DeleteData(
                table: "Identidade_UsuarioPerfis",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "33333333-3333-3333-3333-333333333333", "00000000-0000-0000-0000-000000000002" });

            // ProjetosC
            migrationBuilder.DeleteData(
                table: "ProjetosC",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });

            // Projetos
            migrationBuilder.DeleteData(
                table: "Projetos",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });

            // Ambientes
            migrationBuilder.DeleteData(
                table: "Ambientes",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });

            // Usuários
            migrationBuilder.DeleteData(
                table: "Identidade_Usuarios",
                keyColumn: "Id",
                keyValues: new object[] { "11111111-1111-1111-1111-111111111111", "22222222-2222-2222-2222-222222222222", "33333333-3333-3333-3333-333333333333" });

            // Perfis (roles)
            migrationBuilder.DeleteData(
                table: "Identidade_Perfis",
                keyColumn: "Id",
                keyValues: new object[] { "00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000002" });

            migrationBuilder.DropTable(
                name: "Identidade_PerfilClaims");

            migrationBuilder.DropTable(
                name: "Identidade_UsuarioClaims");

            migrationBuilder.DropTable(
                name: "Identidade_UsuarioLogins");

            migrationBuilder.DropTable(
                name: "Identidade_UsuarioPerfis");

            migrationBuilder.DropTable(
                name: "Identidade_UsuarioTokens");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropTable(
                name: "Identidade_Perfis");

            migrationBuilder.DropTable(
                name: "Ambientes");

            migrationBuilder.DropTable(
                name: "ProjetosC");

            migrationBuilder.DropTable(
                name: "Identidade_Usuarios");
        }
    }
}