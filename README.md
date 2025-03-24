# Development

Add Google API Client ID and secret to user secrets.

> ❗ You need to be in the project folder (same level as .csproj file)

```bash
# Make sure the user secrets store is initialized
dotnet user-secrets init

# Set the secrets. Replace <ClientID> and <ClientSecret> with the values
### Google
dotnet user-secrets set "Authentication:Google:ClientId" "<ClientID>"
dotnet user-secrets set "Authentication:Google:ClientSecret" "<ClientSecret>"

### Facebook
dotnet user-secrets set "Authentication:Facebook:AppId" "<app-id>"
dotnet user-secrets set "Authentication:Facebook:AppSecret" "<app-secret>"

### Twitter
dotnet user-secrets set "Authentication:Twitter:ConsumerAPIKey" "<consumer-api-key>"
dotnet user-secrets set "Authentication:Twitter:ConsumerSecret" "<consumer-secret>"
```
