using API.Models;
using System;

namespace API.DTOs.Rooms
{
    public class NewRoomDto
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public static implicit operator Room(NewRoomDto newRoomDto)
        {
            return new Room
            {
                Guid = Guid.NewGuid(), // Assuming Guid is the primary key of the "Room" entity and generating a new Guid for the new Room entry
                Name = newRoomDto.Name,
                Floor = newRoomDto.Floor,
                Capacity = newRoomDto.Capacity,
                CreatedDate = newRoomDto.CreatedDate,
                ModifiedDate = newRoomDto.ModifiedDate
            };
        }

        public static explicit operator NewRoomDto(Room room)
        {
            return new NewRoomDto
            {
                Name = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity,
                CreatedDate = room.CreatedDate,
                ModifiedDate = room.ModifiedDate
            };
        }
    }
}
