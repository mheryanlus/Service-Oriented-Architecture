using AutoMapper;
using ServiceOrientedArchitecture.Common.Models;
using ServiceOrientedArchitecture.Data.Entities;
using ServiceOrientedArchitecture.Repositories;

namespace ServiceOrientedArchitecture.Services;

public class UserService : IUserService
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateAsync(string firstNamae, string lastName)
    {
        var existing_user = _userRepository.AsNoTracking().Where(x => x.FirstName == firstNamae && x.LastName == lastName).FirstOrDefault();    

        if (existing_user != null)
        {
            throw new ApplicationException("User Already Exists");
        }

        var user = new UserEntity()
        {
            FirstName = firstNamae,
            LastName = lastName
        };

        await _userRepository.AddAsync(user);
        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            return;
        }

        await _userRepository.DeleteAsync(user);
    }

    public async Task<IEnumerable<UserEntityDto>> GetAllUsersAsync()
    {
        var allUsers = await _userRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserEntityDto>>(allUsers);
    }
}
