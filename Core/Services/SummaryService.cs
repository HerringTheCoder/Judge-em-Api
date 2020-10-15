using System;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using Storage.Tables;

namespace Core.Services
{
    public class SummaryService : ISummaryService
    {
        public Task<Summary> Generate(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
