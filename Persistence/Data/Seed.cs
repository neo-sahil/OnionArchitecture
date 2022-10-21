﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Persistence.Data
{
    public class Seed
    {
        public static async Task SeedUsers(ApplicationDbContext context)
        {
            try
            {
                if (await context.Users.AnyAsync()) return;

                var userData = await System.IO.File.ReadAllTextAsync("D:/Projects/.Net 6'/OnionAcchitecture/Persistence/Data/UserSeedData.json");

                var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

                foreach (var user in users)
                {
                    using var hmac = new HMACSHA512();

                    user.UserName = user.UserName.ToLower();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("12345"));
                    user.PasswordSalt = hmac.Key;
                    context.Users.Add(user);
                }
            }
            catch (Exception ex)
            {

            }

            await context.SaveChangesAsync();

        }
    }
   
}
