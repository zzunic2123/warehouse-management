namespace FluentMigrator.Migrations;

[Migration(10112023_001)]
public class AddUsersAndRolesAndRefreshToken : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Email").AsString().NotNullable().Unique()
            .WithColumn("Password").AsString().NotNullable()
            .WithColumn("PreferredUsername").AsString().NotNullable()
            .WithColumn("GiveName").AsString().NotNullable()
            .WithColumn("FamilyName").AsString().NotNullable()
            .WithColumn("EmailVerified").AsBoolean().NotNullable();

        Create.Table("Role")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
            .WithColumn("Name").AsString().NotNullable().Unique();

        Create.Table("UserRole")
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_user_role_user_id", "Users", "Id")
            .WithColumn("RoleId").AsInt64().NotNullable().ForeignKey("FK_user_role_role_id","Role", "Id");
        
        Create.Table("RefreshToken")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CreatedAtUtc").AsDateTimeOffset().NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_refresh_token_user_id", "Users", "Id")
            .WithColumn("Token").AsString().NotNullable()
            .WithColumn("ExpiresAt").AsDateTimeOffset().NotNullable();
        
    }

    public override void Down()
    {
    }
}

[Migration(10112023_002)]
public class AddTenantIdToUsers : Migration
{
    public override void Up()
    {
        Create.Column("TenantId").OnTable("Users").AsInt64().Nullable();
    }

    public override void Down()
    {
    }
}