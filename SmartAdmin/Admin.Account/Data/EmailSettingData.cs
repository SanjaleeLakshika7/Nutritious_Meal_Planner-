using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Models;
using DBAccess;

namespace Account.Data
{
    public class EmailSettingData : IEmailSettingData
    {
        #region Construction

        private readonly IDBAccess db;

        public EmailSettingData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion

        public Task<string> AddEdit(string APIKey, EmailSettings obj)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },

                { "EmailServer",obj.EmailServer },
                { "SenderName",obj.SenderName },
                { "WebURL",obj.WebURL },
                { "SenderEmail",obj.SenderEmail },
                { "UseAuthentication",obj.UseAuthentication },
                { "SenderUsername",obj.SenderUsername },
                { "SenderPassword",obj.SenderPassword },
                { "PortNumber",obj.PortNumber },
                { "UseSSL",obj.UseSSL }
            };

            string query = "adm.EmailSettings_AddEdit";
            return db.Execute(query, param);
        }

        public Task<EmailSettings> Get(string APIKey)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey }
            };

            string query = "adm.EmailSettings_Get";
            return db.Get<EmailSettings, dynamic>(query, param);
        }

    }
}
