using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stocks;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;
using api.Mapper;
using api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace api.reposotry
{
    public class StockReposotry : IStockReposotry
    {

        private readonly ApplicationDBContext _context;
        public StockReposotry(ApplicationDBContext context)
        { 
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null )
            {
                return null;
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync([FromQuery]QueryObject query)
        {
            var stock = _context.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stock = stock.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stock = stock.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stock = query.IsDecsending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
                }
                
            }


             var skipNumber = (query.PageNumber - 1) * query.PageSize;


            return await stock.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
            
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateRequestDto dto)
        {

            
                var existingStock = await _context.Stocks.FindAsync(id);
                if (existingStock == null) return null;
                // Update the fields
                dto.ToUpdatedStock(existingStock);
                await _context.SaveChangesAsync();
                return existingStock;
            }

            


        }
}
    


