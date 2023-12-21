package api

import (
	"github.com/gin-gonic/gin"
)

type Server struct {
	router *gin.Engine
}

func (server *Server) SetupRouter() {
	router := gin.Default()
	router.GET("/random", server.GetRandomNumber)
	router.GET("/healthcheck", server.HealthCheck)

	server.router = router
}

func (server *Server) Start(httpAddress string) error {
	return server.router.Run(httpAddress)
}
