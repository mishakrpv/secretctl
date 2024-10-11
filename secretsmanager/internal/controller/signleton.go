package controller

import (
	"os"

	"github.com/mishakrpv/secretctl/secrets-manager/storage"
)

var Repository = storage.Prepare(os.Getenv("DSN"))
