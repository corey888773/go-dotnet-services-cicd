package main

import "github.com/corey888773/go-dotnet-services-cicd/api"

func main() {
	server := api.Server{}
	server.SetupRouter()
	server.Start(":8080")
}
