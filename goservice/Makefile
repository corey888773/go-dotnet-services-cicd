.PHONY:
	start apidocker

init: 
	go mod init github.com/corey888773/go-dotnet-services-cicd

apidocker:
	docker run -p 8000:8000 --network elympics elympics-golang  

start:
	go mod tidy
	go run main.go
