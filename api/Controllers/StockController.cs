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
namespace api.Controllers
{

    [Route("api/stock")]
    [ApiController]

    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {

            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stock = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());
            return Ok(stock);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var stock = _context.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound();
            }


            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockDto stockDto)
        {
            var StockModel = stockDto.FromStockCreateDto();
            _context.Stocks.Add(StockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = StockModel.Id }, StockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateRequestDto reqDto)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            stock.Symbol = reqDto.Symbol;
            stock.CompanyName = reqDto.CompanyName;
            stock.Purchase = reqDto.Purchase;
            stock.LastDiv = reqDto.LastDiv;
            stock.Industry = reqDto.Industry;
            stock.MarketCap = reqDto.MarketCap;

            _context.SaveChanges();
            return Ok(stock.ToStockDto());

        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var Stock = _context.Stocks.Find(id);
            if (Stock == null)
            {
                return NotFound();
            }
            _context.Remove(Stock);
            _context.SaveChanges();
            return NoContent();
        }

    }
    
}