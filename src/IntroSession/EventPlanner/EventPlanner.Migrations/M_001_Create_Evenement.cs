using FluentMigrator;

namespace EventPlanner.Migrations
{
    [Migration(1)]
    public class M_001_Create_Evenement : Migration
    {
        public override void Up()
        {
            Create.Table("Evenement")
                .WithColumn("Id").AsInt32().PrimaryKey()
                .WithColumn("Organisatorid").AsInt32().Nullable()
                .WithColumn("Titel").AsString()
                .WithColumn("Omschrijving").AsString()
                .WithColumn("DatumBeslissing").AsDateTime().Nullable()
                .WithColumn("Eigenaar").AsString()
                .WithColumn("Optie").AsBoolean().Nullable()
                .WithColumn("MuziekVergunning").AsBoolean().Nullable();
        }

        public override void Down()
        {
        }
    }
}