namespace CarDealerShip.Constants
{
    public static class CarStatus
    {
        public const string InStock = "在庫";
        public const string Reserved = "保留";
        public const string Sold = "已售出";
        public const string Removed = "下架";

        public static readonly string[] All = { InStock, Reserved, Sold, Removed };
    }
}
