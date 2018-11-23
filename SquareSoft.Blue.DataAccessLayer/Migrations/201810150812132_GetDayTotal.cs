namespace SquareSoft.Blue.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GetDayTotal : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE PROCEDURE [dbo].[Get_DayTotal]
AS
BEGIN
	
	SET NOCOUNT ON;
	select top 10 SUM(sales_amount) as amount,dbo.Date_String(sales_date) as dates from sales group by dbo.Date_String(sales_date)
 order by Convert(datetime,dbo.Date_String(sales_date)) desc

END");
        }
        
        public override void Down()
        {
            Sql(@"Drop Procedure [dbo].[Get_DayTotal]");
        }
    }
}
