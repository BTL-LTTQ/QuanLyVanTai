using QuanLyVanTai.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyVanTai.BLL.Services
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<List<RevenueByDateDto>> GetRevenueByDateAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<List<RevenueByRouteDto>> GetRevenueByRouteAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<List<RecentTransactionDto>> GetRecentTransactionsAsync(int limit = 10);
    }
}
