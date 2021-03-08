using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Assets
{
    class DatabaseInit
    {
        private static String databaseName = "UserData";//need to change depend on your databse name
        private static IMongoDatabase db;
        private static DatabaseInit instance = new DatabaseInit();
        private static MongoClient dc;

        public DatabaseInit()
        {

            //dc = new MongoClient("mongodb+srv://AllInOneUser:AllInOneAdmin@university.0om1z.mongodb.net/University?retryWrites=true&w=majority");
            dc = new MongoClient("mongodb+srv://cz3003:Password1@cluster0.sj9w8.mongodb.net/test?authSource=admin&replicaSet=atlas-bwa64u-shard-0&readPreference=primary&appname=MongoDB%20Compass&ssl=true");
            db = dc.GetDatabase(databaseName);
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });


        }
        public static DatabaseInit getInstance()
        {
            return instance;
        }

        public List<User> retrieveAllUser()
        {

            IMongoCollection<User> c_collection = db.GetCollection<User>("User");
            List<User> au = c_collection.Find(new BsonDocument()).ToList();


            return au;
        }

        public List<String> retrieveAllUsername()
        {
            List<String> un = new List<string>();
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var p = Builders<User>.Projection.Include(x => x.Username);
            List<User> aun = u_collection.Find(new BsonDocument()).Project<User>(p).ToList();
            foreach (User c in aun)
            {
                un.Add(c.Username);
            }
            return un;
        }

        //Retrieve User by UserName, presume unique username for all users
         public User retrieveUser(String Username)
        {
            User usr = new User();
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var p = Builders<User>.Filter.Eq(x=>x.Username,Username);
            List<User> lUsr;

            lUsr=u_collection.Find(p).ToList();
            if (lUsr.Count > 0)
            {
                usr = lUsr[0];
                return usr;
            }
            return usr;
        }

        public Boolean createUser(User usr)
        {
            User tmp = new User("abc","123",11,0,"Tom Dick Harry","Adock@gmail.com");
            Boolean createUserSuccess = false;
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            u_collection.InsertOne(tmp);
            createUserSuccess = true;
            return createUserSuccess;

        }
        public Boolean deleteUser(String Username)
        {
            Boolean deleteStatus = false;
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var p = Builders<User>.Filter.Eq(x => x.Username, Username);
            long deleteCount=u_collection.DeleteOne(p).DeletedCount;
            if (deleteCount == 1)
            {
                return deleteStatus = true;
            }
            return deleteStatus;
        }
    }
}
