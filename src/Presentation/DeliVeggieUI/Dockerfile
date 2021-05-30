FROM node:10-alpine as build-step
RUN mkdir -p /app
WORKDIR /app
COPY ["src/Presentation/DeliVeggieUI/package.json", "/app"]
RUN npm install
COPY ["src/Presentation/DeliVeggieUI/", "/app"]
RUN npm run build --prod

FROM nginx:1.17.1-alpine
COPY --from=build-step /app/dist /usr/share/nginx/html
