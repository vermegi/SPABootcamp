using FluentMigrator;

namespace EventPlanner.Migrations
{
    [Migration(2)]
    public class M_002_Create_Periode : Migration
    {
        public override void Up()
        {
            Create.Table("Periode")
                .WithColumn("Id").AsInt32().PrimaryKey()
                .WithColumn("EvenementId").AsInt32()
                .WithColumn("BeginPeriode").AsDateTime()
                .WithColumn("EindePeriode").AsDateTime()
                .WithColumn("Opmerking").AsString();
        }

        public override void Down()
        {
        }
    }
}