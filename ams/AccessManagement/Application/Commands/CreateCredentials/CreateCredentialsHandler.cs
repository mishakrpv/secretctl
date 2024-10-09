using AccessManagement.Data;
using AccessManagement.Model;
using MediatR;

namespace AccessManagement.Application.Commands.CreateCredentials;

public class CreateCredentialsHandler(AppDbContext context) : IRequestHandler<CreateCredentialsRequest, Credentials>
{
    private readonly AppDbContext _context = context;
    
    public Task<Credentials> Handle(CreateCredentialsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}