using DatabaseBackaup.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading;

namespace DatabaseBackaup.Helpers
{
    public static class SqlHelper
    {

        /// <summary>
        /// used to bckup sql server database 
        /// </summary>
        /// <param name="backup"></param>
        /// <returns></returns>
        public static BackupState Backup(Backup backup)

        {
            try
            {
                //Thread.Sleep(5000);

                string connectionString = $@"Data Source ={ backup.Server };Initial Catalog='master';Persist Security Info=True;User ID={backup.Username};Password={backup.Password} ";

                string finalData_Path = backup.BackupPath + "\\" + backup.Database + "__" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss").Replace(":", "-") + ".bak"; ;

                string Restore_Query = "Backup Database " + backup.Database + " To DISK='" + finalData_Path + "'";

                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                new SqlCommand(Restore_Query, con).ExecuteNonQuery();
                con.Close();

                return BackupState.finished;
            }
            catch (Exception)
            {
                return BackupState.failed;
            }

        }



    }
}
