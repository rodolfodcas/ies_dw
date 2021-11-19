using App.DataAccess;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Core
{
    public class BOCore
    {
        public BODTO Create(BODTO user)
        {
            user.IsEnabled = true;
            user.CreatedDate = DateTime.Now;

            int returnValue = new BODA().Create(user);
            user.Id = returnValue;

            return user;
        }

        /// <summary>
        /// Traer solo los registros activos
        /// </summary>
        /// <returns></returns>
        public List<BODTO> List()
        {
            List<BODTO> users = new BODA().List();

            //Linq
            //users = (from u in users
            //         where u.IsEnabled == true
            //         select u).ToList();

            return users;
        }

        public BODTO Get(int id)
        {
            BODTO user = new BODA().Get(id);
            return user;
        }

        public bool Update(BODTO user)
        {
            user.UpdatedDate = DateTime.Now;

            bool isUpdated = new BODA().Update(user);
            return isUpdated;
        }

        public bool Delete(int id)
        {
            bool isDeleted = new BODA().Delete(id);
            return isDeleted;
        }


    }
}
