package startup

import (
	"fmt"
	"net/http"
	"os"
	"strconv"
	"time"

	"github.com/gin-gonic/gin"
	"github.com/mishakrpv/secretctl/secrets-manager/internal/controller"
)

var (
	port, _ = strconv.Atoi(os.Getenv("PORT"))
)

func registerRoutes() http.Handler {
	handler := gin.Default()

	api := handler.Group("api/v1")
	api.GET("secrets/{project_id}", controller.GetSecrets)
	api.POST("secrets", controller.CreateSecret)
	api.DELETE("secrets/{secret_id}", controller.DeleteSecret)

	return handler
}

func StartupServer() *http.Server {

	return &http.Server{
		Addr:         fmt.Sprintf(":%d", port),
		Handler:      registerRoutes(),
		IdleTimeout:  time.Minute,
		ReadTimeout:  10 * time.Second,
		WriteTimeout: 30 * time.Second,
	}
}
