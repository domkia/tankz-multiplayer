﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TankzSignalRServer.Data;
using TankzSignalRServer.Models;
using Newtonsoft.Json;
using TankzSignalRServer.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace TankzSignalRServer.Controllers
{
    public class PlayersController
    {
        private readonly TankzContext _context;
        private readonly IHubContext<TestHub> _hubContext;

        public PlayersController(TankzContext context)
        {
            _context = context;
            _hubContext = Startup.mMyHubContext;
        }

        public void AddPlayer(Player player)
        {
            _context.Players.AddAsync(player);
            _context.SaveChangesAsync();
            
        }
        public void RemovePlayer(string ConnId)
        {
            Player player = GetPlayerById(ConnId);
            _context.Players.Remove(player);
            _context.SaveChangesAsync();

        }
        public Player GetPlayerById(string ConnId)
        {
            return _context.Players.FirstOrDefault(c => c.ConnectionId == ConnId);
        }
        public List<Player> GetPlayerList()
        {
            return _context.Players.ToList();
        }


        //public async Task<IActionResult> GetPlayer([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var player = await _context.Players.FindAsync(id);

        //    if (player == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(player);
        //}

        //// PUT: api/Players/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPlayer([FromRoute] int id, [FromBody] Player player)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != player.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(player).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PlayerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        public async Task PostPlayer(Player player)
        {

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

           
        }

        //// DELETE: api/Players/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePlayer([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var player = await _context.Players.FindAsync(id);
        //    if (player == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Players.Remove(player);
        //    await _context.SaveChangesAsync();

        //    return Ok(player);
        //}

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.ID == id);
        }
    }
}