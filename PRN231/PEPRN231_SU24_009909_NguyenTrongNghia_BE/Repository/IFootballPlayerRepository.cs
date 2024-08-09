using BusinessObjects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IFootballPlayerRepository
    {
        Task<IEnumerable<FootballPlayerResponseDto>> GetFootballPlayer();
        Task<FootballPlayerResponseDto> GetFootballPlayerId(string id);
        Task<bool> AddFootballPlayer(FootballPlayerDto footballPlayerDto);
        Task<bool> DeleteFootballPlayer(string id);
    }
}
