using API.Models;
using System;

namespace API.DTOs.Rooms
{
    public class RoomDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public static implicit operator Room(RoomDto roomDto)
        {
            return new Room
            {
                Guid = roomDto.Guid,
                Name = roomDto.Name,
                Floor = roomDto.Floor,
                Capacity = roomDto.Capacity,
                CreatedDate = roomDto.CreatedDate,
                ModifiedDate = roomDto.ModifiedDate
            };
        }

        public static explicit operator RoomDto(Room room)
        {
            return new RoomDto
            {
                Guid = room.Guid,
                Name = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity,
                CreatedDate = room.CreatedDate,
                ModifiedDate = room.ModifiedDate
            };
        }
    }
}
