using AzureWebAPIWithAuth.Models;
using AzureWebAPIWithAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AzureWebAPIWithAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService inventoryService;
        private readonly ILogger<InventoryController> logger;

        public InventoryController(InventoryService inventoryService, ILogger<InventoryController> logger)
        {
            this.inventoryService = inventoryService;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize]
        [Route("InventoryDetails")]
        public async Task<IActionResult> GetInventory()
        {
            try
            {
                return Ok(await inventoryService.GetInventoryList());
            }
            catch (Exception ex)
            {
                Log.Error("Controller Name : " + this.ControllerContext.ActionDescriptor.ControllerName + "Controller" +
                    " \nFailure message : " + ex);
                return this.BadRequest(ex);
            }
        }
        [HttpPost]
        [Authorize]
        [Route("AddInventory")]
        public async Task<IActionResult> AddInventory(Inventory inventory)
        {
            try
            {
                int result = await inventoryService.AddInventory(inventory);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Controller Name : " + this.ControllerContext.ActionDescriptor.ControllerName + "Controller" +
                    " \nFailure message : " + ex);
                return this.BadRequest(ex);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateInventory")]
        public async Task<IActionResult> UpdateInventory(Inventory inventory)
        {
            try
            {
                int result = await inventoryService.UpdateInventory(inventory);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Controller Name : " + this.ControllerContext.ActionDescriptor.ControllerName + "Controller" +
                    " \nFailure message : " + ex);
                return this.BadRequest(ex);
            }
        }
        [HttpDelete]
        [Authorize]
        [Route("DeleteInventory")]
        public async Task<IActionResult> DeleteInventory(int inventoryID)
        {
            try
            {
                int result = await inventoryService.DeleteInventory(inventoryID);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Controller Name : " + this.ControllerContext.ActionDescriptor.ControllerName + "Controller" +
                    " \nFailure message : " + ex);
                return this.BadRequest(ex);
            }
        }
    }
}
