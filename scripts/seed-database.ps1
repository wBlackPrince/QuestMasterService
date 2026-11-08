[CmdletBinding()]
param(
    [ValidateRange(1, [int]::MaxValue)]
    [int]$QuestionCount = 3000,

    [ValidateRange(1, [int]::MaxValue)]
    [int]$BatchSize = 250
)

$projectPath = Join-Path $PSScriptRoot "..\backend\QuestMasterService\src\QuestMasterService.Web\QuestMasterService.Web.csproj"

dotnet run --project $projectPath -- --seed --questions $QuestionCount --batch-size $BatchSize
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}
