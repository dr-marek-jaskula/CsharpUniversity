FROM node:alpine AS myBuild
WORKDIR /app
COPY index.ts .
RUN npm install -g typescript && tsc index.ts

FROM nginx:alpine

WORKDIR /usr/share/nginx/html
COPY index.html .
COPY --from=myBuild /app/index.js .