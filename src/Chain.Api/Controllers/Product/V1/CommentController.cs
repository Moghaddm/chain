using Chain.Application.Contract.Helpers.Api;
using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Services;
using Chain.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chain.Api.Controllers.Product.V1
{
    [ApiController]
    [Route("api/v{version:apiversion}/comments")]
    [ApiVersion(ConstantHelper.ApiVersion)]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService) => _commentService = commentService;

        [HttpPatch("{commentId}/[action]")]
        public async Task<IActionResult> Update([FromRoute] Guid commentId, [FromBody] CommentDto commentDto)
        {
            await _commentService.UpdateOnProduct(commentId, commentDto);

            return NoContent();
        }

        [HttpDelete("{commentId}/[action]")]
        public async Task<IActionResult> Delete([FromRoute] Guid commentId,[FromBody] Guid productId)
        {
            await _commentService.DeleteFromProduct(commentId,productId);

            return NoContent();
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetComment([FromRoute] Guid commentId)
        {
            var comments = await _commentService.Get(commentId);

            return comments is not null ? Ok(comments) : NotFound();
        }
    }
}
