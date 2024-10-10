package api

import "net/http"

func registerRoutes(routes map[upstreamRoute]map[string][]string) http.Handler {
	mux := &http.ServeMux{}
	
	for route, _ := range routes {
		mux.HandleFunc(route.pattern, func(w http.ResponseWriter, r *http.Request) {
			
		})
	}

	return mux
}
