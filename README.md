# SpotifyShuffler
C# WebApp to shuffle spotify playlist. 
<br>
**SpotifyShuffler** offers very simple and... Not ideal UI, but it's an old project
<Br><br>
It's archived right now.

## Functionality 
* Login using Spotify - OAuth2
* You can shuffle your playlists, or someone else.
* You can change the name and description of the playlist before app will shuffle.

## Installation
```shell
git clone https://www.github.com/kacperfaber/SpotifyShuffler && cd SpotifyShuffler
```

Run setup
```shell
dotnet restore
dotnet build
```

## Configuration
You need to specify your ClientId and ClientSecret before app will start.
<br>
```json5
// configuration.Development.json

{
  "Authentication":{
    "Spotify":{
      "ClientId":"", // Your ClientId...
      "ClientSecret": "" // Your ClientSecret...
    }
  }
  
  // ...
}
```

## Run locally
Setup database.
```shell
# setup database
dotnet ef database update
```
Run on localhost
```shell
cd SpotifyShuffler # web app project
dotnet run
```

> Application will forbid you to do anything with playlists, unless you have no email given.
> <br>
> Go to settings, and input your email. Application accepts email address without verification.

## Author
Kacper Faber