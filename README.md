# Development

Add Google API Client ID and secret to user secrets.

> ❗ You need to be in the project folder (same level as .csproj file)

```bash
# Make sure the user secrets store is initialized
dotnet user-secrets init

# Set the secrets. Replace <ClientID> and <ClientSecret> with the values
dotnet user-secrets set "Authentication:Google:ClientId" "<ClientID>"
dotnet user-secrets set "Authentication:Google:ClientSecret" "<ClientSecret>"
```
