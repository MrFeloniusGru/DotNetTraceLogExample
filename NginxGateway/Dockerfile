FROM nginx:stable-alpine as production
COPY ./default.conf /etc/nginx/conf.d/default.conf
COPY ./nginx.conf /etc/nginx/nginx.conf
RUN mkdir -p /etc/nginx/logs && cat > /etc/nginx/logs/error.log

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
