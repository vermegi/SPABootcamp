using FluentMigrator;

namespace EventPlanner.Migrations
{
    [Migration(3)]
    public class M_003_Create_Straat : Migration
    {
        public override void Up()
        {
            Create.Table("Straat")
                .WithColumn("Id").AsInt32().PrimaryKey()
                .WithColumn("Straatnaam").AsString()
                .WithColumn("Postcode").AsString()
                .WithColumn("Gemeente").AsString();
        }

        public override void Down()
        {
        }
    }
}