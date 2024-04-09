using Microsoft.AspNetCore.Mvc;
using ServiceOrientedArchitecture.Dtos;
using ServiceOrientedArchitecture.Services;

namespace ServiceOrientedArchitecture.Controllers;

[ApiController]
[Route("v1/[controller]/[action]")]
public class UserBankingController : ControllerBase
{
    private readonly IBankingService _bankingService;

    public UserBankingController(IBankingService bankingService)
    {
        _bankingService = bankingService;
    }

    [HttpPost]
    public async Task<IResult> Debit([FromBody] TransactionRequest request)
    {
        var result = await _bankingService.Debit(request.UserId, request.Amount);
        return result;
    }

    [HttpPost]
    public async Task<IResult> Credit([FromBody] TransactionRequest request)
    {
        var result = await _bankingService.Credit(request.UserId, request.Amount);
        return result;
    }

    [HttpGet]
    public async Task<IResult> GetHistory([FromQuery] int userId)
    {
        var result = await _bankingService.GetHistoryByUserIdAsync(userId);
        return result;
    }
}
