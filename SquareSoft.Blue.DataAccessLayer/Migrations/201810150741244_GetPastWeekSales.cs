namespace SquareSoft.Blue.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GetPastWeekSales : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE PROCEDURE Get_PastWeekSales
AS
Select products.product_name, product_sales.quantity, product_sales.amount_paid, product_sales.sales_date, product_sales.original_price
From product_sales left outer join products on products.id=product_sales.product_id
Where DATEDIFF(WEEK,sales_date,SYSDATETIME())>0 and DATEDIFF(Month,sales_date,SYSDATETIME())=1 ;
RETURN 0");
        }
        
        public override void Down()
        {
            Sql(@"Drop Procedure Get_PastWeekSales");
        }
    }
}
