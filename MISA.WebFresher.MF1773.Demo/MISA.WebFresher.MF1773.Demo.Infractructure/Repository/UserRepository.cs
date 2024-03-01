﻿using Dapper;
using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Infractructure
{
    public class UserRepository : IUserRepository
    {
        IDbContext _dbContext;
        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User?> GetUser(UserLogin userLogin)
        {
            //mã hóa pass
            //var pass_md5 = MD5Hash(userModel.Password);
            //var sql = "select * from Account where Username = @Username And Password = @Password
            var sql = "Proc_CheckedUser";

            var user = await _dbContext.Connection.QueryFirstOrDefaultAsync<User>(sql, userLogin);
            return user;
        }

        public async Task<User?> GetUserByToken(string token)
        {
            var sql = "Select * from User where RefreshToken = @RefreshToken";
            var param = new DynamicParameters();
            param.Add("@RefreshToken", token);
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<User>(sql, param);
            return res;
        }

        public async Task<bool> UpdateAsync(User user, Guid userId)
        {
            var res = await _dbContext.UpdateAsync<User>(userId,user);

            return res;
        }
        /*
public string MD5Hash(string password)
{
   MD5 md5 = System.Security.Cryptography.MD5.Create();
   byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
   byte[] hashBytes = md5.ComputeHash(inputBytes);

   // Step 2, convert byte array to hex string
   StringBuilder sb = new StringBuilder();
   for (int i = 0; i < hashBytes.Length; i++)
   {
       sb.Append(hashBytes[i].ToString("X2"));
   }
   return sb.ToString().ToLower();
}
*/
    }
}
