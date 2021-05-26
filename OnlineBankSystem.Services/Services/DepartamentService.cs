using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Core.Repositories;
using OnlineBankSystem.Services.Interfaces;

namespace OnlineBankSystem.Services.Services
{
    public class DepartamentService : IDepartamentService
    {
        public IDepartamentRepository _departamentRepository;
        public DepartamentService(IDepartamentRepository departamentRepository)
        {
            _departamentRepository = departamentRepository;
        }

        public async Task<IEnumerable<Departament>> GetDepartamentsAsync()
        {
            Expression<Func<Departament, bool>> filter = null;
            return await _departamentRepository.List(filter);
        }
    }
}
