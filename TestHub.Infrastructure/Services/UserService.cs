using TestHub.Core.Models;
using TestHub.Core.Dtos;
using TestHub.Infrastructure.Repository;

namespace TestHub.Infrastructure.Services;

public class UserService
{
    private readonly GenericRepository<User> _userRepository;

    public UserService(GenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetUser(UserDto userDto, string passwordHash, string role = "User")
    {
        return new User
        {
            Email = userDto.Email,
            Password = passwordHash,
            Name = userDto.Name,
            Role = role
        };
    }
    
    public IEnumerable<User> GetAll()
    {
        return _userRepository.Get();
    }
    
    public User GetById(int id)
    {
        return _userRepository.GetByID(id);
    }
    
    public void Add(User user)
    {
        user.CreateAt = DateTime.Now;
        _userRepository.Insert(user);
    }

    public void Delete(User userToDelete)
    {
        userToDelete.DeleteAt = DateTime.Now;
        _userRepository.Update(userToDelete);
    }

    public void Update(User userToUpdate, UserDto userChanging)
    {
        userToUpdate.Name = userChanging.Name;
        userToUpdate.Email = userChanging.Email;
        userToUpdate.Password = userChanging.Password;
        userToUpdate.UpdateAt = DateTime.Now;
        
        _userRepository.Update(userToUpdate);
    }
}