FROM node:current as build
WORKDIR /tmp

COPY . .

RUN npm install && npm run build

FROM nginx:stable-alpine as production

COPY --from=build /tmp/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /tmp/default.conf /etc/nginx/conf.d
COPY --from=build /tmp/dist/. /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
