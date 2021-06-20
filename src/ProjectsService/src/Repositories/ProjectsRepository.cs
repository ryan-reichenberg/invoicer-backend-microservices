using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectsService.Domain;

namespace ProjectsService.Repositories
{
    public class ProjectsRepository : IProjectRepository
    {
        public Task<Project> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> FindAllProjectsByUserId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Project> SaveAsync(Project entity)
        {
            throw new NotImplementedException();
        }

        public Task<Project> UpdateAsync(Project entity)
        {
            throw new NotImplementedException();
        }

        public Task<Project> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}