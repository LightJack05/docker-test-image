FROM ubuntu:rolling as build

WORKDIR /source
RUN apt update && apt install dotnet-sdk-9.0 aspnetcore-runtime-9.0 -y 

COPY ./*.csproj .
RUN dotnet restore

COPY . . 
RUN dotnet publish -o /app

FROM ubuntu:rolling
ENV ASPNETCORE_HTTP_PORTS=5000
RUN apt update && apt install dotnet-runtime-9.0 aspnetcore-runtime-9.0 curl iputils-ping -y
EXPOSE 5000
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["./docker-test"]
