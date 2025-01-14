#!/bin/bash
SECONDS=0
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

printf "Building project...\n"
dotnet build "$SCRIPT_DIR"
printf "\n\n================\n\n"

printf "Generating document contexts...\n"
t4 "$SCRIPT_DIR/Templates/PlanningCenterContextTemplate.tt" 2> /dev/null

printf "Generating resource classes...\n"
t4 "$SCRIPT_DIR/Templates/PlanningCenterResourceTemplate.tt" 2> /dev/null

printf "Generating API client classes...\n"
t4 "$SCRIPT_DIR/Templates/PlanningCenterClientTemplate.tt" 2> /dev/null

printf "\nCompleted in $SECONDS seconds.\n"
