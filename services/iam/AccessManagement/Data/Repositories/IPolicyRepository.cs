using AccessManagement.Model.Policy;

namespace AccessManagement.Data.Repositories;

public interface IPolicyRepository
{
    public Task<Policy> GetPolicyForPrincipal(string projectId, string principalId, string keyspace);
}