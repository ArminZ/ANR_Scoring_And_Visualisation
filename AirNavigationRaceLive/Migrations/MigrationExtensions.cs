namespace AirNavigationRaceLive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Data.Entity.Migrations.Model;

    static internal class MigrationExtensions
    {
        public static void DeleteDefaultConstraint(this IDbMigration migration, string tableName, string colName, bool suppressTransaction = false)
        {
            // see https://stackoverflow.com/questions/17894906/ef-migration-for-changing-data-type-of-columns
            // (function name there with typo: DeleteDefaultContraint)

            var sql = new SqlOperation(String.Format(@"DECLARE @SQL varchar(1000)
        SET @SQL='ALTER TABLE {0} DROP CONSTRAINT ['+(SELECT name
        FROM sys.default_constraints
        WHERE parent_object_id = object_id('{0}')
        AND col_name(parent_object_id, parent_column_id) = '{1}')+']';
        PRINT @SQL;
        EXEC(@SQL);", tableName, colName)) { SuppressTransaction = suppressTransaction };
            migration.AddOperation(sql);
        }
    }
}
