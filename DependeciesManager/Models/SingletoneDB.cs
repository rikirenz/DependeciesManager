using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependeciesManager.Model
{
    class DB
    {
        private static DB single_instance = null;

        public Dictionary<String, HashSet<String>> db;

        private DB()
        {
            db = new Dictionary<String, HashSet<String>>();
        }

        public static DB getInstance()
        {
            if (single_instance == null)
                single_instance = new DB();

            return single_instance;
        }   

    }
}
