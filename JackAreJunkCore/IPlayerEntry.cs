using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JackAreJunk
{
    public interface IPlayerEntry
    {
        Task<List<string>> GetPlayerNames();
    }
}
