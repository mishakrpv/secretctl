package messaging

type brokerConfig struct {
	bootstrapServers string
	groupId          string
	autoOffsetReset  string
}
