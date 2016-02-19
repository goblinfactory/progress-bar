"Hello world, building first nuget package"
$dir= '.\Goblinfactory.ProgressBar\'
Push-Location
cd $dir
nuget pack .\Goblinfactory.ProgressBar.csproj -Prop Configuration=Release
Pop-Location