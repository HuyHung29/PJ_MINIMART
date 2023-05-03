using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using System.Security.Claims;

namespace MINIMART.API.Controllers
{
    [Authorize]
    public class AddressController : BaseController<Address>
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService) : base(addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> Get(PagingObject filter)
        {
            try
            {
                var accId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                filter.AccountId = Guid.Parse(accId.ToString());

                var result = await _addressService.GetByFilterAndPaging(filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Address address)
        {
            try
            {
                var accId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                address.AccountId = Guid.Parse(accId);

                var result = await _addressService.Insert(address, "");

                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] Address address)
        {
            try
            {
                var accId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                address.AccountId = Guid.Parse(accId);

                var result = await _addressService.Update(id, address, "");

                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }
    }
}
