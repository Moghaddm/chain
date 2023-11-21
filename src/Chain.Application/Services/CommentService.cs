using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Repositories;
using Chain.Application.Contract.Ports.Services;
using Chain.Domain.Entities;

namespace Chain.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _commentRepository;
        private readonly IProductRepository _productRepository;

        public CommentService(
            IProductRepository productRepository,
            ICommentRepository commentRepository,
            IUnitOfWork unitOfWork)
            => (_commentRepository, _productRepository, _unitOfWork) = (commentRepository, productRepository, unitOfWork);

        public async Task AddOnProduct(Guid productId, CommentDto commentDto)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            Comment comment = new(commentDto.WriterAlias,
                commentDto.Title,
                commentDto.Description,
                commentDto.Gmail,
                commentDto.Suggest,
                commentDto.DateTimeCommented,
                commentDto.RateNumber);

            comment.Product = product;

            product.Comments.Add(comment);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteFromProduct(Guid id, Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            var comment = await _commentRepository.GetByIdAsync(id);

            product.Comments.Remove(comment!);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateOnProduct(Guid id, CommentDto commentDto)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            comment.Update(commentDto.WriterAlias,
                commentDto.Title,
                commentDto.Description,
                commentDto.Gmail,
                commentDto.Suggest,
                commentDto.DateTimeCommented,
                commentDto.RateNumber);

            await _unitOfWork.SaveChangesAsync();
        }

        public async ValueTask<CommentDto> Get(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment is null)
                return null;

            CommentDto commentDto = new(
                comment!.Id,
                comment.WriterAlias,
                comment.Title,
                comment.Description,
                comment.Gmail,
                comment.DateTimeCommented,
                comment.Suggest,
                comment.VoteUps,
                comment.VoteDowns,
                comment.RateNumber);

            return commentDto;
        }

        public async ValueTask<IEnumerable<CommentDto>> GetProductComments(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
                return Enumerable.Empty<CommentDto>();

            return product.Comments.Select(
                c => new CommentDto(
                c.Id,
                c.WriterAlias,
                c.Title,
                c.Description,
                c.Gmail,
                c.DateTimeCommented,
                c.Suggest,
                c.VoteUps,
                c.VoteDowns,
                c.RateNumber));
        }
    }
}
