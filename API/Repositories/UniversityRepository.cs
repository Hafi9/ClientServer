﻿using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UniversityRepository : ApiRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingDbContext context) : base(context) { }


    }
}
