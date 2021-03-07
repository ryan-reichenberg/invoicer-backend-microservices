using System;
using System.Threading.Tasks;
using AuthenticationService.Domain;
using AuthenticationService.Repositories.Documents;
using Invoicer.Common.MongoDB.Repositories;

namespace AuthenticationService.Repositories
{
    internal sealed  class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<UserDocument, Guid> _repository;

        public UserRepository(IMongoRepository<UserDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var user = await _repository.GetAsync(id);

            return user?.AsEntity();
        }

        public async Task<User> GetUserAsync(string email)
        {
            var user = await _repository.GetAsync(x => x.Email == email.ToLowerInvariant());

            return user?.AsEntity();
        }

        public Task AddUserAsync(User user) => _repository.AddAsync(user.AsDocument());
    }
}