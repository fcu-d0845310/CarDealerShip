namespace CarDealerShip.Services
{
    public interface ICommentService
    {
        Task<bool> DeleteCommentByIdAsync(int id);
    }
}
