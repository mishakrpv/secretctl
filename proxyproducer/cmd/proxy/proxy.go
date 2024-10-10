package main

import (
	"log"

	"github.com/mishakrpv/secretctl/proxy-producer/internal/api"
)

func main() {
	server := api.NewServer()

	log.Fatal(server.ListenAndServe())
}
