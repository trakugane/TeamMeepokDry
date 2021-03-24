using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using UnityEngine;

namespace Assets
{
   
    public class DatabaseInit : MonoBehaviour
    {
        public static DatabaseInit dbInit;
        private static String databaseName = "UserData";//need to change depend on your databse name
        public static IMongoDatabase db;
        private static DatabaseInit instance = new DatabaseInit();
        private static MongoClient dc;
       
        private void Awake()
        {

            if (DatabaseInit.dbInit == null)
            {
                DatabaseInit.dbInit = this;

            }
            DontDestroyOnLoad(DatabaseInit.dbInit);
        }
        
        void Start()
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
            return c_collection.Find(new BsonDocument()).ToList();


           
        }

        public List<String> retrieveAllEmail()
        {
            List<String> un = new List<string>();
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var p = Builders<User>.Projection.Include(x => x.email);
            List<User> aun = u_collection.Find(new BsonDocument()).Project<User>(p).ToList();
            foreach (User c in aun)
            {
                un.Add(c.email);
            }
            return un;
        }

        //Retrieve User by email, presume unique email for all users
        public User retrieveUser(String email)
        {
            User usr = new User();
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var p = Builders<User>.Filter.Eq(x => x.email, email);
            List<User> lUsr;

            lUsr = u_collection.Find(p).ToList();
            if (lUsr.Count > 0)
            {
                usr = lUsr[0];
                return usr;
            }
            return null;
        }

        public Boolean createUser(User usr)
        {
            Boolean createUserSuccess = false;
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            usr = initialiseStage(usr);
            u_collection.InsertOne(usr);

            createUserSuccess = true;
            return createUserSuccess;

        }
        public User initialiseStage(User newUser)
        {
            List<StageProgress> spList = new List<StageProgress>();
            for (int i = 0; i <= 40; i += 10)
            {
                for (int j = 11; j <= 15; j++)
                {
                    int stg = i + j;
                    StageProgress stp = new StageProgress(stg, 0);
                    spList.Add(stp);
                }

            }
            SinglePlayer sp = new SinglePlayer(spList, 11);
            newUser.spProgress = sp;
            return newUser;

        }

        // Method to delete user through his email
        public Boolean deleteUser(String email)
        {
            Boolean deleteStatus = false;
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var p = Builders<User>.Filter.Eq(x => x.email, email);
            long deleteCount = u_collection.DeleteOne(p).DeletedCount;
            if (deleteCount == 1)
            {
                return deleteStatus = true;
            }
            return deleteStatus;
        }

        public Boolean incrementCurrentStageAttempt(int Stage, String email)
        {
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Where(x => x.email == email && x.spProgress.AllStageProgress.Any(i => i.Stage == Stage));
            var update = Builders<User>.Update.Inc(u => u.spProgress.AllStageProgress[-1].Attempt, 1);
            u_collection.FindOneAndUpdate(filter, update);
             
            return true;
        }
        public Boolean updateCurrentStage(int Stage, String email)
        {

            /*Stage += 6;*/
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Where(x => x.email == email);
            var update = Builders<User>.Update.Set(x => x.spProgress.currStage, Stage);
            u_collection.FindOneAndUpdate(filter, update);
            return true;

        }
        // Method to increment current stage attempt count
        /*public async Task<Boolean> incrementCurrentStageAttempt(int Stage, String email)
        {
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Where(x => x.email == email && x.spProgress.AllStageProgress.Any(i => i.Stage == Stage));
            var update = Builders<User>.Update.Inc(u => u.spProgress.AllStageProgress[-1].Attempt, 1);
            await u_collection.FindOneAndUpdateAsync(filter, update);
            return true;
        }
        public async Task<Boolean> updateCurrentStage(int Stage, String email)
        {

            Stage += 6;
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Where(x => x.email == email);
            var update = Builders<User>.Update.Set(x => x.spProgress.currStage, Stage);
            await u_collection.FindOneAndUpdateAsync(filter, update);
            return true;

        }*/
        public bool checkEmailExists(String email)
       {
      
           bool checkEmailExistsSuccess = false;
           IMongoCollection<User> u_collection = db.GetCollection<User>("User");

           var filter = Builders<User>.Filter.Eq(u => u.email, email);

           var user = u_collection.Find<User>(filter).ToList();
          
           if (user.Count == 1)
           {
               checkEmailExistsSuccess = true;
               Console.WriteLine("Email found");
           }
          

           return checkEmailExistsSuccess;
       }
        // Method to check if the email exists
        /*public async Task<Boolean> checkEmailExists(String email)
        {
          
            bool checkEmailExistsSuccess = false;
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
         
            var filter = Builders<User>.Filter.Eq(u => u.email, email);
         
            var user = await u_collection.Find<User>(filter).ToListAsync();
          
            if (user.Count == 1)
            {
                checkEmailExistsSuccess = true;
                Console.WriteLine("Email found");
            }

            return checkEmailExistsSuccess;
        }*/

        // Method to verify password
        public Boolean checkPassword(String email, String password)
        {
            bool checkPasswordCorrect = false;
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Where(x => x.email == email && x.Password == password);
            var user = u_collection.Find<User>(filter).ToList();

            if (user.Count == 1)
            {
                checkPasswordCorrect = true;
                Console.WriteLine("Password correct");
            }

            return checkPasswordCorrect;
        }

        // Method to verify account
        public Boolean verifyAccount(String email, String password)
        {
  
            bool emailExists = instance.checkEmailExists(email);
            
            if (!emailExists)
            {
                Console.WriteLine("Invalid email/password");
                return false;
            }

            bool checkPassword = instance.checkPassword(email, password);
            if (!checkPassword)
            {
                Console.WriteLine("Invalid email/password");
                return false;
            }
         
            Console.WriteLine("Login successful");
            return true;
        }

    }
}