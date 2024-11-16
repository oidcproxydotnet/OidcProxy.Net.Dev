# The IdentityServer4 Test Server

This server is an ASP.NET server that always signs in "John Doe".

To build the container go to the folder that contains `IdentityServer4.sln` and type:

```bash
docker build -f OidcServer.Web/Dockerfile -t identityserver4 .
```

To run it:

```bash
docker run -d 
  -e ASPNETCORE_URLS="https://0.0.0.0:8081" 
  -e "Kestrel__Certificates__Default__Path=https_certificate.pfx" 
  -e "Kestrel__Certificates__Default__Password=hallo" 
  -e "PFX_BASE64=MIIKPwIBA....HgICCAA=" 
  -p 8081:8081 
  --name identityserver4 identityserver4 
```

Or get it from Dockerhub:

```bash
docker pull albertstarreveld/openiddictserver:2.0

docker run -d 
  -e ASPNETCORE_URLS="https://0.0.0.0:8081" 
  -e "Kestrel__Certificates__Default__Path=https_certificate.pfx" 
  -e "Kestrel__Certificates__Default__Password=hallo" 
  -e "PFX_BASE64=MIIKPwIBA....HgICCAA=" 
  -p 8081:8081 
  --name identityserver4 albertstarreveld/identityserver4:2.0
```