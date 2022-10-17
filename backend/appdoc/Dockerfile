# Get the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the csproj and restore all of the nugets
COPY ./BL/*.csproj ./BL/
COPY ./DB/*.csproj ./DB/
COPY ./UI/API/*.csproj ./UI/API/
RUN dotnet restore ./UI/API/API.csproj

# # Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out ./UI/API/API.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "API.dll"]
CMD ["--urls", "http://*:5001"]