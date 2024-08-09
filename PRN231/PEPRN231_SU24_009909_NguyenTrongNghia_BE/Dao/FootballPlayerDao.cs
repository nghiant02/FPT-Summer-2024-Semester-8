using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class FootballPlayerDao
    {
        private readonly EnglishPremierLeague2024DbContext _context;

        public FootballPlayerDao()
        {
            _context = new EnglishPremierLeague2024DbContext();
        }

        public async Task<IEnumerable<FootballPlayer>> GetFootballPlayers()
        {
            return await _context.FootballPlayers.Include(f => f.FootballClub).ToListAsync();
        }

        public async Task<FootballPlayer> GetFootballPlayerId(string id)
        {
            return await _context.FootballPlayers.Include(f => f.FootballClub).FirstOrDefaultAsync(f => f.FootballClubId == id);
        }

        public async Task<bool> AddFootballPlayer(FootballPlayer footballPlayer)
        {
            try
            {
                footballPlayer.FootballPlayerId = Guid.NewGuid().ToString();
                _context.FootballPlayers.Add(footballPlayer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateFootballPlayer(string id, FootballPlayer footballPlayer)
        {
            try
            {
                var exitPlayer = await _context.FootballPlayers.FindAsync(id);

                exitPlayer.FullName = footballPlayer.FullName;
                exitPlayer.Achievements = footballPlayer.Achievements;
                exitPlayer.Birthday = footballPlayer.Birthday;
                exitPlayer.PlayerExperiences = footballPlayer.PlayerExperiences;
                exitPlayer.Nomination = footballPlayer.Nomination;
                exitPlayer.FootballClubId = footballPlayer.FootballClubId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteFootballPlayer(string id)
        {
            try
            {
                var player = await _context.FootballPlayers.FindAsync(id);
                _context.FootballPlayers.Remove(player);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
