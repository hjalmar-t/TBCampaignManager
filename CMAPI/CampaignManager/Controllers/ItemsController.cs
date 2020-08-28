using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampaignManager.Models;
using Microsoft.AspNetCore.Cors;

namespace CampaignManager.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly TabletopCampaignDBContext _context;

        public ItemsController(TabletopCampaignDBContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByOwner(int id)
        {
            //IList<Item> items = null;

            //var ctx = new TabletopCampaignDBContext();
            //items = ctx.Item
            //    .FromSqlRaw("SELECT * FROM Item INNER JOIN OwnedItem ON OwnedItem.ItemID = Item.ItemID WHERE OwnedItem.InventoryID = {0}", id)
            //    .ToList<Item>();

            IEnumerable<Item> itemQuery;
            var ctx = new TabletopCampaignDBContext();

            itemQuery =
                from item in ctx.Item
                join owned in ctx.OwnedItem on item.ItemId equals owned.ItemId
                where owned.InventoryId == id
                select item;

            //if (items.Count == 0)
            //{
            //    return NotFound();
            //}
            //return Ok(items);

            if (itemQuery == null || !itemQuery.Any())
            {
                return NotFound();
            }
            return Ok(itemQuery.ToList());
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }
    }
}
