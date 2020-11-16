# FantasyFootballDashboard
This application will give the ability to see all of your fantasy football players in one place! Currently, we are planning on targeting three different services, but we may add others in the future:
* CBS Fantasy Football
* MyFantasyLeague
* ESPN Fantasy Football

The goal is to hook-up the API in this repo to a front-end project so that you can see how all of your players are doing on gameday.

## Dashboard API Requests
As of November 9, 2020, you can run a GET request against the API project running locally to retrieve a list of players from the listed services. Use the URL

```https://localhost:{portNumber}/api/player/GetPlayers```

with the request body 
```
{
    "CbsUserName": "{{CBS_Username}}",
    "CbsLeagueName": "{{CBS_League_Name}}",
    "EspnLeagueId": "{{ESPN_League_ID}}",
    "EspnTeamId": "{{ESPN_Team_ID}}",
    "MflUsername": "{{MFL_Username}}",
    "MflPassword": "{{MFL_Password}}"
} 
```

You do not need to populate every field, but if you do not populate a field, you will not be able to access the service.

## Test Harness Console Application
While we are working on developing the REST API, we've created a .NET Core console application so you can see how the connections work. As of November 8, 2020, you can use this console app to list out your players from CBS and My Fantasy League, as well as players from public leagues in ESPN. 

## Accessing Players from ESPN Leagues
Currently, this application only supports public leagues. In order access the players, you will need to pull your league ID and team ID from the ESPN URL for your team's home page. The ESPN API does not allow for a programatic lookup of these values at this time.

## Integration Tests
Integration tests are included in the solution, however they are marked as ignored until we can configure a secure pipeline for the test variables. You can run them locally by removing the Ignore attribute in the test and updating the test JSON file or configuring your user secrets (see this link for how to configure user secrets in .NET Core https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows)

## Running the API Locally
The API leverages Azure Active Directory (Azure AD) for authentication. Even when running locally, you will need to secure an access token via the OAuth 2.0 protocol. You will need to create your own app registration in Azure AD. The authorization tab in the example player get request in the Postman collection demonstrates how to get a token once the app registration in Azure AD is setup. See this documentation for more details on using Azure AD for authentication (https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-aspnet-core-web-api).
