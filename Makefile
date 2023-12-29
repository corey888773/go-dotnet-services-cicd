RELEASE_NAME=elympics-v0
NAMESPACE=elympics
VALUES_FILE=./helm-chart/values/development.yaml
REVISION=1

.PHONY:
	helminstall helmdel helmupgrade helmrollback automation

composeup:
	docker-compose up -d --build --remove-orphans

composedown:
	docker-compose down --remove-orphans

composepush:
	docker-compose push

helminstall:
	helm install $(RELEASE_NAME) ./helm-chart/ --namespace $(NAMESPACE) --create-namespace --values $(VALUES_FILE) 

helmdelete:
	helm uninstall $(RELEASE_NAME) --namespace $(NAMESPACE)

helmupgrade:
	helm upgrade $(RELEASE_NAME) ./helm-chart/ --namespace $(NAMESPACE) --values $(VALUES_FILE)

helmrollback:
	helm rollback $(RELEASE_NAME) $(REVISION) --namespace $(NAMESPACE)
	
mkdashboard:
	minikube dashboard 

mkurl:
	minikube service $(RELEASE_NAME)-dotnet-api-service --namespace $(NAMESPACE) --url 

automation:
	./run-automation-tests.sh $(URL)

# Path: Makefile
