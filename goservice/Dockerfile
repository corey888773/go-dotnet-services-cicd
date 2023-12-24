# stage Build
FROM golang:1.21.5-alpine3.19 AS builder
WORKDIR /app
COPY . .
RUN go build -o main main.go

# stage Run
FROM alpine:3.19
WORKDIR /app
COPY --from=builder /app/main .
COPY start.sh .
RUN chmod +x start.sh

EXPOSE 8000
CMD [ "/app/main" ]
ENTRYPOINT ["/app/start.sh"]
