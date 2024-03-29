﻿using System.Linq.Expressions;
using DLL.Context;
using DLL.Interfaces;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository {
    public class PersonRepository : BaseRepository<Person> {
        public PersonRepository(CinemaContext context) : base(context) { }
        public override async Task<IReadOnlyCollection<Person>> GetAllAsync() {
            return await Entities.Include(user => user.User)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
        public override async Task<IReadOnlyCollection<Person>> FindByConditionAsync(
                Expression<Func<Person, bool>> predicate) {
            return await Entities.Where(predicate)
                                 .Include(user => user.User)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
    }
}