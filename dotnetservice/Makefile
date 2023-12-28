DB_URL=postgresql://root:secret@localhost:5432/elympics?sslmode=disable

.PHONY: postgres createdb dropdb psql dbstart

postgres:
	docker run --rm --name elympics-postgres -p 5432:5432 -e POSTGRES_USER=root -e POSTGRES_PASSWORD=secret -e POSTGRES_DB=elympics -d postgres:latest --network elympics
	
dbstart:
	docker start elympics-postgres
	
createdb:
	docker exec -it elympics-postgres createdb --username=root --owner=root elympics

dropdb:
	docker exec -it elympics-postgres dropdb elympics
	
psql:
	docker exec -it elympics-postgres psql --username=root --dbname=elympics
	
apidocker:
	docker run --rm -p 8001:8001 elympics-dotnet --name elympics-dotnet --network elympics
