\# Library Management API



A layered ASP.NET Core Web API for a small library: members borrow and return books, admins manage the catalog. Built with EF Core and PostgreSQL, secured with JWT and role-based authorization.



\## Tech Stack



\- \*\*C# / .NET 10\*\*, ASP.NET Core Web API

\- \*\*EF Core\*\* (Code First, migrations) with \*\*PostgreSQL\*\*

\- \*\*JWT\*\* authentication, \*\*BCrypt\*\* password hashing

\- \*\*FluentValidation\*\*, \*\*AutoMapper\*\*

\- \*\*Scalar\*\* (API reference), \*\*Postman\*\* collection



\## Architecture



```

Library.API         → Controllers, DI setup, auth, CORS

Library.Business    → Services (business rules), DTOs, validators, mapping

Library.DataAccess  → DbContext, repositories, migrations

Library.Entities    → Book, User, Loan

```



Dependencies point downwards only: `API → Business → DataAccess`, and all layers reference `Entities`. Controllers only work with DTOs; entities never leave the Business layer.



\## Features



| Area | Endpoint | Access |

|---|---|---|

| Auth | `POST /api/auth/register` | Public |

| Auth | `POST /api/auth/login` | Public |

| Books | `GET /api/books`, `GET /api/books/{id}` | Public |

| Books | `POST`, `PUT`, `DELETE /api/books/{id}` | Admin |

| Loans | `POST /api/loans` (borrow) | Member |

| Loans | `POST /api/loans/{id}/return` | Member (own loans only) |



\## Business Rules



Each rule is enforced in the service layer (for a clear error response) \*\*and\*\* in the database (so it cannot be bypassed).



| Rule | Service layer | Database |

|---|---|---|

| A book with loan history cannot be deleted | `409 Conflict` | `ON DELETE RESTRICT` |

| An email can only be registered once | `409 Conflict` | Unique index on `Email` |

| A book cannot be on loan to two members at once | `409 Conflict` | Partial unique index on `BookId WHERE ReturnDate IS NULL` |



Other rules:

\- A member can only return their own loans (`403`), and a returned loan cannot be returned again (`409`).

\- The borrowing user is always read from the JWT, never from the request body.

\- New users always register as `Member`; the role is never accepted from the client.



\## Design Decisions



\- \*\*Book status is computed, not stored.\*\* A book is `OnLoan` if it has a loan with no `ReturnDate`, otherwise `Available`. Keeping a separate status column would mean updating two places on every borrow/return.

\- \*\*Loan history is kept.\*\* Returning a book sets `ReturnDate`; nothing is deleted.

\- \*\*Login errors don't reveal which part was wrong.\*\* Unknown email and wrong password both return `401 Invalid email or password`.

\- \*\*Separate request/response DTOs\*\* (`CreateBookDto`, `UpdateBookDto`, `BookDto`) so each can change independently.



\## Getting Started



\*\*Requirements:\*\* .NET 10 SDK, PostgreSQL



1\. Set secrets (never stored in `appsettings.json`):

&#x20;  ```bash

&#x20;  dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=LibraryDb;Username=postgres;Password=YOUR\_PASSWORD" --project Library.API

&#x20;  dotnet user-secrets set "Jwt:Key" "A\_RANDOM\_KEY\_OF\_AT\_LEAST\_32\_BYTES" --project Library.API

&#x20;  ```

2\. Create the database:

&#x20;  ```bash

&#x20;  dotnet ef database update --project Library.DataAccess --startup-project Library.API

&#x20;  ```

3\. Run:

&#x20;  ```bash

&#x20;  dotnet run --project Library.API

&#x20;  ```

4\. Open the API reference at `https://localhost:7259/scalar` (development only).



\*\*First admin:\*\* Registration always creates a `Member`. Promote a user to admin directly in the database:

```sql

UPDATE "Users" SET "Role" = 'Admin' WHERE "Email" = 'admin@example.com';

```



\## Postman



Import `postman/LibraryManagementApi.postman\_collection.json`. Run \*\*Login - Admin\*\* and \*\*Login - Member\*\* first; their scripts store the tokens in collection variables, and the `Books` and `Loans` folders use them automatically. Each folder contains the happy path and the error cases (400, 401, 403, 404, 409).



\## Known Limitations



\- `GET /api/books` runs one extra query per book to compute its status (N+1).

\- Tokens can't be revoked before they expire; a role change takes effect on the next login.

\- Data access is synchronous; async/await is planned for a future iteration.

