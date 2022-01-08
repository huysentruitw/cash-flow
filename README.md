# CashFlow

Application for managing cash flows written in ASP.NET Core 6 and Angular 13 (EF Core, Apollo, GraphQL, CQS).

The application currently has the following features:
* Add bank/cash accounts
* Add suppliers
* Add codes (can be linked while booking transactions)
* Add a financial year
* Book transactions on the selected financial year

Future features:
* All kind of different reports (probably using jsReport)

## Development

### Prerequisites

#### Install these extensions in Visual Studio

* Make sure you have _Text Template Transformation_ checked in Visual Studio Installer, tab _Individual components_ > _Code tools_ 

#### Install and start jsreport

For windows: see https://jsreport.net/learn/windows
Other: see https://jsreport.net/on-prem

### Start debugging

* Set `CashFlow.Host` as StartUp project.
* Hit F5 (Start Debugging)

### Adding new projects to the solution

#### xUnit Test Project

* Right-click on the correct solution folder
* Add > New project...
* Pick _xUnit Test Project (.NET Core)_ from _Test_
* Enter a name in the form _CashFlow.Something.Tests_
* Append _\tests_ to the location
* Click OK
* Change the target framework to net6.0 in the csproj file

#### Class Library

* Right-click on the correct solution folder
* Add > New project...
* Pick _Class Library (.NET Standard)_ from _.NET Standard_
* Enter a name in the form _CashFlow.Something_
* Append _\src_ to the location
* Click OK
* Change the target framework to net6.0 in the csproj file

### Test GraphQL endpoint

* Install GraphiQL tool from https://electronjs.org/apps/graphiql
* Set the GraphQL endpoint to https://localhost:5001/api/graph (application url)
* Start writing queries

### Text Template Transformation (T4)

Some projects contain .tt files (T4 templates) for generating code on-the-fly.

They're used to generate GraphQL mutation and related command/result models and code from definition files (.def) located in CashFlow.Command.Abstractions.

Whenever you need to rebuild the templates, hit Build > Transform All T4 Templates
