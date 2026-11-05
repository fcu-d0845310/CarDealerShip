namespace CarDealerShip.ViewModels
{
    public class CarImageViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }
    }
}
