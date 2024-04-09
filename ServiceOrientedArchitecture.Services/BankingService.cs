using ServiceOrientedArchitecture.Data.Entities;
using ServiceOrientedArchitecture.Repositories;
using ServiceOrientedArchitecture.Common.Constants;
using Microsoft.AspNetCore.Http;
using ServiceOrientedArchitecture.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace ServiceOrientedArchitecture.Services;

public class BankingService : IBankingService
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IRepository<TransactionHistoryEntity> _transactionHistoryRepository;

    public BankingService(
        IRepository<UserEntity> userRepository, 
        IRepository<TransactionHistoryEntity> transactionHistoryRepository)
    {
        _userRepository = userRepository;
        _transactionHistoryRepository = transactionHistoryRepository;
    }

    public async Task<IResult> Credit(int userId, float amount)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            return Results.NotFound($"No such User: {userId}.");
        }
        user.Balance += amount;
        await _userRepository.UpdateAsync(user);

        var transaction = new TransactionHistoryEntity()
        {
            UserId = user.Id,
            Ammount = amount,
            TransactionType = TransactionType.Credit,
            TransactionDate = DateTime.UtcNow
        };
        await _transactionHistoryRepository.AddAsync(transaction);

        return Results.Ok();
    }

    public async Task<IResult> Debit(int userId, float amount)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            return Results.NotFound($"No such User: {userId}.");
        }
        if (user.Balance < amount)
        {
            return Results.BadRequest("Not enough Balance!");
        }
        user.Balance -= amount;
        await _userRepository.UpdateAsync(user);

        var transaction = new TransactionHistoryEntity()
        {
            UserId = user.Id,
            Ammount = amount,
            TransactionType = TransactionType.Debit,
            TransactionDate = DateTime.UtcNow
        };
        await _transactionHistoryRepository.AddAsync(transaction);

        return Results.Ok();
    }

    public async Task<IResult> GetHistoryByUserIdAsync(int userId)
    {
        var transactionHistory =  await _transactionHistoryRepository
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Include(x => x.User)
            .Select(x => new TransactionHistoryDto
            {
                UserName = x.User.FirstName + " " + x.User.LastName,
                Ammount = x.Ammount,
                TransactionType = x.TransactionType,
                TransactionDate = x.TransactionDate
            }).ToListAsync();
        return Results.Ok(transactionHistory);
    }
}
