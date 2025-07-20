using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.reposotry
{
    public class StockReposotry : IStockReposotry
    {

        private readonly ApplicationDBContext _context;
        public StockReposotry(ApplicationDBContext context)
        { 
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }
    }


}