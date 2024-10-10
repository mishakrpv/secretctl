package main

import "github.com/mishakrpv/secretctl/secrets-manager/messaging"

func main() {
	broker := &messaging.MessageBroker{}
	go broker.StartConsuming()
}
