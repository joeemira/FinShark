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
using api.Helpers;

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
        public async Task<IActionResult> GetAll([FromQuery]QueryObject query)
        {
            
            var stock = await _stockRepo.GetAllAsync(query);
            var stockdto = stock.Select(s => s.ToStockDto());
            return Ok(stockdto);
        }

        [HttpGet("{id:int}")]  
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {   if(!ModelState.IsValid) return BadRequest(ModelState);

            var StockModel = stockDto.CreateDtofromStock();
            var createdStock = await _stockRepo.CreateAsync(StockModel);
            return CreatedAtAction(nameof(GetById), new { id = StockModel.Id }, createdStock.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedStock= await _stockRepo.UpdateAsync(id, dto);
            if (updatedStock == null) return NotFound();

            return Ok(updatedStock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _stockRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
