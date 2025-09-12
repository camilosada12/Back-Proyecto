# Etapa 1: Construir la aplicación Angular
FROM node:18 as build
WORKDIR /usr/src/app
COPY package*.json ./
RUN npm install --legacy-peer-deps
COPY . .
# El nombre del proyecto 'front' se toma de angular.json (projects.front.architect.build.options.outputPath)
RUN npm run build -- --configuration production

# Etapa 2: Servir la aplicación desde Nginx
FROM nginx:alpine
# Copia los artefactos de construcción a la carpeta web del servidor Nginx
COPY --from=build /usr/src/app/dist/front/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
# Expone el puerto 80 para el tráfico entrante
EXPOSE 80
