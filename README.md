## Dependecies Manager

Dependecies Manager is a solution that implements 2 methods:

-  `RegisterDependencies(string component, string[] dependentComponents)` -> `return StatusCode`

-  `GetDependencies(string component)` -> `return string[]`

These are valid HTTP requests used to test the endpoints:

POST RegisterDependencies

```
POST /dependeciesmanager HTTP/1.1
Host: localhost:44314
Content-Type: application/json
Cache-Control: no-cache
Postman-Token: 370a9566-8617-70ca-7627-c264307be9b1

{
	"item":"C",
	"deps":["Z", "X", "Y"]
}
```

GET GetDependencies

```
GET /dependeciesmanager?item=C HTTP/1.1
Host: localhost:44314
Content-Type: application/json
Cache-Control: no-cache
Postman-Token: 55f61d85-b550-d024-2b6a-99ef5da60c64
```

In addition to the solution a test solution has been added to cover all the possible code paths in the service; testing all possible scenarios.

For this type of problem instead of a database a Dictionary has been used. The class responsible for this can be found in `DependeciesManager/Models/SingletonDB`.








