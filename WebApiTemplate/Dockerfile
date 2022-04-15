#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#FROM ubuntu:focal
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# editor
RUN apt-get update -y && apt-get install -y nano
# for memory extend if docker no much Memory
RUN apt-get install -y dphys-swapfile
# for monitoring when ssh to docker 
RUN apt-get install -y curl
RUN apt-get install -y htop
# for drawing and Bitmap
RUN apt-get update -y && apt-get install -y libgdiplus && rm -rf /var/lib/apt/lists/* && ln -s /lib/x86_64-linux-gnu/libdl.so.2 /lib/x86_64-linux-gnu/libdl.so && ln -s /usr/lib/libgdiplus.so /lib/x86_64-linux-gnu/libgdiplus.so

WORKDIR /app
#check appsettiing.json key: Kestrel
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
#COPY ["local relative path with Dockerfile", "inside docker under Workdir: src folder"]
COPY ["/WebApiTemplate/*", "WebApiTemplate/"]

RUN dotnet restore "./WebApiTemplate/WebApiTemplate.csproj"

WORKDIR "/src/."
RUN dotnet build "WebApiTemplate/WebApiTemplate.csproj" -c Release -o /app/build
#RUN dotnet build "MilionsFaceIdsApi/MilionsFaceIdsApi.csproj" -c Release -o /app/build
#
FROM build AS publish
RUN dotnet publish "WebApiTemplate/WebApiTemplate.csproj" -c Release -o /app/publish
#
FROM base AS final
WORKDIR /app
#COPY ["/dlls/*", "."]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiTemplate.dll"]

#docker build -f "D:/devdotnetenviroment/WebApiTemplate/Dockerfile" --force-rm -t webapiteamplate "D:/devdotnetenviroment/WebApiTemplate/" 

#https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-6.0
#dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p 123456
## -p `you pc port` : `inside docker port`
#docker run --rm -it -p 9080:80 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=443 -e ASPNETCORE_Kestrel__Certificates__Default__Password="123456" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v %USERPROFILE%\.aspnet\https:/https/ --mount type=bind,source=D:/devdotnetenviroment/WebApiTemplate/WebApiTemplate/appsettings.json,target=/app/appsettings.json webapiteamplate 
#docker run --rm -it -p 9443:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=443 -e ASPNETCORE_Kestrel__Certificates__Default__Password="123456" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v %USERPROFILE%\.aspnet\https:/https/ --mount type=bind,source=D:/devdotnetenviroment/WebApiTemplate/WebApiTemplate/appsettings.json,target=/app/appsettings.json webapiteamplate 


