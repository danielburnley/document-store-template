#!/bin/bash

set -e

if [ -z "$1" ]; then
  echo 'Provide an environment to deploy'
  exit 1
fi
ENVIRONMENT_NAME="$1"

PRODUCTION_NAME="production"
if [[ "${ENVIRONMENT_NAME}" == "${PRODUCTION_NAME}" ]]; then
  APP_NAME="${ENVIRONMENT_NAME}-dark"
else
  APP_NAME="${ENVIRONMENT_NAME}"
fi

curl -L "https://packages.cloudfoundry.org/stable?release=linux64-binary&source=github" | tar -zx
./cf api https://api.cloud.service.gov.uk
./cf auth "${CF_USER}" "${CF_PASSWORD}"

./cf target -o "${CF_ORG}" -s "${ENVIRONMENT_NAME}"

./cf push -f "deploy-manifests/${APP_NAME}.yml" \
  --var "HmacSecret=${HMAC_SECRET}" \
