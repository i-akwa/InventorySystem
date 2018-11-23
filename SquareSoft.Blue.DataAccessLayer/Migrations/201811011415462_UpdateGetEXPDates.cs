namespace SquareSoft.Blue.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGetEXPDates : DbMigration
    {
        public override void Up()
        {
            Sql(@"Alter PROCEDURE [dbo].[Get_ExpiredProducts]
AS
	SELECT TOP 800
      [product_name]
	  ,[bq_number]
      ,[quantity]
	  ,[unit_price]
      ,Convert(Varchar(10),[last_stock_date],103) as Last_Stock_Date
      ,CONVERT(varchar(10),stocks.[expDate],103) as Exp_Date
  FROM [blueinventory].[dbo].[products] left outer join stocks
  on stocks.product_BQ=products.bq_number
  where DATEDIFF(MONTH,GETDATE(),stocks.expDate) < 4
RETURN 0");
        }
        
        public override void Down()
        {
            Sql(@"Drop Procedure Get_ExpiredProducts");
        }
    }
}
