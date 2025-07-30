using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.DTOs.Comments;
using api.models;

namespace api.Mapper
{
    public static class CommentMaper
    {
        public static CommentDto ToCommentDto(this Comment CommentModel)
        {
            return new CommentDto
            {
                Id = CommentModel.Id,
                Title = CommentModel.Title,
                Content = CommentModel.Content,
                CreatedOn = CommentModel.CreatedOn,
                StockId = CommentModel.StockId
            };

        }
        public static Comment ToUpdateComment(this UpdateCommentDto updateComment, Comment CommentModel)
        {
            CommentModel.Title = updateComment.Title;
            CommentModel.Content = updateComment.Content;

            return CommentModel;

        }

        public static Comment ToCommentFromDto(this CreateCommentDto commentdto, int stockId)
        { 
            return new Comment
            {
                Title = commentdto.Title,
                Content = commentdto.Content,   
                StockId = stockId
            }; 


        }
    }


}