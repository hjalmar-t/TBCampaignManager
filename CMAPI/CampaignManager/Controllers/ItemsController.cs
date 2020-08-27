using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampaignManager.Models;

namespace CampaignManager.Controllers
{
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByOwner(int id)
        {
            IList<Item> items = null;

            using(var ctx = new TabletopCampaignDBContext())
            {
                items = ctx.Item
                    .FromSqlRaw("SELECT Item.name FROM Item INNER JOIN OwnedItem ON OwnedItem.ItemID = Item.ItemID WHERE OwnedItem.InventoryID = {0}", id)
                    .ToList<Item>();
            }

            if(items.Count == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }
    }
}
