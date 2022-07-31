using eTickets.Data.Base;
using eTickets.Models;
using eTickets.Models.Data;

namespace eTickets.Data.Services
{
    public class CinemasService : EntityBaseRepository<Cinema> , ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context)
        {
        }
    }
}
