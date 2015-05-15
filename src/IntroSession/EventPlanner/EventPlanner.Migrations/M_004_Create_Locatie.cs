using FluentMigrator;

namespace EventPlanner.Migrations
{
    [Migration(4)]
    public class M_004_Create_Locatie : Migration
    {
        public override void Up()
        {
            Create.Table("Locatie")
                .WithColumn("PeriodeId").AsInt32().ForeignKey()
                .WithColumn("StraatId").AsInt32().ForeignKey();
        }

        public override void Down()
        {
        }
    }
}