#!/bin/bash

# Usage: ./generate_module <name> [customRoute]

if [ -z "$1" ]; then
  echo "Usage: ./generate_module <name> [customRoute]"
  exit 1
fi

NAME=$1
ROUTE=$2

# If no custom route provided, apply pluralization rules
if [ -z "$ROUTE" ]; then
  if [[ "$NAME" =~ (s|x|z|ch|sh)$ ]]; then
    ROUTE="${NAME}es"                  # e.g. bus -> buses, box -> boxes
  elif [[ "$NAME" =~ y$ ]]; then
    STEM="${NAME%y}"
    ROUTE="${STEM}ies"                 # e.g. category -> categories
  else
    ROUTE="${NAME}s"                   # default: just add "s"
  fi
fi

# Capitalized module name (Book -> BookModule)
CAPITALIZED="$(tr '[:lower:]' '[:upper:]' <<< ${NAME:0:1})${NAME:1}"

echo "Generating module '$NAME' with route '$ROUTE'..."

ng generate module "$NAME" --module app --routing --route "$ROUTE"

# Path to generated module file
MODULE_FILE="src/app/$NAME/$NAME.module.ts"

if [ -f "$MODULE_FILE" ]; then
  echo "Patching $MODULE_FILE to include SharedModule..."

  if ! grep -q "SharedModule" "$MODULE_FILE"; then
    sed -i "" "1a\\
import { SharedModule } from '../shared/shared.module';
" "$MODULE_FILE"
    sed -i "" "s/imports: \[/imports: \[SharedModule, /" "$MODULE_FILE"
  fi
else
  echo "Error: Could not find $MODULE_FILE"
fi

# Path to app routing module
APP_ROUTING_FILE="src/app/app-routing.module.ts"

if [ -f "$APP_ROUTING_FILE" ]; then
  echo "Adding lazy route to $APP_ROUTING_FILE..."

  ROUTE_DEF="  { path: '$ROUTE', loadChildren: () => import('./$NAME/$NAME.module').then(m => m.${CAPITALIZED}Module) },"

  if ! grep -q "$NAME.module" "$APP_ROUTING_FILE"; then
    sed -i "" "/const routes: Routes = \[/a\\
$ROUTE_DEF
" "$APP_ROUTING_FILE"
  else
    echo "Route for '$ROUTE' already exists in app-routing.module.ts"
  fi
else
  echo "Error: Could not find $APP_ROUTING_FILE"
fi

# Path to route provider
ROUTE_PROVIDER_FILE="src/app/route.provider.ts"

if [ -f "$ROUTE_PROVIDER_FILE" ]; then
  echo "Adding menu route to $ROUTE_PROVIDER_FILE..."

  BLOCK="      {
        path: '/$ROUTE',
        name: '::Menu:${CAPITALIZED}s',
        iconClass: 'fas fa-person-running',
        layout: eLayoutType.application,
        requiredPolicy: 'Syper.${CAPITALIZED}s',
      },"

  # Only add if not already present
  if ! grep -q "path: '/$ROUTE'" "$ROUTE_PROVIDER_FILE"; then
    # Insert before the first occurrence of "]);"
    sed -i "" "/]);/ i\\
$BLOCK
" "$ROUTE_PROVIDER_FILE"
  else
    echo "Route for '$ROUTE' already exists in route.provider.ts"
  fi
else
  echo "Error: Could not find $ROUTE_PROVIDER_FILE"
fi

abp generate-proxy -t ng
