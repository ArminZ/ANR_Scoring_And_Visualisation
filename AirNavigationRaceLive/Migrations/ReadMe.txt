steps to create a new migration ( = update of DB, based on code)
SQL server:
==========
with SQL server Mgr, attach the 'old' DB to SQL express
give a simple name e.g. AAA

Code:
=====
open app.config
switch the connection string to use the SQLExpress DB
check that you use the DB name as specified in the app.config
in VS, open the Nuget Configuration Manager
after "PM>" type Add-Migration myMigrationName
this will add a new migration to your code
modify the down() part of the code if required (default value contraints must be dropped before columns can be removed)
(optional: you can also run in the Package manager the command Update-Database. In our case this is not required, the update will be handled from the code side)
change the app.config to use the original DB connection.

SQL server:
==========
detach the DB from SQL server
use a copy of the old DB version and copy it over the one that you 've just used

Now you are ready to start the code in VS
If everyhting goes well, the database will be updated
To verify, you can re-attach the DB and check that the new fields have been added, and a new row has been added to the table dbo.__MigrationHistory
