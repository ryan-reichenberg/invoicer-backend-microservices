using System;
using System.Threading.Tasks;
using AuthenticationService.Domain;
using AuthenticationService.Repositories.Documents;
using Invoicer.Common.MongoDB.Repositories;

namespace AuthenticationService.Repositories
{
    internal sealed class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshTokenDocument, Guid> _repository;

        public RefreshTokenRepository(IMongoRepository<RefreshTokenDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<RefreshToken> GetTokenAsync(string token)
        {
            var refreshToken = await _repository.GetAsync(x => x.Token == token);

            return refreshToken?.AsEntity();
        }

        public Task AddTokenAsync(RefreshToken token) => _repository.AddAsync(token.AsDocument());

        public Task UpdateTokenAsync(RefreshToken token) => _repository.UpdateAsync(token.AsDocument());
    }
}