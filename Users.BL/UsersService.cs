using Common.Domain;
using Common.Repositories;

namespace Users.BL;

public class UsersService : IUsersService
{
    private readonly IRepository<User> _usersRepository;

    public UsersService(IRepository<User> usersRepository)
    {
        _usersRepository = usersRepository;
        usersRepository.Add(new User() { Id = 1, Name = "name 1" });
        usersRepository.Add(new User() { Id = 2, Name = "name 2" });
        usersRepository.Add(new User() { Id = 3, Name = "name 3" });
    }


    public IReadOnlyCollection<User> GetList(int? offset, string? nameFreeText, int? limit)
    {
        return _usersRepository.GetList(
            offset,
            limit,
            nameFreeText == null ? null : b => b.Name.Contains(nameFreeText), u => u.Id);
    }

    public User? GetById(int id)
    {
        return _usersRepository.SingleOrDefault(b => b.Id == 1);
    }

    public User Create(User user)
    {
        user.Id = _usersRepository.GetList().Length == 0 ? 1 : _usersRepository.GetList().Max(b => b.Id) + 1;
        return _usersRepository.Add(user);
    }

    public User? Update(User user)
    {
        var userEntity = GetById(user.Id);
        if (userEntity == null)
        {
            return null;
        }

        return _usersRepository.Update(user);
    }

    public bool Delete(int id)
    {
        var userToDelete = GetById(id);
        if (userToDelete == null)
        {
            return false;
        }

        return _usersRepository.Delete(userToDelete);
    }
}