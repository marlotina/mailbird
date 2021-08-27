
FROM node:latest as build
WORKDIR /app
COPY ./ /app/
RUN npm install
RUN npm run build --prod


FROM nginx:latest
COPY --from=build /app/dist/reading-emails-app /usr/share/nginx/html

COPY ./nginx.conf /etc/nginx/nginx.conf
COPY ./default.conf /etc/nginx/conf.d/default.conf


RUN touch /var/run/nginx.pid && \
  chown -R www-data:www-data /var/run/nginx.pid && \
  chown -R www-data:www-data /var/cache/nginx

USER www-data


EXPOSE 80
