# Golang + Dotnet microservices hosted on Kubernetes
## Prerequisites
Scripts are written for Unix-like systems (Linux, macOS, etc.)

### Tools
- [Docker](https://www.docker.com/)
- [Kubernetes](https://kubernetes.io/)
- [Minikube](https://kubernetes.io/docs/tasks/tools/install-minikube/) or other Kubernetes cluster (some Makefile commands are specific to Minikube)
- [Helm](https://helm.sh/)
### Languages
- [Golang](https://golang.org/)
- [.NET 8](https://dotnet.microsoft.com/)

## Basics of the project
### Architecture
Project consists of 2 microservices:
- **Golang** service that provides REST API that allows us to get random number   


```json
//GET 
//URL /random

// Request body
{
    "min": 0,
    "max": 100,
    "seed": 1 // optional
}
```
```json
// Response body
{
    "number": 42
}
```
- **Dotnet** service that provides REST API that creates a connection to the Golang service, gets a random number saves it to **PostgreSQL** database and returns the configured number of previous records from the database along with the current one.
Request type: GET

```json
//GET
//URL /RandomNumber
```

```json
// Response body
{
  "Number": 77,
  "Records": [
    {
      "Id": "551f20db-d5a5-4825-bbfd-c5f7623734a5",
      "Number": 46,
      "CreatedAt": "2023-12-29T18:56:03.033545Z"
    },
    {
      "Id": "544f2584-8ef0-4cd9-9ec4-b4aa87a9432d",
      "Number": 29,
      "CreatedAt": "2023-12-29T18:58:28.888558Z"
    }
  ]
}
```

## CI/CD 
all CI/CD workflows are defined in the .github/workflows directory

### Integration tests workflow
Integration test workflow is triggered by push to the main branch.
It builds .NET service and runs the integration tests against the Golang service and PostgreSQL database.

## Usage
### Build and push Docker images to Docker Hub repository <span style="color:grey">*corey773/*</span>
```bash
Make composeup
Make composepush
```

### Stop the Docker Compose stack
```bash
Make composedown
```

### Deploy the Helm chart to Kubernetes cluster
```bash
# default values are used
Make helminstall 
# or specify custom values for the Helm chart
Make helminstall RELEASE_NAME = <release_name> NAMESPACE = <namespace> VALUES_FILE = <values_file>
```

### Delete the Helm release
```bash
# default
Make helmdelete
Meke helmdelete RELEASE_NAME = <release_name> NAMESPACE = <namespace>
```

### Rollback the Helm release
```bash
# default
Make helmrollback
Meke helmrollback RELEASE_NAME = <release_name> NAMESPACE = <namespace> REVISION = <revision>
```

### Upgrade the Helm release
```bash
# default
Make helmupgrade
Meke helmupgrade RELEASE_NAME = <release_name> NAMESPACE = <namespace> VALUES_FILE = <values_file>
```

### Open minikube dashboard
```bash
Make mkdashboard
```

### Open minikube service
```bash
Make mkurl
```

### Run automated tests
To run automated tests you need to specify the URL of the service in the Makefile command or set it in the auomationSettings.json in the root directory of the test project.

```bash
# url of the service is required
Make automation URL = <service-url>
# you can also specify the service url in the automationSettings.json file and run 
Make automation
```


