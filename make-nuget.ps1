"Hello world, building first nuget package"
$dir= '.\Goblinfactory.ProgressBar\'
Push-Location
cd $dir
nuget spec -f
Pop-Location