using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebApplication2.Models;

namespace WebApplication2.Security
{
    public class MyRoles : RoleProvider
    {
        public override string ApplicationName {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roller = null;
            YemekSiparisEntities n = new YemekSiparisEntities();
            Kullanici user = n.Kullanici.FirstOrDefault(x => x.kullaniciAdi == username);
            string rol = user.rol;

            char[] rolChar = rol.ToCharArray();
            roller = new string[rolChar.Length];
            for(int i = 0; i < rol.Length; i++)
            {
                roller[i] = rolChar[i].ToString();
            }

            return roller;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}