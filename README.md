# Chatty App

## General info
An easy-to-use real-time chat application. Create a chat room and manage messages, nicknames and users.


## Technologies
-  Backend
    - .Net 6.0 WebAPI
    - EntityFrameworkCore
    - AspNetCore.Identity
    - SignalR
    - MediatR
    - SQLite*

- Frontend
    - React
    - TypeScript
    - ChakraUI
    - MobX
    - SignalR
    - Axios
    
\* For development purposes, application uses SQLite by default, but can also be configured to any modern DBMS that supports EntityFrameworkCore.
    
## Usage

### Backend
In _API_ directory run following commands:
```console
> dotnet ef database update
```
to create SQLite database

```console
> dotnet run
```
to run application at _https://localhost:7152_ and _http://localhost:5063_

### Frontend
In _client-app_ directory run following commands:
```console
> npm install
```
to install dependences 
```console
> npm start
```
to run application at _http://localhost:3000/_