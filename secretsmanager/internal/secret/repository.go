package secret

type Repository interface {
	GetSecret(projectId string, key string) (Secret, error)
	GetSecrets(projectId string) ([]*Secret, error)
	CreateSecret(s Secret) error
	DeleteSecret(projectId, string, key string) error
}
