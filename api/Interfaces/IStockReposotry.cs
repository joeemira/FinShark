using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stocks;
using api.Helpers;
using api.models;

namespace api.Interfaces
{
    public interface IStockReposotry
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id ,UpdateRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id );
        Task <bool> StockExists(int id);
    }
}