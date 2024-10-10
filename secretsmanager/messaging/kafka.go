package messaging

import (
	"github.com/confluentinc/confluent-kafka-go/v2/kafka"
)

type MessageBroker struct {
	consumer *kafka.Consumer
}

func NewMessageBroker(config *brokerConfig) *MessageBroker {
	broker := &MessageBroker{}

	consumer, err := kafka.NewConsumer(&kafka.ConfigMap{
		"bootstrap.servers": config.bootstrapServers,
		"group.id":          config.groupId,
		"auto.offset.reset": config.autoOffsetReset,
	})
	if err != nil {
		panic(err)
	}

	broker.consumer = consumer
	return broker
}

func (b *MessageBroker) StartConsuming() {

}
