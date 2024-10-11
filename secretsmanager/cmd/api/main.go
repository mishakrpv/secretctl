package main

import (
	"log"

	"github.com/mishakrpv/secretctl/secrets-manager/internal/controller"
	"github.com/mishakrpv/secretctl/secrets-manager/internal/startup"
)

func main() {
	server := startup.StartupServer()

	err := (server.ListenAndServe())
	controller.Repository.Shutdown()
	if err != nil {
		log.Fatal(err)
	}
}
