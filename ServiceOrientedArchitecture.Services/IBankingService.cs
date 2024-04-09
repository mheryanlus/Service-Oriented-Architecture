using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceOrientedArchitecture.Services;

public interface IBankingService
{
    Task<IResult> Debit(int userId, float amount);

    Task<IResult> Credit(int userId, float amount);

    Task<IResult> GetHistoryByUserIdAsync(int userId);
}
