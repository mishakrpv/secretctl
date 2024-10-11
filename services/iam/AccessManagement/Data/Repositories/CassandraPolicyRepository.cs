using AccessManagement.Model.Policy;
using Cassandra.Mapping;

namespace AccessManagement.Data.Repositories;

public class CassandraPolicyRepository(CassandraSessionCache cassandra) : IPolicyRepository
{
    private readonly CassandraSessionCache _cassandra = cassandra;
    private IMapper _mapper = null!;
    
    public async Task<Policy> GetPolicyForPrincipal(string projectId, string principalId, string keyspace = "iam")
    {
        throw new NotImplementedException();
        SetSessionAndMapper(keyspace);

        var policies = await _mapper.FetchAsync<Policy>("WHERE project_id = ?", projectId);
    }
    
    private void SetSessionAndMapper(string keyspace)
    {
        var session = _cassandra.GetSession(keyspace);
        _mapper = new Mapper(session);
    }
}