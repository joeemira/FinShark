using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;   
using api.DTOs.Comments;

namespace api.Interfaces
{
    public interface IcommentsREposotry
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> UpdateAsync(int id, UpdateCommentDto comment);
        Task<Comment> CreateAsync(Comment comment);

        Task<Comment?> DeleteAsync(int id);
    }
} 