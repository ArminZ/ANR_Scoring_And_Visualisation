set objArgs = WScript.Arguments
set o_installer = CreateObject("WindowsInstaller.Installer")
set o_database = o_Installer.OpenDatabase(objArgs(0), 1)
s_SQL = "INSERT INTO Property (Property, Value) Values( 'REINSTALLMODE', 'amus')"
set o_MSIView = o_DataBase.OpenView( s_SQL)
o_MSIView.Execute
o_DataBase.Commit