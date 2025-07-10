using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLogicLayer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTrail",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditObjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    TableName = table.Column<string>(type: "text", nullable: true),
                    ActionType = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.ObjectId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    ObjectName = table.Column<string>(type: "text", nullable: true),
                    ObjectNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_User_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "ObjectId");
                });

            migrationBuilder.CreateTable(
                name: "AuditTrailField",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditTrailId = table.Column<Guid>(type: "uuid", nullable: true),
                    FieldName = table.Column<string>(type: "text", nullable: true),
                    TableName = table.Column<string>(type: "text", nullable: true),
                    OldValue = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    OldReadableValue = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    NewValue = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    NewReadableValue = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrailField", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_AuditTrailField_AuditTrail_AuditTrailId",
                        column: x => x.AuditTrailId,
                        principalTable: "AuditTrail",
                        principalColumn: "ObjectId");
                });

            migrationBuilder.CreateTable(
                name: "ApprovalProcess",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectName = table.Column<string>(type: "text", nullable: true),
                    ObjectNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalProcess", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_ApprovalProcess_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "ObjectId");
                });

            migrationBuilder.CreateTable(
                name: "Func",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    FuncCode = table.Column<string>(type: "text", nullable: true),
                    FuncName = table.Column<string>(type: "text", nullable: true),
                    MainCategoryName = table.Column<string>(type: "text", nullable: true),
                    SubCategoryName = table.Column<string>(type: "text", nullable: true),
                    MainUrl = table.Column<string>(type: "text", nullable: true),
                    SubUrl = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AlwaysHidden = table.Column<bool>(type: "boolean", nullable: false),
                    AlwaysAllowAccess = table.Column<bool>(type: "boolean", nullable: false),
                    RequiredAccess2FA = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Func", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_Func_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "ObjectId");
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleCode = table.Column<string>(type: "text", nullable: true),
                    RoleName = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_Role_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "ObjectId");
                });

            migrationBuilder.CreateTable(
                name: "Workflow",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ObjectName = table.Column<string>(type: "text", nullable: true),
                    ObjectNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflow", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_Workflow_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "ObjectId");
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleID = table.Column<Guid>(type: "uuid", nullable: true),
                    ObjectName = table.Column<string>(type: "text", nullable: true),
                    ObjectNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_Position_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ObjectId");
                    table.ForeignKey(
                        name: "FK_Position_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "ObjectId");
                });

            migrationBuilder.CreateTable(
                name: "RoleFunc",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: true),
                    FuncId = table.Column<Guid>(type: "uuid", nullable: true),
                    AllowCreate = table.Column<bool>(type: "boolean", nullable: false),
                    AllowDelete = table.Column<bool>(type: "boolean", nullable: false),
                    AllowEdit = table.Column<bool>(type: "boolean", nullable: false),
                    AllowView = table.Column<bool>(type: "boolean", nullable: false),
                    ObjectName = table.Column<string>(type: "text", nullable: true),
                    ObjectNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleFunc", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_RoleFunc_Func_FuncId",
                        column: x => x.FuncId,
                        principalTable: "Func",
                        principalColumn: "ObjectId");
                    table.ForeignKey(
                        name: "FK_RoleFunc_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "ObjectId");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowEdge",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReactFlowId = table.Column<string>(type: "text", nullable: true),
                    SourceNodeId = table.Column<string>(type: "text", nullable: true),
                    TargetNodeId = table.Column<string>(type: "text", nullable: true),
                    Label = table.Column<string>(type: "text", nullable: true),
                    DataJson = table.Column<string>(type: "text", nullable: true),
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowEdge", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_WorkflowEdge_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflow",
                        principalColumn: "ObjectId");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowNode",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReactFlowId = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Label = table.Column<string>(type: "text", nullable: true),
                    DataJson = table.Column<string>(type: "text", nullable: true),
                    PositionX = table.Column<double>(type: "double precision", nullable: true),
                    PositionY = table.Column<double>(type: "double precision", nullable: true),
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowNode", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_WorkflowNode_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflow",
                        principalColumn: "ObjectId");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowInstance",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: true),
                    CurrentNodeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CurrentWorkflowNodeObjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    RelatedObjectTypeName = table.Column<string>(type: "text", nullable: true),
                    RelatedObjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    ObjectName = table.Column<string>(type: "text", nullable: true),
                    ObjectNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowInstance", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_WorkflowInstance_WorkflowNode_CurrentWorkflowNodeObjectId",
                        column: x => x.CurrentWorkflowNodeObjectId,
                        principalTable: "WorkflowNode",
                        principalColumn: "ObjectId");
                    table.ForeignKey(
                        name: "FK_WorkflowInstance_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflow",
                        principalColumn: "ObjectId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalProcess_CreatedUserId",
                table: "ApprovalProcess",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrailField_AuditTrailId",
                table: "AuditTrailField",
                column: "AuditTrailId");

            migrationBuilder.CreateIndex(
                name: "IX_Func_CreatedUserId",
                table: "Func",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_CreatedUserId",
                table: "Position",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_RoleID",
                table: "Position",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_CreatedUserId",
                table: "Role",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleFunc_FuncId",
                table: "RoleFunc",
                column: "FuncId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleFunc_RoleId",
                table: "RoleFunc",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedUserId",
                table: "User",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_CreatedUserId",
                table: "Workflow",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowEdge_WorkflowId",
                table: "WorkflowEdge",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_CurrentWorkflowNodeObjectId",
                table: "WorkflowInstance",
                column: "CurrentWorkflowNodeObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_WorkflowId",
                table: "WorkflowInstance",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowNode_WorkflowId",
                table: "WorkflowNode",
                column: "WorkflowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalProcess");

            migrationBuilder.DropTable(
                name: "AuditTrailField");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "RoleFunc");

            migrationBuilder.DropTable(
                name: "WorkflowEdge");

            migrationBuilder.DropTable(
                name: "WorkflowInstance");

            migrationBuilder.DropTable(
                name: "AuditTrail");

            migrationBuilder.DropTable(
                name: "Func");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "WorkflowNode");

            migrationBuilder.DropTable(
                name: "Workflow");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
