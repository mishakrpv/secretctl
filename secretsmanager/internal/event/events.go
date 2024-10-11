package event

type CreateSecretRequestSent struct {
	ProjectID      string `json:"project_id"`
	Principal      string `json:"principal"`
	Key            string `json:"key"`
	Value          string `json:"value"`
	EcryptionKeyID string `json:"ecryption_key_id"`
}
