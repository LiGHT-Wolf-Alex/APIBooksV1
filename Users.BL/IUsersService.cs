using Common.Domain;

namespace Users.BL
{
    public interface IUsersService
    {
        IReadOnlyCollection<User> GetList(int? offset, string? nameFreeText, int? limit = 10);
        User? GetById(int id);
        User Create(User user);
        User? Update(User user);
        bool Delete(int id);
    }
}
