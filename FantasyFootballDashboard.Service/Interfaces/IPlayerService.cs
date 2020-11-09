using FantasyFootballDashboard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.Service.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllUserPlayers();
    }
}
