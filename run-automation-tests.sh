#!/bin/bash
#!/bin/bash

# set variables for test
export ServiceUrl=$1
dotnet test dotnetservice/WebApi.AutomationTests/WebApi.AutomationTests.csproj

