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

        [EnableCors("AllowMyOrigin")]
        [Route("ownedBy/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByOwner(int id)
        {
            //IEnumerable<Item> itemQuery;
            var ctx = new TabletopCampaignDBContext();

            var itemQuery =
                    from item in ctx.Item
                    join owned in ctx.OwnedItem on item.ItemId equals owned.ItemId
                    where owned.InventoryId == id
                    select new { OwnedItemId = owned.OwnedItemId, ItemId = item.ItemId, InventoryId = owned.InventoryId, Name = item.Name, Weight = item.Weight };

            //SELECT COUNT(OwnedItem.ItemID) AS number, MIN(OwnedItem.OwnedItemID) AS firstOwned FROM OwnedItem
            //WHERE OwnedItem.InventoryID = @keyInventory
            //GROUP BY ItemID

            //var test1 =
            //    from owned in ctx.OwnedItem
            //    where owned.InventoryId == id
            //    group owned by owned.ItemId;

            //var test =
            //    from item in test1
            //    select new { id = item.Key, Count = item.Count() };

            //var test =
            //    from item in ctx.Item
            //    join owned in ctx.OwnedItem on item.ItemId equals owned.ItemId
            //    where owned.InventoryId == id
            //    group item by item.ItemId into itemGroup
            //    select new { OwnedItemId = itemGroup.Key, Count = itemGroup.Count() };

            //need query to get
            //{ownedItemId, count}
            //current one gets itemId instead

            if (itemQuery == null || !itemQuery.Any())
            {
                return NotFound();
            }
            return Ok(itemQuery.ToList());
        }

        [EnableCors("AllowMyOrigin")]
        [Route("owned/ownedBy/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OwnedItem>>> GetOwnedItemsByInventoryId(int id)
        {

            IEnumerable<OwnedItem> itemQuery;
            var ctx = new TabletopCampaignDBContext();

            itemQuery =
                from item in ctx.OwnedItem
                where item.InventoryId == id
                select item;

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
