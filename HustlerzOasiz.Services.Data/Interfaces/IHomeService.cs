using HustlerzOasiz.Services.Data.Models.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HustlerzOasiz.Services.Data.Interfaces
{
    public interface IHomeService
    {
        Task<StatsServiceModel> GetStatsFromAppAsync();
    }
}
