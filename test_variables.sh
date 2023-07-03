#!/bin/bash

# Lee el archivo YAML y obtén el valor de las variables
APP_NAME=$(grep -oP "(?<=APP_NAME=).*" docker-compose.yaml)
APP_PORT=$(grep -oP "(?<=APP_PORT=).*" docker-compose.yaml)

# Comprueba si las variables están en blanco o no
if [ -z "$APP_NAME" ]; then
    echo "La variable APP_NAME está en blanco"
else
    echo "La variable APP_NAME es: $APP_NAME"
fi

if [ -z "$APP_PORT" ]; then
    echo "La variable APP_PORT está en blanco"
else
    echo "La variable APP_PORT es: $APP_PORT"
fi
