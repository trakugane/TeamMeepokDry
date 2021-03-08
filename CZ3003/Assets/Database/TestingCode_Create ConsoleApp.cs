using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Database
{
    class TestingCode_Create_ConsoleApp
    {
        DatabaseInit dbs = DatabaseInit.getInstance();
        User tmp = new User("abc", "123", 11, 0, "Tom Dick Harry", "Adock@gmail.com");
        dbs.createUser(tmp);
    }
}
