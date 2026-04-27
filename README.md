# os-query-api

A .NET 10 Web API for querying information about the host operating system. Protected with JWT Bearer authentication via Keycloak.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A running [Keycloak](https://www.keycloak.org/) instance

---

## Keycloak Setup

### 1. Create a realm

1. Log in to the Keycloak admin console
2. Create a new realm named `os-query-api`

### 2. Create a client

1. Go to **Clients** → **Create client**
2. Set **Client ID** to `apiclient`
3. Set **Client authentication** to `On` (confidential client)
4. Enable the grant types you need:
   - **Direct access grants** — for testing with username/password (ROPC flow)
   - **Service accounts roles** — for machine-to-machine (client credentials flow)
5. Save and copy the **Client secret** from the **Credentials** tab

### 3. Add the audience mapper

This makes Keycloak include `apiclient` in the token's `aud` claim, which the API validates.

1. Go to **Clients** → `apiclient` → **Client scopes** → click `apiclient-dedicated`
2. **Add mapper** → **By configuration** → **Audience**
3. Set **Included Client Audience** to `apiclient`
4. Save

### 4. Add the roles mapper

This puts client roles into a root-level `roles` claim in the token, which ASP.NET Core reads.

1. In `apiclient-dedicated` → **Add mapper** → **By configuration** → **User Client Role**
2. Set **Token Claim Name** to `roles`
3. Set **Claim JSON type** to `String`
4. Save

### 5. Create the admin role

1. Go to **Clients** → `apiclient` → **Roles** → **Create role**
2. Name it `admin` and save

### 6. Create a user and assign the role

1. Go to **Users** → **Create new user**, fill in the details, save
2. Go to the user's **Credentials** tab and set a password
3. Go to the user's **Role mapping** tab → **Assign role** → filter by `apiclient` → assign `admin`

---

## API Configuration

The JWT Bearer options are configured in `Program.cs`. Update the `Authority` and `Audience` to match your Keycloak setup:

```csharp
options.Authority = "http://<keycloak-host>/realms/os-query-api";
options.Audience = "apiclient";
options.RequireHttpsMetadata = false; // set to true in production with HTTPS
options.MapInboundClaims = false;
options.TokenValidationParameters.RoleClaimType = "roles";
```

> `MapInboundClaims = false` is required to prevent the JWT handler from remapping the `roles` claim to a .NET-specific claim type, which would break role-based authorization.

---

## Running the API

### Locally

```bash
dotnet run --project os-query-api
```

The API will be available at `http://localhost:5106` by default.

### Docker

```bash
docker build -t os-query-api .
docker run -p 8080:8080 os-query-api
```

---

## Endpoints

| Method | Route | Auth required | Role required |
|--------|-------|:---:|:---:|
| GET | `/api/os/v1/operating-system-information/version` | Yes | - |
| GET | `/api/os/v1/operating-system-information/ping` | Yes | - |
| GET | `/api/os/v1/shell/run/{command}` | Yes | `admin` |

---

## Testing with the HTTP client

The `os-query-api.http` file (JetBrains Rider / VS Code REST Client) is set up to fetch a token automatically and inject it into subsequent requests.

1. Fill in the variables at the top of the file:
   ```
   @keycloak_url = http://<keycloak-host>
   @keycloak_client_id = apiclient
   @keycloak_client_secret = <your-client-secret>
   ```
2. Run the `### Get token` request — this stores the access token in `auth_token`
3. Run any of the other requests — they use `{{auth_token}}` automatically

> Do not commit `os-query-api.http` with a real client secret. Add it to `.gitignore` or replace the secret value with a placeholder before committing.

The file includes two token grant options (comment/uncomment as needed):

```
# Client credentials (service account, no user)
grant_type=client_credentials&client_id={{keycloak_client_id}}&client_secret={{keycloak_client_secret}}

# Password grant (specific user, requires Direct access grants enabled)
grant_type=password&client_id={{keycloak_client_id}}&client_secret={{keycloak_client_secret}}&username=<user>&password=<pass>
```
