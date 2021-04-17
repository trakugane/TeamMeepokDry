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

    public class DatabaseInit : MonoBehaviour, IDatabaseInit
    {
        public static DatabaseInit dbInit;
        private static String databaseName = "MPDry";//need to change depend on your databse name
        public static IMongoDatabase db;
        private static DatabaseInit instance = new DatabaseInit();
        private static MongoClient dc;
        private int x = 0;


        private void Awake()
        {

            if (DatabaseInit.dbInit == null)
            {
                DatabaseInit.dbInit = this;

            }
            DontDestroyOnLoad(DatabaseInit.dbInit);
        }

        public void Start()
        {

            //dc = new MongoClient("mongodb+srv://AllInOneUser:AllInOneAdmin@university.0om1z.mongodb.net/University?retryWrites=true&w=majority");
            dc = new MongoClient("mongodb+srv://cz3003:Password1@cluster0.sj9w8.mongodb.net/test?authSource=admin&replicaSet=atlas-bwa64u-shard-0&readPreference=primary&appname=MongoDB%20Compass&ssl=true");
            db = dc.GetDatabase(databaseName);
            x++;
            if (x > 0)
            {
                BsonClassMap.RegisterClassMap<User>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
                BsonClassMap.RegisterClassMap<Question>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
                BsonClassMap.RegisterClassMap<Assignment>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
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
            usr = initialiseStage(usr);
            usr = initialisePvP(usr);
            try
            {
                IMongoCollection<User> u_collection = db.GetCollection<User>("User");
                u_collection.InsertOne(usr);
            }
            catch
            {
                return createUserSuccess;
            }


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
        public User initialisePvP(User newUser)
        {
            PVP tp = new PVP();
            tp.AccumulatedPoints = 0;
            tp.PastGame = new List<GameHistory>();
            newUser.mpStatus = tp;
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

        public Boolean addQuestion(Question qn)
        {
            Boolean createQnSuccess = false;
            try
            {
                IMongoCollection<Question> qn_collection = db.GetCollection<Question>("Question");
                qn_collection.InsertOne(qn);

                //qn_collection.InsertMany(lqn);


            }
            catch
            {
                return createQnSuccess;

            }


            createQnSuccess = true;
            return createQnSuccess;
        }
        public List<Question> getQuestionsByCreator(String creatorEmail)
        {

            IMongoCollection<Question> qn_collection = db.GetCollection<Question>("Question");
            var p = Builders<Question>.Filter.Eq(x => x.creatorEmail, creatorEmail);
            List<Question> lQn = new List<Question>();

            lQn = qn_collection.Find(p).ToList();
            return lQn;
        }
        public List<Question> retrieveAllQuestion()
        {

            IMongoCollection<Question> qn_collection = db.GetCollection<Question>("Question");
            return qn_collection.Find(new BsonDocument()).ToList();



        }
        public List<Question> getQuestionsByType(String qnType)
        {

            IMongoCollection<Question> qn_collection = db.GetCollection<Question>("Question");
            var p = Builders<Question>.Filter.Eq(x => x.questionType, qnType);
            List<Question> lQn = new List<Question>();

            lQn = qn_collection.Find(p).ToList();
            return lQn;
        }
        public Boolean deleteQn(String qnId)
        {
            Boolean deleteStatus = false;
            IMongoCollection<Question> qn_collection = db.GetCollection<Question>("Question");
            var p = Builders<Question>.Filter.Eq(x => x.id, ObjectId.Parse(qnId));
            long deleteCount = qn_collection.DeleteOne(p).DeletedCount;
            if (deleteCount == 1)
            {
                return deleteStatus = true;
            }
            return deleteStatus;
        }

        public List<String> retrieveAllStudentEmail()
        {
            List<String> un = new List<string>();
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var p = Builders<User>.Projection.Include(x => x.email);
            var filter = Builders<User>.Filter.Eq(u => u.accountType, 0);
            List<User> aun = u_collection.Find(filter).Project<User>(p).ToList();
            foreach (User c in aun)
            {
                un.Add(c.email);
            }
            return un;
        }

        //add new assignment
        public Boolean createAssignment(Assignment asn)
        {
            Boolean createAsnSuccess = false;
            try
            {
                IMongoCollection<Assignment> asn_collection = db.GetCollection<Assignment>("Assignment");
                asn_collection.InsertOne(asn);
                createAsnSuccess = true;
                return createAsnSuccess;
            }
            catch
            {
                return createAsnSuccess;

            }




        }
        public List<Assignment> getAllAssignmentStaff()
        {
            IMongoCollection<Assignment> a_collection = db.GetCollection<Assignment>("Assignment");
            return a_collection.Find(new BsonDocument()).ToList();
        }


        //first time
        public List<AssignmentRecord> initialiseAssignmentStudent(List<String> stnEmail)
        {

            List<AssignmentRecord> asrList = new List<AssignmentRecord>();
            //create AssignmentRecord object and push to mongo
            foreach (String stn in stnEmail)
            {
                AssignmentRecord asr = new AssignmentRecord(-1, stn);
                asrList.Add(asr);
            }

            return asrList;
        }

        public Boolean assignAllStudentAssgn(String asnName)
        {
            Boolean assignStatus = false;

            //get all student email. 
            List<String> stnEmilList = retrieveAllStudentEmail();
            //initialiseAssignmentStudent (for assignmentrecord in assignment)
            List<AssignmentRecord> asrList = initialiseAssignmentStudent(stnEmilList);

            try
            {
                //update Assignment with record
                IMongoCollection<Assignment> a_collection = db.GetCollection<Assignment>("Assignment");
                var filter = Builders<Assignment>.Filter.Where(x => x.assignmentName == asnName);
                var update = Builders<Assignment>.Update.Set(x => x.result, asrList);
                a_collection.FindOneAndUpdate(filter, update);

                //require user.AssignmentStatus=new List<String>();
                IMongoCollection<User> u_collection = db.GetCollection<User>("User");
                foreach (String snEmail in stnEmilList)
                {
                    var filter2 = Builders<User>.Filter.Where(x => x.email == snEmail);
                    var update2 = Builders<User>.Update.Push(x => x.assignmentAssigned, asnName);
                    u_collection.FindOneAndUpdate(filter2, update2);
                }

                assignStatus = true;
                return assignStatus;
            }
            catch
            {

                return assignStatus;
            }

            //pushAssignmentName to student

        }
        public Boolean updateScore(String email, String asnName, int score)
        {
            Boolean updateSuccess = false;
            try
            {
                IMongoCollection<Assignment> u_collection = db.GetCollection<Assignment>("Assignment");
                var filter = Builders<Assignment>.Filter.Where(x => x.assignmentName == asnName && x.result.Any(rn => rn.userEmail == email));
                var update = Builders<Assignment>.Update.Set(x => x.result[-1].score, score);
                u_collection.FindOneAndUpdate(filter, update);
                updateSuccess = true;
                return updateSuccess;
            }
            catch
            {
                return updateSuccess;
            }

        }

        public Boolean deleteAssignmentStudent(String email, String asnName)
        {
            Boolean updateSuccess = false;
            try
            {
                IMongoCollection<User> u_collection = db.GetCollection<User>("User");
                var filter = Builders<User>.Filter.Where(x => x.email == email && x.assignmentAssigned.Any(rn => rn == asnName));
                var update = Builders<User>.Update.Unset(x => x.assignmentAssigned[-1]);
                u_collection.FindOneAndUpdate(filter, update);
                updateSuccess = true;
                return updateSuccess;
            }
            catch
            {
                return updateSuccess;
            }
        }
        public Assignment getAssignemt(String asnName)
        {
            Assignment an = null;
            try
            {
                IMongoCollection<Assignment> a_collection = db.GetCollection<Assignment>("Assignment");
                var p = Builders<Assignment>.Filter.Eq(x => x.assignmentName, asnName);
                List<Assignment> lAsn;

                lAsn = a_collection.Find(p).ToList();
                if (lAsn.Count > 0)
                {
                    an = lAsn[0];
                    return an;
                }
                return an;
            }
            catch
            {
                return an;
            }

        }

        public List<User> getLeaderboard()
        {

            List<User> aun = null;
            try
            {
                List<String> lstScoreEmail = new List<String>();
                IMongoCollection<User> u_collection = db.GetCollection<User>("User");
                var p = Builders<User>.Projection.Include(x => x.email).Include(x => x.mpStatus.AccumulatedPoints);
                var filter = Builders<User>.Filter.Eq(u => u.accountType, 1);
                aun = u_collection.Find(new BsonDocument()).Project<User>(p).ToList();
                return aun;



            }
            catch
            {
                return aun;
            }


        }

        public Boolean PVPincrement(String email)
        {
            IMongoCollection<User> u_collection = db.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Where(x => x.email == email);
            var update = Builders<User>.Update.Inc(u => u.mpStatus.AccumulatedPoints, 3);
            u_collection.FindOneAndUpdate(filter, update);

            return true;
        }
    }
}