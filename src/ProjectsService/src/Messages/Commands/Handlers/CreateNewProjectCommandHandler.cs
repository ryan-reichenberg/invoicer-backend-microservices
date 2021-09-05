using System.Threading.Tasks;
using Invoicer.Common.Handlers;

namespace ProjectsService.Messages.Commands.Handlers
{
    public class CreateNewProjectCommandHandler: ICommandHandler<CreateNewProjectCommand>
    {
        public Task HandleAsync(CreateNewProjectCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}