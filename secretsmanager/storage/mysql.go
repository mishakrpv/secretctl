package storage

import (
	"database/sql"

	_ "github.com/go-sql-driver/mysql"
	"github.com/mishakrpv/secretctl/secrets-manager/internal/secret"
)

var (
	DB        *sql.DB
	StmtIns   *sql.Stmt
	StmtQuery *sql.Stmt
)

var _ secret.Repository = (*MySqlRepository)(nil)

type MySqlRepository struct {
	db     *sql.DB
	insert *sql.Stmt
	query  *sql.Stmt
}

func (r *MySqlRepository) CreateSecret(s secret.Secret) error {
	panic("unimplemented")
}

func (r *MySqlRepository) DeleteSecret(projectId string, string string, key string) error {
	panic("unimplemented")
}

func (r *MySqlRepository) GetSecret(projectId string, key string) (secret.Secret, error) {
	panic("unimplemented")
}

func (r *MySqlRepository) GetSecrets(projectId string) ([]*secret.Secret, error) {
	panic("unimplemented")
}

func Prepare(dsn string) *MySqlRepository {
	db, err := sql.Open("mysql", dsn)
	if err != nil {
		panic(err.Error())
	}
	insert, err := db.Prepare("INSERT INTO secret VALUES( ?, ? )")
	if err != nil {
		panic(err.Error())
	}
	query, err := db.Prepare("SELECT * FROM secret WHERE project_id = ? AND key = ?")
	if err != nil {
		panic(err.Error())
	}
	return &MySqlRepository{
		db:     db,
		insert: insert,
		query:  query,
	}
}

func (r *MySqlRepository) Shutdown() {
	r.query.Close()
	r.insert.Close()
	r.db.Close()
}
