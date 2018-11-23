namespace SquareSoft.Blue.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FUncDateString : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE Function [dbo].[Date_String](@salesDate DateTime)
returns varchar(50)
AS BEGIN
	DECLARE @day NVARCHAR(15)
	DECLARE @intDay NVARCHAR(7)
	DECLARE @month NVARCHAR(15)
	DECLARE @year NVARCHAR(15)
	DECLARE @newDate VARCHAR(50)
	SET @day=DATENAME(DW,@salesDate)
	SET @intDay=DATENAME(DAY,@salesDate)
	SET @month=DATENAME(MONTH,@salesDate)
	SET @year=DATENAME(YEAR,@salesDate)
	SET @newDate= @month+' '+@intDay+' '+@year

	RETURN @newDate
END");
        }
        
        public override void Down()
        {
        }
    }
}
