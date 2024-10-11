package secret

import (
	"time"

	"github.com/google/uuid"
)

type Secret struct {
	ProjectID      uuid.UUID `json:"project_id"`
	Principal      string    `json:"principal"`
	Key            string    `json:"key"`
	ValueEncrypted string    `json:"value"`
	CreatedAt      time.Time `json:"created_at"`
}

func New(projectId uuid.UUID, principal string, key string, valueEncrypted string) *Secret {
	return &Secret{
		ProjectID:      projectId,
		Principal:      principal,
		Key:            key,
		ValueEncrypted: valueEncrypted,
		CreatedAt:      time.Now().UTC(),
	}
}
