# # Set SDK and ASPNET version from build args.
# # --build-arg NETCORE_VERSION=5.0
# # --build-arg NETCORE_VERSION=3.1
#
ARG NETCORE_VERSION
FROM mcr.microsoft.com/dotnet/sdk:${NETCORE_VERSION} AS build
ARG APP_NAME
ENV APP_NAME=$APP_NAME

WORKDIR /app
# Exponemos el puerto 80 
EXPOSE 80
# EXPOSE ${APP_PORT}
# ENV ASPNETCORE_URLS=http://+:${APP_PORT}
# Copiar csproj y restauramos nuestra app
COPY ./${APP_NAME}/*.csproj ./
RUN dotnet restore

# Copiamos todos los archivos y compilamos o contruimos nuestra app
COPY . .
RUN dotnet publish -c Release -o publish

# Construimos o instanciamos nuestro contenedor
FROM mcr.microsoft.com/dotnet/aspnet:${NETCORE_VERSION}
ARG APP_NAME
ENV APP_NAME=$APP_NAME
WORKDIR /app

COPY --from=build /app/publish ./
#Indicamos el archivo dll compilado (nombre del proyecto)
ENTRYPOINT dotnet ${APP_NAME}.dll
