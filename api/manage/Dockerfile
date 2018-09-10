FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY manage/manage.csproj manage/
COPY Dal/Dal.csproj Dal/
COPY Model/Model.csproj Model/
COPY Common/Common.csproj Common/
RUN dotnet restore manage/manage.csproj
COPY . .
WORKDIR /src/manage
RUN dotnet build manage.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish manage.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "manage.dll"]
