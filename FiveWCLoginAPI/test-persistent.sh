#!/bin/bash
# Test persistent database using docker
# shell script to run docker with volume using postgres.
# shellcheck disable=SC2215
docker run -d -p 5432:5432 --name 5wc-test-persistent -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=five_wc_api -v persistent-test:/var/lib/postgresql/data postgres