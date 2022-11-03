using API.Context;
using API.Handlers;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class AccountRepository
    {
        private MyContext myContext;
        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Login (string email, string password)
        {
            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .SingleOrDefault(x => x.Employee.Email.Equals(email));
            if (data != null && Hashing.ValidatePassword(password, data.Password) == true)
            {         
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }
    }
}
