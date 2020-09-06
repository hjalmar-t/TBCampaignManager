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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            return await _context.Item.ToListAsync();
        }


        [EnableCors("AllowMyOrigin")]
        [Route("owned/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnedItem>> GetOwnedItem(int id)
        {
            var item = await _context.OwnedItem.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [EnableCors("AllowMyOrigin")]
        [Route("ownedBy/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByOwner(int id)
        {
            //IEnumerable<Item> itemQuery;
            var ctx = new TabletopCampaignDBContext();

            var countQuery =
                from owned in ctx.OwnedItem
                group owned by owned.ItemId into g
                select new { ItemId = g.Key, Count = g.Count() };

            //var itemQuery =
            //        from item in ctx.Item
            //        join owned in ctx.OwnedItem on item.ItemId equals owned.ItemId
            //        where owned.InventoryId == id
            //        select new { OwnedItemId = owned.OwnedItemId, ItemId = item.ItemId, InventoryId = owned.InventoryId, Name = item.Name, Weight = item.Weight };

            var itemQuery =
                from count in countQuery.ToList()
                join owned in ctx.OwnedItem on count.ItemId equals owned.ItemId
                join item in ctx.Item on count.ItemId equals item.ItemId
                where owned.InventoryId == id
                //group count by owned.OwnedItemId into g
                //orderby g.Key
                //select g;
                select new { owned.OwnedItemId, item.ItemId, item.Name, item.Weight, count.Count };

            //var itemQuery =
            //    from item in dupQuery.ToList()
            //    select item;

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
        [Route("owned/")]
        [HttpPost]
        public async Task<ActionResult<OwnedItem>> PostOwnedItem(OwnedItem item)
        {
            _context.OwnedItem.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOwnedItem", new { id = item.OwnedItemId }, item);

            return BadRequest("testing post: " + item.ItemId);

            //var itemQuery =
            //    from 

            //return null;
            //_context.Character.Add(character);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCharacter", new { id = character.CharacterId }, character);
        }

        [EnableCors("AllowMyOrigin")]
        [Route("owned/delete/{id}")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<OwnedItem>> DeleteOwnedItem(int id)
        {
            var item = await _context.OwnedItem.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.OwnedItem.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }
    }
}
