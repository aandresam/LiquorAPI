
using Microsoft.EntityFrameworkCore;
using liquorApi.Exceptions;
using liquorApi.Context.Entities;
using liquorApi.Context;
using liquorApi.Models;

namespace liquorApi.Services;

    public interface IUsersService{

        Task<IEnumerable<UserDtoOut>> GetAll();

        Task<UserDtoOut> GetById(int id);

        Task Update(UserDtoIn user);

        Task Delete(int id);

    }

public class UsersService : IUsersService
{

    private readonly LicoresDbContext _context;

    public UsersService(LicoresDbContext context)
    {
        this._context = context;
    }


    public async Task<IEnumerable<UserDtoOut>> GetAll()
    {
        return this.toUsersDtoOut(await this._context.Users.ToListAsync());
    }


    public async Task<UserDtoOut> GetById(int id)
    {
        var existUser = await this._context.Users.FindAsync(id);

        if (existUser is null) 
            throw new UserNotFoundException("User not found by id: " + id);

        return this.toUserDtoOut(existUser);
    }


    public async Task Update(UserDtoIn userDtoIn)
    {
        var existUser = await this._context.Users.FindAsync(userDtoIn.Id);

        if (existUser is null) 
            throw new UserNotFoundException("User not found by id: " + userDtoIn.Id);
        
        await this.updateUser(userDtoIn, existUser);
    }


    public async Task Delete(int id)
    {
        var existUser = await this._context.Users.FindAsync(id);

        if (existUser is null) 
            throw new UserNotFoundException("User not found by id: " + id);

        this._context.Users.Remove(existUser);
        await this._context.SaveChangesAsync();
    }


    private UserDtoOut toUserDtoOut(User user)
    {
        UserDtoOut userDtoOut = new UserDtoOut();
        userDtoOut.Id = user.Id;
        userDtoOut.Name = user.Name;
        userDtoOut.LastName = user.LastName;
        userDtoOut.Email = user.Email;
        userDtoOut.PhoneNumber = user.PhoneNumber;

        return userDtoOut;
    }

    private IEnumerable<UserDtoOut> toUsersDtoOut(IEnumerable<User> users)
    {
        IList<UserDtoOut> usersDtoOut = new List<UserDtoOut>();
        foreach (var user in users)
        {
            usersDtoOut.Add(toUserDtoOut(user));
        }
        return usersDtoOut;
    }

    private async Task updateUser(UserDtoIn userDtoIn, User user)
    {
        user.Id = userDtoIn.Id;
        user.Name = userDtoIn.Name;
        user.LastName = userDtoIn.LastName;
        user.Email = userDtoIn.Email;
        user.Password = userDtoIn.Password;
        user.PhoneNumber = userDtoIn.PhoneNumber;

        await this._context.SaveChangesAsync();
    }

}
