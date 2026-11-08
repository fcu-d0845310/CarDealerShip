namespace CarDealerShip.Dtos.Branch
{
    public class BranchListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone {  get; set; } = null!;
        public int InstockCount {  get; set; }

    }
}
