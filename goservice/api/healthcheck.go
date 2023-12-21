package api

import (
	"net/http"

	"github.com/gin-gonic/gin"
)

func (server *Server) HealthCheck(ctx *gin.Context) {
	ctx.JSON(http.StatusOK, gin.H{
		"healthy": "OK",
		"status":  http.StatusOK,
	})
}
