﻿using FinanceApi.Dtos.Comment;
using FinanceApi.Interfaces;
using FinanceApi.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult?> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());

        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if (!await _stockRepo.StockExist(stockId))
            {
                return BadRequest("Stock not found");
            }
            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate());
            if (comment == null)
            {
                return NotFound("comment not found");
            }
            return Ok(comment.ToCommentDto());
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _commentRepo.DeleteAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
