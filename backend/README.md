# Logitar.Demo

## PostgreSQL

Execute the following command to create a PostgreSQL Docker image.

`docker run --name demo-postgres -e POSTGRES_PASSWORD=YJsW4QzpUwnPEZFA -p 5435:5432 -d postgres`

## User Secrets

Copy the contents of the `secrets.example.json` in your user secrets (right-click the `Logitar.Demo.Web` project, then click `Manage User Secrets`).

Replace the following variables:

- `<POSTGRES_PASSWORD>`: the password you assigned to your PostgreSQL Docker image.
- `<LOCAL_IP_ADDRESS>`: your local IP address (ex.: `192.168.X.Y`).
