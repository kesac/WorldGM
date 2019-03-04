# This script recreates the local SQL .db file that test web applications can use.
# Execute this within the context of project 'WorldGM.DataConsole'.

# Make sure to actually run WorldGM.DataConsole after this script!

$project = "WorldGM.DataConsole"
$db = "WorldGM.DataConsole/WorldGM_DataConsole_dev.db"

echo "Re-creating $db..."

if(Test-Path $db){
	echo "Deleting existing database..."
	rm $db
}

if(Test-Path $project){
	echo "Deleting existing migrations..."
	rm "$project/migrations/*"
}

echo "Initializing database..."
add-migration "Initialization" -Project "WorldGM.DataConsole"
update-database -Project "WorldGM.DataConsole"
