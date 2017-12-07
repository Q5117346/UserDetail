using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserDetail.Models;

namespace UserDetail.Data
{
    public class UserDbInitializer
    {
        public static void Initialize(UserContext context)
        {
#if DEBUG
            //context.Database.EnsureDeleted();
#endif
            context.Database.EnsureCreated();
#if DEBUG
            List<User> testUsers = new List<User>();

            testUsers.Add(new User {id="test-id", name = "Test Name", email = "Test@email.com", phoneNumber = 12345678910, address = "1 Test Road Testville", canBuy = false});

            context.User.AddRange(testUsers);

            context.SaveChanges();
#endif
        }
    }
}
