using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cofim.Web.Helpers
{
    public interface IMailHelper
    {
        void SendEmail(string to, string subject, string bodyMail);

        void SendEmailAccountConfirmation(string to, string token);

        void SendEmailRecoverPwd(string to, string tokenLink);


    }//Class
}//Namespace
