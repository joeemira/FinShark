using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using System.Net.NetworkInformation;
using api.Mapper;
using api.DTOs;
using Microsoft.EntityFrameworkCore.Update.Internal;
using api.DTOs.Stocks;
using api.models;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockReposotry _stockRepo;
        public StockController(ApplicationDBContext context, IStockReposotry stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stock = await _stockRepo.GetAllAsync();
            var stockdto = stock.Select(s => s.ToStockDto());
            return Ok(stockdto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {
            var StockModel = stockDto.FromStockCreateDto();
            await _context.Stocks.AddAsync(StockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = StockModel.Id }, StockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRequestDto dto)
        {
            var existingStock = await _context.Stocks.FindAsync(id);
            if (existingStock == null)
            {
                return NotFound();
            }

            dto.ToUpdatedStock(existingStock); // Update fields via mapper
            await _context.SaveChangesAsync();

            return Ok(existingStock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var Stock = await _context.Stocks.FindAsync(id);
            if (Stock == null)
            {
                return NotFound();
            }

            _context.Remove(Stock);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
