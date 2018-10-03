using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApisTokenAuth.Repository
{
    public class Accounts
    {

        //Varifying user credentials
        public bool Login(string userName, string password)
        {
            try
            {
                if (userName == "admin" && password=="admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}