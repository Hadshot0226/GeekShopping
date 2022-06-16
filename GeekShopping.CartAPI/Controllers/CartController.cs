using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _reposirory;

        public CartController(ICartRepository reposirory)
        {
            _reposirory = reposirory;
        }

        [HttpGet("find-cart/{id}")]   
        public async Task<ActionResult<CartVO>> FindById(string userId)
        {
            var cart = await _reposirory.FindCartByUserId(userId);
            if (cart == null)
            { 
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost("find-add/{id}")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            var cart = await _reposirory.SaveOrUpdateCart(vo);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPut("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            var cart = await _reposirory.SaveOrUpdateCart(vo);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            var status = await _reposirory.RemoveFromCart(id);
            if (!status)
            {
                return BadRequest();
            }
            return Ok(status);
        }
    }
}
