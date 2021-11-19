using App.DataAccess;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Core
{
    public class UserCore
    {
        public UserDTO Create(UserDTO user)
        {
            user.IsEnabled = true;
            user.CreatedDate = DateTime.Now;

            int returnValue = new UserDA().Create(user);
            user.Id = returnValue;

            return user;
        }

        /// <summary>
        /// Traer solo los registros activos
        /// </summary>
        /// <returns></returns>
        public List<UserDTO> List()
        {
            List<UserDTO> users = new UserDA().List();

            //Linq
            //users = (from u in users
            //         where u.IsEnabled == true
            //         select u).ToList();

            return users;
        }

        public UserDTO Get(int id)
        {
            UserDTO user = new UserDA().Get(id);
            return user;
        }

        public bool Update(UserDTO user)
        {
            user.UpdatedDate = DateTime.Now;

            bool isUpdated = new UserDA().Update(user);
            return isUpdated;
        }

        public bool Delete(int id)
        {
            bool isDeleted = new UserDA().Delete(id);
            return isDeleted;
        }

    }
}
