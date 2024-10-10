package api

import (
	"net/http"
	"slices"
	"time"

	"github.com/mishakrpv/secretctl/proxy-producer/internal/config"
)

var appropriateSources = []string{
	"[FromBody]",
	"[FromQuery]",
	"[FromForm]",
	"[FromHeader]",
}

type upstreamRoute struct {
	pattern string
	method  string
}

func mapRoutes(config *config.ProxyConfig) (routes map[upstreamRoute]map[string][]string) {
	for _, route := range config.Routes {
		routes[upstreamRoute{
			pattern: route.UpstreamPathTemplate,
			method: route.UpstreamHttpMethod,
		}] = extractParams(route.DownstreamMessage)
	}
	return
}

func extractParams(json map[string]interface{}) map[string][]string {
	params := make(map[string][]string)

	for key, value := range json {
		if strval, ok := value.(string); ok {
			if slices.Contains(appropriateSources, strval) {
				params[strval] = append(params[strval], key)
			}
		}

		if nestedMap, ok := value.(map[string]interface{}); ok {
			nestedKeysBySource := extractParams(nestedMap)
			for source, keys := range nestedKeysBySource {
				params[source] = append(params[source], keys...)
			}
		}
	}

	return params
}

func NewServer() *http.Server {
	config := config.GetProxyConfig("../../proxy-messaging.json")

	routes := mapRoutes(config)

	server := &http.Server{
		Addr:         ":4052",
		Handler:      registerRoutes(routes),
		IdleTimeout:  time.Minute,
		ReadTimeout:  10 * time.Second,
		WriteTimeout: 30 * time.Second,
	}

	return server
}
