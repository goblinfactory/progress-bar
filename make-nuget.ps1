"Hello world, building first nuget package"
$dir= '.\Goblinfactory.Konsole\'
Push-Location
cd $dir
nuget pack .\Goblinfactory.ProgressBar.csproj -Prop Configuration=Release
Pop-Location