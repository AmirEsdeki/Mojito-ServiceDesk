using Mojito.ServiceDesk.Application.Common.Interfaces.Services.Common;
using System;

namespace Mojito.ServiceDesk.Infrastructure.Services.Common
{
    class RandomNumberGeneratorService : IRandomService
    {
        public int GenerateRandomInt(int from, int to)
        {
            return new Random().Next(from, to);
        }
    }
}
