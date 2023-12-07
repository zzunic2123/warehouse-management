using Microsoft.AspNetCore.Http.HttpResults;

namespace FluentMigrator.Migrations;


[Migration(15102023100005)]
public class Migration_15102023100005 : Migration
{
    
    public override void Up()
    {
        Create.Table("Address")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Street").AsString()
            .WithColumn("City").AsString()
            .WithColumn("ZipCode").AsString()
            .WithColumn("WarehouseId").AsInt32();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}

[Migration(15102023100006)]
public class Migration_15102023100006 : Migration
{
    
    public override void Up()
    {
        Create.Column("Deleted").OnTable("Address").AsBoolean().WithDefaultValue(false).NotNullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}

[Migration(15102023100007)]
public class Migration_15102023100007 : Migration
{
    
    public override void Up()
    {
        Delete.Table("Address");
        
        Create.Table("Address")
            .WithColumn("Id").AsCustom("serial").PrimaryKey().NotNullable()
            .WithColumn("Street").AsString().NotNullable()
            .WithColumn("City").AsString().NotNullable()
            .WithColumn("ZipCode").AsString().NotNullable()
            .WithColumn("WarehouseId").AsInt32().Nullable()
            .WithColumn("Deleted").AsBoolean().WithDefaultValue(false).NotNullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}

[Migration(15102023100008)]
public class AddTenantTable : Migration
{
    
    public override void Up()
    {
        Create.Table("Tenant")
            .WithColumn("Id").AsCustom("serial").PrimaryKey().NotNullable()
            .WithColumn("Name").AsString().NotNullable();
    }

    public override void Down()
    {
    }
}

[Migration(15102023100009)]
public class AddBackOfficeUserTable : Migration
{
    
    public override void Up()
    {
        Create.Table("BackOfficeUser")
            .WithColumn("Id").AsCustom("serial").PrimaryKey().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("TenantId").AsInt32().NotNullable();
    }

    public override void Down()
    {
    }
}

[Migration(15102023100010)]
public class AddOperatorTable : Migration
{
    
    public override void Up()
    {
        Create.Table("Operator")
            .WithColumn("Id").AsCustom("serial").PrimaryKey().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("TenantId").AsInt32().NotNullable()
            .WithColumn("ValidationId").AsInt32().Nullable()
            .WithColumn("WarehouseId").AsInt32().Nullable()
            .WithColumn("Deleted").AsBoolean().NotNullable();
    }

    public override void Down()
    {
    }
}

[Migration(15102023100011)]
public class AddOperatorValidationTable : Migration
{
    public override void Up()
    {
        Create.Table("OperatorValidation")
            .WithColumn("Id").AsCustom("serial").PrimaryKey().NotNullable()
            .WithColumn("OperatorId").AsInt32().NotNullable()
            .WithColumn("BackOfficeUserId").AsInt32().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("WarehouseId").AsInt32().Nullable();
    }

    public override void Down()
    {
    }
}

[Migration(15102023100012)]
public class AddWarehouseTable : Migration
{
    public override void Up()
    {
        Create.Table("Warehouse")
            .WithColumn("Id").AsCustom("serial").PrimaryKey().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Capacity").AsInt32().NotNullable()
            .WithColumn("OperatorId").AsInt32().Nullable();
    }

    public override void Down()
    {
    }
}

[Migration(15102023100013)]
public class AddAddresColumnToWarehouseTable : Migration
{
    public override void Up()
    {
        Create.Column("AddressId").OnTable("Warehouse").AsInt32().Nullable();
    }

    public override void Down()
    {
    }
}

[Migration(15102023100014)]
public class DeleteAndCreateOperatorValidationTable : Migration
{
    public override void Up()
    {
        Delete.Table("OperatorValidation");
        
        Create.Table("OperatorValidation")
            .WithColumn("Id").AsCustom("serial").PrimaryKey().NotNullable()
            .WithColumn("OperatorId").AsInt32().NotNullable()
            .WithColumn("BackOfficeUserId").AsInt32().NotNullable()
            .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
            .WithColumn("WarehouseId").AsInt32().Nullable();
    }

    public override void Down()
    {
    }
}

[Migration(01112023100000)]
public class AddEmailColumnToOperator : Migration
{
    public override void Up()
    {
        Create.Column("Email").OnTable("Operator").AsString().Nullable();
    }

    public override void Down()
    {
    }
}

[Migration(01112023100020)]
public class InsertRoleData : Migration
{
    public override void Up()
    {
        Insert.IntoTable("Role").Row(new
        {
            Name = "USER",
            CreatedAt = System.DateTimeOffset.UtcNow
        });

        Insert.IntoTable("Role").Row(new
        {
            Name = "ADMIN",
            CreatedAt = System.DateTimeOffset.UtcNow
        });
    }

    public override void Down()
    {

    }
}

[Migration(01112023100021)]
public class InsertNewRoleData : Migration
{
    public override void Up()
    {
        Insert.IntoTable("Role").Row(new
        {
            Name = "OPERATOR",
            CreatedAt = System.DateTimeOffset.UtcNow
        });

        Insert.IntoTable("Role").Row(new
        {
            Name = "BACKOFFICEUSER",
            CreatedAt = System.DateTimeOffset.UtcNow
        });
    }

    public override void Down()
    {

    }
}
    







