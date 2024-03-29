﻿using AsyncCourse.Accounting.Api.Domain.Commands.Analytics;
using AsyncCourse.Accounting.Api.Models.Analytics.Models;
using AsyncCourse.Accounting.Api.Models.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.AccountingApi.Controllers;

[Route("analytics")]
public class AnalyticsController : Controller
{
    private readonly IUpdateMaxPriceIssueCommand updateMaxPriceIssueCommand;
    private readonly IGetMaxPricesCommand getMaxPricesCommand;
    private readonly IGetBalanceAnalyticsCommand getBalanceAnalyticsCommand;

    public AnalyticsController(
        IUpdateMaxPriceIssueCommand updateMaxPriceIssueCommand,
        IGetMaxPricesCommand getMaxPricesCommand,
        IGetBalanceAnalyticsCommand getBalanceAnalyticsCommand)
    {
        this.updateMaxPriceIssueCommand = updateMaxPriceIssueCommand;
        this.getMaxPricesCommand = getMaxPricesCommand;
        this.getBalanceAnalyticsCommand = getBalanceAnalyticsCommand;
    }

    [HttpPost("update-max-price")]
    public async Task<bool> Update([FromQuery] Guid transactionId)
    {
        await updateMaxPriceIssueCommand.UpdateAsync(transactionId);

        return true;
    }
    
    [HttpGet("get-max-price")]
    public async Task<List<MaxPriceIssueModel>> Get([FromQuery] DateTime from, DateTime to)
    {
        var result = await getMaxPricesCommand.GetAsync(from, to);

        return result.Select(MaxPriceIssueMapping.MapFromDomainResult).ToList();
    }
    
    [HttpGet("show-balances")]
    public async Task<EarnedTodayBalanceModel> ShowBalances()
    {
        // Нужно указывать, сколько заработал топ-менеджмент за сегодня и сколько попугов ушло в минус
        
        var result = await getBalanceAnalyticsCommand.GetAsync();
        
        return EarnedTodayBalanceMapping.MapFromDomainResult(result);
    }
}