using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RoomRepository : ApiRepository<Room>, IRoomRepository
    {
        public RoomRepository(BookingDbContext context) : base(context) { }


    }
}
