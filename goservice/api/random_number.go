package api

import (
	"math/rand"
	"net/http"
	"time"

	"github.com/gin-gonic/gin"
)

type randomNumberRequest struct {
	Min  int `json:"min" binding:"required"`
	Max  int `json:"max" binding:"required"`
	Seed int `json:"seed"`
}

type randomNumberResponse struct {
	Number int `json:"number"`
}

func (server *Server) GetRandomNumber(ctx *gin.Context) {
	var req randomNumberRequest
	if err := ctx.ShouldBindJSON(&req); err != nil {
		ctx.JSON(http.StatusBadRequest, gin.H{
			"message": "Invalid request",
			"status":  http.StatusBadRequest,
		})
		return
	}

	if req.Min >= req.Max || req.Min < 0 || req.Max < 0 {
		ctx.JSON(http.StatusBadRequest, gin.H{
			"message": "Min must be less than max and both must be positive",
			"status":  http.StatusBadRequest,
		})
		return
	}

	var source rand.Source
	if req.Seed == 0 {
		source = rand.NewSource(time.Now().UnixNano())
	} else {
		source = rand.NewSource(int64(req.Seed))
	}

	rand := rand.New(source)

	response := randomNumberResponse{
		Number: rand.Intn(req.Max-req.Min) + req.Min,
	}

	ctx.JSON(http.StatusOK, response)
}
