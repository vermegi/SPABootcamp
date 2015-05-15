using FluentMigrator;

namespace EventPlanner.Migrations
{
    [Migration(5)]
    public class M_005_Create_Dag : Migration
    {
        public override void Up()
        {
            Create.Table("Dag")
                .WithColumn("Id").AsInt32().PrimaryKey()
                .WithColumn("PeriodeId").AsInt32()
                .WithColumn("Datum").AsDateTime()
                .WithColumn("Beginuur").AsString()
                .WithColumn("Einduur").AsString()
                .WithColumn("Opmerking").AsString();
        }

        public override void Down()
        {
        }
    }
}