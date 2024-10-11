CREATE KEYSPACE IF NOT EXISTS iam
    WITH replication = {'class': 'SimpleStrategy', 'replication_factor' : 3};

CREATE TABLE iam.policies (
    policy_id uuid PRIMARY KEY,
    project_id uuid,
    version text,
    statements set<frozen<policy_statement>>,
    created_at timestamp,
    updated_at timestamp
);

CREATE TYPE iam.policy_statement (
    effect text,
    principal text,
    resource set<text>,
    action set<text>
);