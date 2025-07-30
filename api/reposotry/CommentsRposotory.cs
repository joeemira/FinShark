using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Mapper;
using api.models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using api.DTOs.Comments;


namespace api.reposotry
{
    public class CommentsRposotory : IcommentsREposotry
    {
        private readonly ApplicationDBContext _context;
        public CommentsRposotory(ApplicationDBContext context)
        {
            _context = context;

        }

        public async Task<Comment> CreateAsync(Comment comment)
         {   //comment.CreatedOn = DateTime.Now;
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();  
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return null;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();

        }

        public async Task<Comment?> GetByIdAsync(int id)

        {
            return await  _context.Comments.FindAsync(id);
            
        }



        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto dto)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null) return null;
            // Update the fields
            dto.ToUpdateComment(existingComment);
            await _context.SaveChangesAsync();
            return existingComment;
        }

        
    

    }
}