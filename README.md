# CineTix - movie ticket booking (ASP.NET Core Razor Pages + EF Core/SQLite, no JavaScript)

Requires the .NET 8 SDK. If you only have a newer SDK, change `<TargetFramework>` in
MovieBooking.csproj to match (e.g. net9.0).

    dotnet run

The first run downloads the SQLite/EF Core package (needs internet once), creates `cinetix.db`
next to the project and seeds sample data. Open the URL printed in the console.

Backend
- Database: SQLite via EF Core (Data/AppDbContext.cs). Data survives restarts.
- Service layer: Services/DataStore.cs, used by both the Razor pages and the JSON API.
- Double booking is prevented by a unique (ShowId, seat) index.
- To reset everything: stop the app and delete cinetix.db (plus cinetix.db-shm / -wal if present).

JSON API (same data as the website)
    GET  /api/movies?q=&lang=
    GET  /api/movies/{id}
    GET  /api/movies/{id}/shows
    GET  /api/theatres
    GET  /api/shows/{id}/seats
    POST /api/bookings            {"showId":1,"name":"Asha","email":"asha@example.com","seats":["C5","C6"]}
    GET  /api/bookings/{id}
    GET  /api/bookings?email=asha@example.com

Notes
- /admin has no login. Add authentication before exposing it anywhere.
- Posters: paste an image URL in Admin > Edit movie, otherwise a text poster is shown.
