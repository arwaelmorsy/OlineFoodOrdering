namespace FoodAwayApii.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listCusinesinRest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cuisines", "Restaurant_RestaurantId", c => c.Int());
            CreateIndex("dbo.Cuisines", "Restaurant_RestaurantId");
            AddForeignKey("dbo.Cuisines", "Restaurant_RestaurantId", "dbo.Restaurants", "RestaurantId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cuisines", "Restaurant_RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Cuisines", new[] { "Restaurant_RestaurantId" });
            DropColumn("dbo.Cuisines", "Restaurant_RestaurantId");
        }
    }
}
