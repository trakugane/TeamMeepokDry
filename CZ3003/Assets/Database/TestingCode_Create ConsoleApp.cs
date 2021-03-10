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
        DatabaseInit db = DatabaseInit.getInstance();
        User usr = null;
        StageProgress a = new StageProgress(1, 1);
        List<StageProgress> spl = new List<StageProgress>();
        spl.Add(a);
            SinglePlayer tmp = new SinglePlayer(spl);

        GameHistory gh = new GameHistory(1, "abc", 1, DateTime.Now);
        List<GameHistory> al = new List<GameHistory>();
        al.Add(gh);

            PVP t = new PVP(1, al);

        User usrt = new User("tanasf", "abc,", tmp, 1, "tanahcaow", "a@gmail.com", t);
        db.createUser(usrt);
            Console.WriteLine("testing user");
            Console.Write("Enter your name: ");

            string name = Console.ReadLine();
        Console.WriteLine($"Hello {name}");
    }
}
