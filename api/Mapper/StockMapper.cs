using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.DTOs.Stocks;
using api.models;

namespace api.Mapper
{
    public static class StockMapper
    {
        public static StockDTO ToStockDto(this Stock stockModel)
        {
            return new StockDTO
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,

            };

        }

        public static Stock FromStockCreateDto(this CreateStockDto DTOOO)
        {
            return new Stock
            {
                Symbol = DTOOO.Symbol,
                CompanyName = DTOOO.CompanyName,
                Purchase = DTOOO.Purchase,
                LastDiv = DTOOO.LastDiv,
                Industry = DTOOO.Industry,
                MarketCap = DTOOO.MarketCap

            };


        }


    }



}