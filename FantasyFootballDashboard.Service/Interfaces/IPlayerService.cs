using FantasyFootballDashboard.Models;
using System.Collections.Generic;

namespace FantasyFootballDashboard.Service.Interfaces
{
    public interface IPlayerService
    {
        public List<Player> GetAllUserPlayers();
    }
}
