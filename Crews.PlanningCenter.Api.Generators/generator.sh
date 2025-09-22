#!/bin/bash
SECONDS=0
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

HTTP_EXTENSIONS_VERSION="2.0.0"
HTTP_EXTENSIONS_FRAMEWORK="net8.0"

PRIMITIVES_EXTENSIONS_VERSION="1.1.2"
PRIMITIVES_EXTENSIONS_FRAMEWORK="net8.0"

PC_MODELS_VERSION="1.2.0"
PC_MODELS_FRAMEWORK="net8.0"

HUMANIZER_VERSION="2.14.1"
HUMANIZER_FRAMEWORK="net6.0"

BASE_PROJECT_FRAMEWORK="net8.0"
GENERATOR_FRAMEWORK="net9.0"

printf "Building project...\n"
dotnet build "$SCRIPT_DIR"
printf "\n\n================\n\n"

printf "You can safely ignore errors that begin with 'Could not write output file'.\n\n"

printf "Generating document contexts...\n"
t4 \
	-r "$HOME/.nuget/packages/crews.extensions.http/$HTTP_EXTENSIONS_VERSION/lib/$HTTP_EXTENSIONS_FRAMEWORK/Crews.Extensions.Http.dll" \
	-r "$HOME/.nuget/packages/crews.extensions.primitives/$PRIMITIVES_EXTENSIONS_VERSION/lib/$PRIMITIVES_EXTENSIONS_FRAMEWORK/Crews.Extensions.Primitives.dll" \
	-r "$HOME/.nuget/packages/crews.planningcenter.models/$PC_MODELS_VERSION/lib/$PC_MODELS_FRAMEWORK/Crews.PlanningCenter.Models.dll" \
	-r "$HOME/.nuget/packages/humanizer.core/$HUMANIZER_VERSION/lib/$HUMANIZER_FRAMEWORK/Humanizer.dll" \
	-r "$SCRIPT_DIR/../Crews.PlanningCenter.Api/bin/Debug/$BASE_PROJECT_FRAMEWORK/Crews.PlanningCenter.Api.dll" \
	-r "$SCRIPT_DIR/../Crews.PlanningCenter.Api.Generators/bin/Debug/$GENERATOR_FRAMEWORK/Crews.PlanningCenter.Api.Generators.dll" \
	"$SCRIPT_DIR/Templates/PlanningCenterContextTemplate.tt"

printf "Generating resource classes...\n"
t4 \
	-r "$HOME/.nuget/packages/crews.extensions.http/$HTTP_EXTENSIONS_VERSION/lib/$HTTP_EXTENSIONS_FRAMEWORK/Crews.Extensions.Http.dll" \
	-r "$HOME/.nuget/packages/crews.extensions.primitives/$PRIMITIVES_EXTENSIONS_VERSION/lib/$PRIMITIVES_EXTENSIONS_FRAMEWORK/Crews.Extensions.Primitives.dll" \
	-r "$HOME/.nuget/packages/crews.planningcenter.models/$PC_MODELS_VERSION/lib/$PC_MODELS_FRAMEWORK/Crews.PlanningCenter.Models.dll" \
	-r "$HOME/.nuget/packages/humanizer.core/$HUMANIZER_VERSION/lib/$HUMANIZER_FRAMEWORK/Humanizer.dll" \
	-r "$SCRIPT_DIR/../Crews.PlanningCenter.Api/bin/Debug/$BASE_PROJECT_FRAMEWORK/Crews.PlanningCenter.Api.dll" \
	-r "$SCRIPT_DIR/../Crews.PlanningCenter.Api.Generators/bin/Debug/$GENERATOR_FRAMEWORK/Crews.PlanningCenter.Api.Generators.dll" \
	"$SCRIPT_DIR/Templates/PlanningCenterResourceTemplate.tt"

printf "Generating API client classes...\n"
t4 \
	-r "$HOME/.nuget/packages/crews.extensions.http/$HTTP_EXTENSIONS_VERSION/lib/$HTTP_EXTENSIONS_FRAMEWORK/Crews.Extensions.Http.dll" \
	-r "$HOME/.nuget/packages/crews.extensions.primitives/$PRIMITIVES_EXTENSIONS_VERSION/lib/$PRIMITIVES_EXTENSIONS_FRAMEWORK/Crews.Extensions.Primitives.dll" \
	-r "$HOME/.nuget/packages/crews.planningcenter.models/$PC_MODELS_VERSION/lib/$PC_MODELS_FRAMEWORK/Crews.PlanningCenter.Models.dll" \
	-r "$HOME/.nuget/packages/humanizer.core/$HUMANIZER_VERSION/lib/$HUMANIZER_FRAMEWORK/Humanizer.dll" \
	-r "$SCRIPT_DIR/../Crews.PlanningCenter.Api/bin/Debug/$BASE_PROJECT_FRAMEWORK/Crews.PlanningCenter.Api.dll" \
	-r "$SCRIPT_DIR/../Crews.PlanningCenter.Api.Generators/bin/Debug/$GENERATOR_FRAMEWORK/Crews.PlanningCenter.Api.Generators.dll" \
	"$SCRIPT_DIR/Templates/PlanningCenterClientTemplate.tt"

printf "\nCompleted in $SECONDS seconds.\n"

printf "\nREMINDER: You can safely ignore errors that begin with 'Could not write output file'.\n"
