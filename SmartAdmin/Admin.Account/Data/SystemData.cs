using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Models;
using DBAccess;

namespace Account.Data
{
    public class SystemData : ISystemData
    {
        #region Construction

        private readonly IDBAccess db;

        public SystemData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> GetNextKey(string TableName, string KeyColumn)
        {
            var param = new Dictionary<string, object>
            {
                { "TableName",TableName },
                { "KeyColumn",KeyColumn }
            };

            string query = "adm.NumberFormat_Get";
            return db.Execute(query, param);
        }



    }
}
