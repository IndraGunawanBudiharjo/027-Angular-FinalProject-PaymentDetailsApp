using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentDetails.Data;
using PaymentDetails.Models;
using PaymentDetails.Models.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace PaymentDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentDetailController : ControllerBase
    {

        private readonly ApiDbContext _context;

        public PaymentDetailController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var PaymentDetails = await _context.PaymentDetail.ToListAsync();
            return Ok(PaymentDetails);
            // return Ok(new ListMessageResponse()
            // {
            //     message = $"Berhasil",
            //     item = PaymentDetails
            // });
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(PaymentDetail data)
        {
            if (ModelState.IsValid)
            {
                await _context.PaymentDetail.AddAsync(data);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetItem", new { data.PaymentDetailId }, data);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet("{PaymentDetailId}")]
        public async Task<IActionResult> GetItem(int PaymentDetailId)
        {
            var PaymentDetail = await _context.PaymentDetail.FirstOrDefaultAsync(x => x.PaymentDetailId == PaymentDetailId);

            if (PaymentDetail == null)
            {
                return NotFound(new MessageResponse()
                {
                    message = $"Whoops, id: {PaymentDetailId} tidak ditemukan",
                    item = null
                }); 
            }  
                
            return Ok(PaymentDetail);
            // return Ok(new MessageResponse()
            // {
            //     message = $"Berhasil",
            //     item = PaymentDetail
            // });
        }

        [HttpPut("{PaymentDetailId}")]
        public async Task<IActionResult> UpdateItem(int PaymentDetailId, PaymentDetail paymentDetail)
        {
            if (PaymentDetailId != paymentDetail.PaymentDetailId)
                return BadRequest();

            var existItem = await _context.PaymentDetail.FirstOrDefaultAsync(x => x.PaymentDetailId == PaymentDetailId);

            if (existItem == null)
                return NotFound(new MessageResponse()
                {
                    message = $"Whoops, id: {PaymentDetailId} tidak ditemukan",
                    item = null
                });

            existItem.CardOwnerName = paymentDetail.CardOwnerName;
            existItem.CardNumber = paymentDetail.CardNumber;
            existItem.ExpirationDate = paymentDetail.ExpirationDate;
            existItem.SecurityCode = paymentDetail.SecurityCode;

            // Implement the changes on the database level
            await _context.SaveChangesAsync();

            return NoContent();
            //return Ok(new MessageResponse()
            //{
            //    message = $"Data dengan id: {PaymentDetailId} berhasil diupdate",
            //    item = existItem
            //});
        }

        [HttpDelete("{PaymentDetailId}")]
        public async Task<IActionResult> DeleteItem(int PaymentDetailId)
        {
            var existItem = await _context.PaymentDetail.FirstOrDefaultAsync(x => x.PaymentDetailId == PaymentDetailId);

            if (existItem == null)
                return NotFound(new MessageResponse()
                {
                    message = $"Whoops, id: {PaymentDetailId} tidak ditemukan",
                    item = null
                });

            _context.PaymentDetail.Remove(existItem);
            await _context.SaveChangesAsync();

            return NoContent();

            // return Ok(new MessageResponse()
            // {
            //     message = $"Data dengan id: {PaymentDetailId} berhasil dihapus",
            //     item = existItem
            // });
        }
    }
}
