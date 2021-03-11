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
        //await db.incrementCurrentStageAttempt(11, "a@gmail.com").ConfigureAwait(false);
        //await db.checkEmailExists("a@gmail.com").ConfigureAwait(false);
        await db.verifyAccount("a@gmail.com", "abc,");
    }
}
