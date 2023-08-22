using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;
using MMABackend.Utilities.Extensions;

namespace MMABackend.DataAccessLayer
{
    public static partial class DataSeeding
    {
        private static void MarketSeeding(this UnitOfWork uow)
        {
            uow.Markets.AddRange(
                new Market
                {
                    Id = MarketIds.Usta.ToInt(),
                    Latitude = 42.86526601364874M, 
                    Longitude = 74.57076567819526M,
                    Name = "Уста",
                },
                new Market
                {
                    Id = MarketIds.BishkekPark.ToInt(),
                    Latitude = 142.874706859642295M,
                    Longitude = 74.5900042060683M,
                    Name = "Бишкек парк",
                }
            );
            uow.SaveChangesWithIdentityInsert<Market>();
        }
    }

    enum MarketIds
    {
        Usta = 1,
        BishkekPark = 2,
    }
}