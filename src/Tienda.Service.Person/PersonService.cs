using Tienda.Domain.Core;
using Tienda.Domain.Entities;
using Tienda.Domain.Interfaces;

namespace Tienda.Service
{
    public class PersonService : IPersonService
    {
        public readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<bool> InsertAsync(PersonEntity entity)
        {
            return await _personRepository.InsertAsync(entity);
        }

        public async Task<(PersonFilter, IEnumerable<PersonEntity>)> UpdateAsync(PersonEntity entity)
        {
            _ = await _personRepository.UpdateAsync(entity);
            return _personRepository.DefaultValues();
        }

        public async Task<(PersonFilter, IEnumerable<PersonEntity>)> SearchAsync(PersonFilter filter)
        {
            var predicate = PredicateBuilder.Create<PersonEntity>(m => m.IsDeleted == false);
            if (!filter.Name.IsNull())
            {
                predicate = predicate.And(m => m.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.OnlyHasImages)
            {
                predicate = predicate.And(m => m.Images != null && m.Images.Any());
            }

            if (filter.OnlyHasVideos)
            {
                predicate = predicate.And(m => m.Videos != null && m.Videos.Any());
            }

            if (filter.Id != Guid.Empty)
            {
                predicate = predicate.And(m => m.Id == filter.Id);
            }

            return await _personRepository.SearchAsync(predicate, filter);
        }
    }
}