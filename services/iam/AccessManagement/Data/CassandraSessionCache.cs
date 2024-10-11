using System.Collections.Concurrent;
using Cassandra;
using ISession = Cassandra.ISession;

namespace AccessManagement.Data;

public class CassandraSessionCache(ICluster cluster)
{
    private readonly ICluster _cluster = cluster;
    private readonly ConcurrentDictionary<string, Lazy<ISession>> _sessions = new();

    public ISession GetSession(string keyspace)
    {
        if (!_sessions.ContainsKey(keyspace))
            _sessions.GetOrAdd(keyspace, key => new Lazy<ISession>(() => 
                _cluster.Connect(key)));

        var result = _sessions[keyspace];

        return result.Value;
    }
}