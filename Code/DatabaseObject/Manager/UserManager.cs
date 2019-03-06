using DatabaseObject.DadaAccess;
using DatabaseObject.Domain;
using DatabaseObject.Util;
using System;
using System.Collections.Generic;

namespace DatabaseObject.Manager
{
    public class UserManager
    {
        private UserDao userDao = new UserDao();

        public UserInfo GetUser(string loginId)
        {
            using (ConnectionUtil conn = new ConnectionUtil())
            {
                UserInfo info = this.userDao.GetUser(loginId);

                return info;
            }
        }

        public UserInfo GetUser(int userId)
        {
            using (ConnectionUtil conn = new ConnectionUtil())
            {
                UserInfo info = this.userDao.GetUser(userId);

                return info;
            }
        }

        public int QueryUsersCount(string filterField, string filterText)
        {
            using (ConnectionUtil conn = new ConnectionUtil())
            {
                return this.userDao.QueryUsersCount(filterField, filterText);
            }
        }

        public List<UserInfo> QueryUsers(string filterField, string filterText, int curRowNum = 0, int pageSize = 0)
        {
            using (ConnectionUtil conn = new ConnectionUtil())
            {
                List<UserInfo> infos = this.userDao.QueryUsers(filterField, filterText, curRowNum, pageSize);

                return infos;
            }
        }

        public bool CheckUserLoginID(int id, string loginID)
        {
            using (ConnectionUtil conn = new ConnectionUtil())
            {
                return this.userDao.CheckUserLoginID(id, loginID);
            }
        }

        public UserInfo AddUser(UserInfo info)
        {
            try
            {
                TransactionUtil.OnEntry();

                info = this.userDao.AddUser(info);

                TransactionUtil.OnSuccess();

            }
            catch (Exception)
            {
                TransactionUtil.OnException();
                throw;
            }
            finally
            {
                TransactionUtil.OnExit();
            }

            return info;
            
        }

        public void UpdateUser(UserInfo info)
        {
            try
            {
                TransactionUtil.OnEntry();

                this.userDao.UpdateUser(info);

                TransactionUtil.OnSuccess();

            }
            catch (Exception)
            {
                TransactionUtil.OnException();
                throw;
            }
            finally
            {
                TransactionUtil.OnExit();
            }          
        }

        public void UpdatePassword(int id, string loginPwd)
        {
            try
            {
                TransactionUtil.OnEntry();

                this.userDao.UpdatePassword(id, loginPwd);

                TransactionUtil.OnSuccess();

            }
            catch (Exception)
            {
                TransactionUtil.OnException();
                throw;
            }
            finally
            {
                TransactionUtil.OnExit();
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                TransactionUtil.OnEntry();

                this.userDao.DeleteUser(id);

                TransactionUtil.OnSuccess();
            }
            catch (Exception)
            {
                TransactionUtil.OnException();
                throw;
            }
            finally
            {
                TransactionUtil.OnExit();
            }
        }
    }
}