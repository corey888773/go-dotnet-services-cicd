RELEASE_NAME=elympics-v0
NAMESPACE=elympics
VALUES_FILE=./helm-chart/development.yaml
REVISION=1

.PHONY:
	helminstall helmdel helmupgrade helmrollback

helminstall:
	helm install $(RELEASE_NAME) ./helm-chart/ --namespace $(NAMESPACE) --create-namespace --values $(VALUES_FILE) 

helmdelete:
	helm uninstall $(RELEASE_NAME) --namespace $(NAMESPACE)

helmupgrade:
	helm upgrade $(RELEASE_NAME) ./helm-chart/ --namespace $(NAMESPACE) --values $(VALUES_FILE)

helmrollback:
	helm rollback $(RELEASE_NAME) $(REVISION) --namespace $(NAMESPACE)

# Path: Makefile
