using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominion;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Persistence
{
    public class TestData
    {
        public static async Task InsertData(CoursesContext context, UserManager<User> userManager){
            Console.WriteLine("dentro de TestData");
            if(!userManager.Users.Any()){
                var user = new User{FullName = "Test Testerson", UserName="test", Email = "test@gmail.com"};
                await userManager.CreateAsync(user,"password.123");
            }
        }
    }
}