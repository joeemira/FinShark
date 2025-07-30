using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.reposotry;
using api.Mapper;
using AutoMapper.Configuration.Annotations;
using api.DTOs.Comments;

namespace api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        public readonly IcommentsREposotry _CommentRepo;
        public readonly IStockReposotry _StockRepo;
        public CommentsController(IcommentsREposotry CommentRepo, IStockReposotry stockRepo)
        {
            _CommentRepo = CommentRepo;
            _StockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _CommentRepo.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _CommentRepo.GetByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment.ToCommentDto());

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto dto)
        { if(!ModelState.IsValid) return BadRequest(ModelState);

            var updatedComment = await _CommentRepo.UpdateAsync(id, dto);
            if (updatedComment == null) return NotFound();
            return Ok(updatedComment.ToCommentDto());
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> createcomment([FromRoute] int id, CreateCommentDto commentdto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            
            if (!await _StockRepo.StockExists(id)) return NotFound("Stock not found");
            var comment = commentdto.ToCommentFromDto(id);
            await _CommentRepo.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());



        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _CommentRepo.DeleteAsync(id);
            if (comment == null) return NotFound();
            return NoContent();
        }
    }
    
}