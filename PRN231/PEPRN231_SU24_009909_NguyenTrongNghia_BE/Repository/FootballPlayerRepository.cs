using BusinessObjects.Dto;
using BusinessObjects.Models;
using Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FootballPlayerRepository : IFootballPlayerRepository
    {
        private readonly FootballPlayerDao _footballPlayerDao;

        public FootballPlayerRepository( FootballPlayerDao footballPlayerDao)
        {
            _footballPlayerDao = footballPlayerDao;
        }

        public async Task<IEnumerable<FootballPlayerResponseDto>> GetFootballPlayer()
        {
            var footballPlayer = await _footballPlayerDao.GetFootballPlayers();
            var footballPlayerResponseDto = footballPlayer.Select(f => new FootballPlayerResponseDto
            {
                FootballPlayerId = f.FootballPlayerId,
                FullName = f.FullName,
                Achievements = f.Achievements,
                Birthday = f.Birthday,
                PlayerExperiences = f.PlayerExperiences,
                Nomination = f.Nomination,
                ClubName = f.FootballClub.ClubName
            });
            return footballPlayerResponseDto;
        }

        public async Task<FootballPlayerResponseDto> GetFootballPlayerId(string id)
        {
            var footballPlayer = await _footballPlayerDao.GetFootballPlayerId(id);
            var footballPlayerResponseDto = new FootballPlayerResponseDto
            {
                FootballPlayerId = footballPlayer.FootballPlayerId,
                FullName = footballPlayer.FullName,
                Achievements = footballPlayer.Achievements,
                Birthday = footballPlayer.Birthday,
                PlayerExperiences = footballPlayer.PlayerExperiences,
                Nomination = footballPlayer.Nomination,
                ClubName = footballPlayer.FootballClub.ClubName
            };
            return footballPlayerResponseDto;
        }

        public async Task<bool> AddFootballPlayer(FootballPlayerDto footballPlayerDto)
        {
            var result = await _footballPlayerDao.AddFootballPlayer(new FootballPlayer
            {
                FullName = footballPlayerDto.FullName,
                Achievements = footballPlayerDto.Achievements,
                Birthday = footballPlayerDto.Birthday,
                PlayerExperiences = footballPlayerDto.PlayerExperiences,
                Nomination = footballPlayerDto.Nomination,
                FootballClubId = footballPlayerDto.FootballClubId,
            });
            return result;
        }

        public async Task<bool> DeleteFootballPlayer (string id)
        {
            var result = await _footballPlayerDao.DeleteFootballPlayer(id);
            return result;
        }

        public async Task<bool> UpdateFootballPlayer (string id,  FootballPlayerDto footballPlayerDto)
        {
            var footballPlayer = new FootballPlayer
            {
                FullName = footballPlayerDto.FullName,
                Achievements = footballPlayerDto.Achievements,
                Birthday = footballPlayerDto.Birthday,
                PlayerExperiences = footballPlayerDto.PlayerExperiences,
                Nomination = footballPlayerDto.Nomination,
                FootballClubId = footballPlayerDto.FootballClubId,
            };
            return await _footballPlayerDao.UpdateFootballPlayer(id , footballPlayer);
        }
    }
}
