using eTickets.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Models.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context) : base(context)
        {
        }
       
    }
}
