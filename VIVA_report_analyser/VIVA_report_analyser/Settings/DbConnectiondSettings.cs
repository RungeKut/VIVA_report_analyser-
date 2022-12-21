using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVA_report_analyser.Settings
{
    internal class DbConnectionSettings
    {
        public String dbName;
        public Boolean connectAutomaticaly;
        public Boolean useLocalDb;
        public String LocalDbPath;
        public Boolean useWebDb;
        public String WebDbPath;

        public DbConnectionSettings()
        {
            dbName = "PILOT-DB";
            connectAutomaticaly = true;
            useLocalDb = true;
            LocalDbPath = "127.0.0.1:3306";
            useWebDb = false;
            WebDbPath = "http://localhost:5000/";
        }
    }
}
