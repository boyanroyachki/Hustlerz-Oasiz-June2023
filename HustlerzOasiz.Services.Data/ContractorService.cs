using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HustlerzOasiz.Services.Data
{
    public class ContractorService : IContractorService
    {
        private readonly HustlerzOasizDbContext data;

        public ContractorService(HustlerzOasizDbContext data) => this.data = data;
        public async Task<bool> ContractorExistsById(string userId)
        {
            return await data.Contractors.AnyAsync(c => c.UserId.ToString() == userId);
        }
    }
}
