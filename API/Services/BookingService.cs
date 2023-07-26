using System;
using System.Collections.Generic;
using System.Linq;
using API.Contracts;
using API.DTOs.Bookings;
using API.Models;

namespace API.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository, IEmployeeRepository employeeRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _employeeRepository = employeeRepository;
        }
        public DetailBookingDto? GetDetailByGuid(Guid bookingGuid)
        {
            var resultBooking = _bookingRepository.GetByGuid(bookingGuid);
            if (resultBooking is null)
            {
                return null; // Return null if the booking with the given Guid is not found
            }

            var resultEmployee = _employeeRepository.GetByGuid(resultBooking.EmployeeGuid);
            if (resultEmployee is null)
            {
                return null; // Return null if the associated employee is not found
            }

            var resultRoom = _roomRepository.GetByGuid(resultBooking.RoomGuid);
            if (resultRoom is null)
            {
                return null; // Return null if the associated room is not found
            }

            var detailDto = new DetailBookingDto
            {
                BookingGuid = resultBooking.Guid,
                BookedNik = resultEmployee.NIK,
                BookedBy = resultEmployee.FirstName + " " + resultEmployee.LastName,
                RoomName = resultRoom.Name,
                StartDate = resultBooking.StartDate,
                EndDate = resultBooking.EndDate,
                Status = resultBooking.Status,
                Remarks = resultBooking.Remarks
            };

            return detailDto;
        }


        public IEnumerable<DetailBookingDto> GetALl()
        {
            var resultBooking = _bookingRepository.GetAll();
            if (!resultBooking.Any())
            {
                return Enumerable.Empty<DetailBookingDto>();
            }

            var detailDtos = new List<DetailBookingDto>();
            foreach (var result in resultBooking)
            {
                var resultEmployee = _employeeRepository.GetByGuid(result.EmployeeGuid);
                if (resultEmployee is null)
                {
                    return Enumerable.Empty<DetailBookingDto>();
                }

                var resultRoom = _roomRepository.GetByGuid(result.RoomGuid);
                if (resultRoom is null)
                {
                    return Enumerable.Empty<DetailBookingDto>();
                }

                var toDto = new DetailBookingDto
                {
                    BookingGuid = result.Guid,
                    BookedNik = resultEmployee.NIK,
                    BookedBy = resultEmployee.FirstName + " " + resultEmployee.LastName,
                    RoomName = resultRoom.Name,
                    StartDate = result.StartDate,
                    EndDate = result.EndDate,
                    Status = result.Status,
                    Remarks = result.Remarks
                };

                detailDtos.Add(toDto);

            }

            return detailDtos;
        }

        public IEnumerable<BookingDto> GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return Enumerable.Empty<BookingDto>(); // Booking is null or not found;
            }

            var bookingDtos = new List<BookingDto>();
            foreach (var booking in bookings)
            {
                bookingDtos.Add((BookingDto)booking);
            }

            return bookingDtos; // Booking is found;
        }

        public BookingDto? GetByGuid(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return null; // Booking is null or not found;
            }

            return (BookingDto)booking; // Booking is found;
        }

        public BookingDto? Create(NewBookingDto newBookingDto)
        {
            var booking = _bookingRepository.Create(newBookingDto);
            if (booking is null)
            {
                return null; // Booking is null or not found;
            }

            return (BookingDto)booking; // Booking is found;
        }

        public int Update(BookingDto bookingDto)
        {
            var booking = _bookingRepository.GetByGuid(bookingDto.Guid);
            if (booking is null)
            {
                return -1; // Booking is null or not found;
            }

            Booking toUpdate = bookingDto;
            // You may update other properties here if needed
            var result = _bookingRepository.Update(toUpdate);

            return result ? 1 // Booking is updated;
                : 0; // Booking failed to update;
        }

        public int Delete(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return -1; // Booking is null or not found;
            }

            var result = _bookingRepository.Delete(booking);

            return result ? 1 // Booking is deleted;
                : 0; // Booking failed to delete;
        }
    }
}
