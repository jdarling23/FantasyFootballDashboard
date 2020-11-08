# FantasyFootballDashboard
This application will give the ability to see all of your fantasy football players in one place! Currently, we are planning on targeting three different services, but we may add others in the future:
* CBS Fantasy Football
* MyFantasyLeague
* ESPN Fantasy Football

The goal is to hook-up the API in this repo to a front-end project so that you can see how all of your players are doing on gameday.

## Test Harness Console Application
While we are working on developing the REST API, we've created a .NET Core console application so you can see how the connections work. As of November 8, 2020, you can use this console app to list out your players from CBS and My Fantasy League, as well as players from public leagues in ESPN. 

## Accessing Players from ESPN Leagues
Currently, this application only supports public leagues. In order access the players, you will need to pull your league ID and team ID from the ESPN URL for your team's home page. The ESPN API does not allow for a programatic lookup of these values at this time.  
