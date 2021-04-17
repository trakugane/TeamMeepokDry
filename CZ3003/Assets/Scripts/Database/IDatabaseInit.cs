using System.Collections.Generic;
using Assets.Models;

namespace Assets
{
    public interface IDatabaseInit
    {
        bool addQuestion(Question qn);
        bool assignAllStudentAssgn(string asnName);
        bool checkEmailExists(string email);
        bool checkPassword(string email, string password);
        bool createAssignment(Assignment asn);
        bool createUser(User usr);
        bool deleteAssignmentStudent(string email, string asnName);
        bool deleteQn(string qnId);
        bool deleteUser(string email);
        List<Assignment> getAllAssignmentStaff();
        Assignment getAssignemt(string asnName);
        List<User> getLeaderboard();
        List<Question> getQuestionsByCreator(string creatorEmail);
        List<Question> getQuestionsByType(string qnType);
        bool incrementCurrentStageAttempt(int Stage, string email);
        List<AssignmentRecord> initialiseAssignmentStudent(List<string> stnEmail);
        User initialisePvP(User newUser);
        User initialiseStage(User newUser);
        bool PVPincrement(string email);
        List<string> retrieveAllEmail();
        List<Question> retrieveAllQuestion();
        List<string> retrieveAllStudentEmail();
        List<User> retrieveAllUser();
        User retrieveUser(string email);
        void Start();
        bool updateCurrentStage(int Stage, string email);
        bool updateScore(string email, string asnName, int score);
        bool verifyAccount(string email, string password);
    }
}