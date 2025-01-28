﻿using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext _dbContext;

    // Ask CLR To Create object from DbContext (StoreContext)  => To Deal with DB
    public GenericRepository(StoreContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
     => await _dbContext.Set<T>().ToListAsync();

    public async Task<T> GetByIdAsync(int id)
     => await _dbContext.Set<T>().FindAsync(id);


}
