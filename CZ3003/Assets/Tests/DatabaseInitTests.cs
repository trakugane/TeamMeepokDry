using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Assets.Models;
using NUnit.Framework;
using UnityEngine.TestTools;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NSubstitute;
using FluentAssertions;


namespace Tests
{
    public class DatabaseInitTests
    {
        //private Assets.DatabaseInit dbInit;

        Assets.IDatabaseInit dbInit;

        [SetUp]
        public void SetUp()
        {
            dbInit = Substitute.For<Assets.IDatabaseInit>();
        }

        // Test to retrieve all users
        [Test]
        public void TestToRetrieveAllUsers()
        {
            User usr1 = new User("password", null, 0, "Tom", "email@gmail.com", null, new List<string>());
            User usr2 = new User("password2", null, 0, "Harry", "email2@gmail.com", null, new List<string>());
            User usr3 = new User("password3", null, 0, "Dick", "email3@gmail.com", null, new List<string>());
            List<User> listOfAllUsers = new List<User>();
            listOfAllUsers.Add(usr1);
            listOfAllUsers.Add(usr2);
            listOfAllUsers.Add(usr3);
            dbInit.retrieveAllUser().Returns(listOfAllUsers);
            Assert.AreEqual(listOfAllUsers, dbInit.retrieveAllUser());
        }

        // Test to retrieve all emails
        [Test]
        public void TestToRetrieveAllEmails()
        {
            User usr1 = new User("password", null, 0, "Tom", "email@gmail.com", null, new List<string>());
            User usr2 = new User("password2", null, 0, "Harry", "email2@gmail.com", null, new List<string>());
            User usr3 = new User("password3", null, 0, "Dick", "email3@gmail.com", null, new List<string>());
            List<User> listOfAllUsers = new List<User>();
            listOfAllUsers.Add(usr1);
            listOfAllUsers.Add(usr2);
            listOfAllUsers.Add(usr3);

            dbInit.retrieveAllEmail().Returns(x =>
            {
                List<string> allEmails = new List<string>();
                foreach (User c in listOfAllUsers)
                {
                    allEmails.Add(c.email);
                }
                return allEmails;
            });
            Assert.AreEqual(new List<string> { "email@gmail.com", "email2@gmail.com", "email3@gmail.com" }, dbInit.retrieveAllEmail());
        }

        // Test to retrieve a single user
        [Test]
        public void TestToRetrieveUser()
        {
            User usr1 = new User("password", null, 0, "Tom", "email@gmail.com", null, new List<string>());
            dbInit.retrieveUser(usr1.email).Returns(usr1);
            Assert.AreEqual(usr1, dbInit.retrieveUser(usr1.email));
        }

        // Test to retrieve a single user
        [Test]
        public void TestToCreateUser()
        {
            User usr1 = new User("password", null, 0, "Tom", "email@gmail.com", null, new List<string>());
            dbInit.createUser(usr1).Returns(true);
            Assert.IsTrue(dbInit.createUser(usr1));
        }

        // Test to delete a single user
        [Test]
        public void TestToDeleteUser()
        {
            User usr1 = new User("password", null, 0, "Tom", "email@gmail.com", null, new List<string>());
            dbInit.deleteUser(usr1.email).Returns(true);
            Assert.IsTrue(dbInit.deleteUser(usr1.email));
        }

        // Test to check if email exists
        [Test]
        public void TestToCheckEmail()
        {
            User usr1 = new User("password", null, 0, "Tom", "email@gmail.com", null, new List<string>());
            dbInit.checkEmailExists(usr1.email).Returns(true);
            Assert.IsTrue(dbInit.checkEmailExists(usr1.email));
        }

        // Test to check if password is correct
        [Test]
        public void TestToCheckPassword()
        {
            User usr1 = new User("password", null, 0, "Tom", "email@gmail.com", null, new List<string>());
            dbInit.checkPassword(usr1.email, usr1.Password).Returns(true);
            Assert.IsTrue(dbInit.checkPassword(usr1.email, usr1.Password));
        }

        // Test to add a question
        [Test]
        public void TestToAddQuestion()
        {
            Question q1 = new Question("Exercise1", "Multiplication", 25, "t@gmail.com");
            dbInit.addQuestion(q1).Returns(true);
            Assert.IsTrue(dbInit.addQuestion(q1));
        }

        // Test to get list of questions by creator
        [Test]
        public void TestToGetListOfQuestions()
        {

            Question q1 = new Question("Exercise1", "Multiplication", 25, "t@gmail.com");
            Question q2 = new Question("Exercise2", "Addition", 45, "t@gmail.com");
            Question q3 = new Question("Exercise3", "Division", 4, "t@gmail.com");
            List<Question> questionList = new List<Question> { q1, q2, q3 };

            dbInit.getQuestionsByCreator(Arg.Any<string>()).Returns(x =>
            {
                Question Q1 = new Question("Exercise1", "Multiplication", 25, (string)x[0]);
                Question Q2 = new Question("Exercise2", "Addition", 45, (string)x[0]);
                Question Q3 = new Question("Exercise3", "Division", 4, (string)x[0]);
                List<Question> QuestionList = new List<Question> { Q1, Q2, Q3 };

                return QuestionList;
            });

            questionList.Should().BeEquivalentTo(dbInit.getQuestionsByCreator("t@gmail.com"));
        }

        // Test to get by questions by type
        [Test]
        public void TestToGetListOfQuestionsByType()
        {

            Question q1 = new Question("Exercise1", "Multiplication", 25, "t@gmail.com");
            Question q2 = new Question("Exercise2", "Addition", 45, "t@gmail.com");
            Question q3 = new Question("Exercise3", "Division", 4, "t@gmail.com");
            Question q4 = new Question("Exercise4", "Multiplication", 60, "t@gmail.com");
            List<Question> questionList = new List<Question> { q1, q4 };

            dbInit.getQuestionsByType(Arg.Any<string>()).Returns(x =>
            {
                Question Q1 = new Question("Exercise1", "Multiplication", 25, "t@gmail.com");
                Question Q2 = new Question("Exercise2", "Addition", 45, "t@gmail.com");
                Question Q3 = new Question("Exercise3", "Division", 4, "t@gmail.com");
                Question Q4 = new Question("Exercise4", "Multiplication", 60, "t@gmail.com");
                List<Question> QuestionList = new List<Question> { Q1, Q2, Q3, Q4 };
                List<Question> sameType = new List<Question>();
                foreach (Question q in QuestionList)
                {
                    if (q.questionType == (string)x[0])
                    {
                        sameType.Add(q);
                    }
                }

                return sameType;
            });

            questionList.Should().BeEquivalentTo(dbInit.getQuestionsByType("Multiplication"));
        }

        // Test to delete question
        [Test]
        public void TestToDeleteQuestion()
        {
            dbInit.deleteQn(Arg.Any<string>()).Returns(true);
            Assert.IsTrue(dbInit.deleteQn("123"));
        }

        // Test to retrieve all students' emails
        [Test]
        public void TestToRetrieveAllStudentsEmails()
        {
            User usr1 = new User("password", null, 0, "Tom", "email@gmail.com", null, new List<string>());
            User usr2 = new User("password2", null, 0, "Harry", "email2@gmail.com", null, new List<string>());
            User usr3 = new User("password3", null, 0, "Dick", "email3@gmail.com", null, new List<string>());
            User usr4 = new User("password4", null, 1, "TeacherA", "teacherA@gmail.com", null, new List<string>());
            User usr5 = new User("password5", null, 1, "TeaherB", "teacherB@gmail.com", null, new List<string>());

            List<User> listOfAllUsers = new List<User> { usr1, usr2, usr3, usr4, usr5 };

            dbInit.retrieveAllStudentEmail().Returns(x =>
            {
                List<string> allStudentEmails = new List<string>();
                foreach (User c in listOfAllUsers)
                {
                    if (c.accountType == 0)
                    {
                        allStudentEmails.Add(c.email);
                    }
                }
                return allStudentEmails;
            });
            Assert.AreEqual(new List<string> { "email@gmail.com", "email2@gmail.com", "email3@gmail.com" }, dbInit.retrieveAllStudentEmail());
        }

        // Test to create an assignment
        [Test]
        public void TestToCreateAssignment()
        {
            AssignmentRecord ar1 = new AssignmentRecord(-1, "test@gmail.com");
            AssignmentRecord ar2 = new AssignmentRecord(-1, "test2@gmail.com");
            List<AssignmentRecord> arList = new List<AssignmentRecord> { ar1, ar2 };
            Assignment a = new Assignment("Assignment 1", "teacher@gmail.com", arList, 0);

            dbInit.createAssignment(Arg.Any<Assignment>()).Returns(true);
            Assert.IsTrue(dbInit.createAssignment(a));
        }

        // Test to update score of student in an assignment, assignment record
        [Test]
        public void TestToUpdateStudentAssignmentScore()
        { 

            dbInit.updateScore(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>()).Returns(true);
            Assert.IsTrue(dbInit.updateScore("test@gmail.com", "Assignment1", 5));
        }

        // Test to delete assignment given to student
        [Test]
        public void TestToDeleteAssignmentGivenToStudent()
        {
            dbInit.deleteAssignmentStudent(Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            Assert.IsTrue(dbInit.deleteAssignmentStudent("test@gmail.com", "Assignment1"));
        }

        // Test to get an assignment by its name
        [Test]
        public void TestToGetAnAssignment()
        {
            Assignment A = new Assignment("Assignment 1", "teacher@gmail.com", new List<AssignmentRecord>(), 0);

            dbInit.getAssignemt(Arg.Any<string>()).Returns(x =>
            {
                Assignment a = new Assignment("Assignment 1", "teacher@gmail.com", new List<AssignmentRecord>(), 0);
                Assignment b = new Assignment("Assignment 2", "teacher@gmail.com", new List<AssignmentRecord>(), 0);
                Assignment c = new Assignment("Assignment 3", "teacher@gmail.com", new List<AssignmentRecord>(), 0);
                List<Assignment> assignmentList = new List<Assignment> { a, b, c };

                foreach (Assignment i in assignmentList)
                {
                    if (i.assignmentName == (string)x[0])
                    {
                        return i;
                    }
                }
                return null;
            });
            A.Should().BeEquivalentTo(dbInit.getAssignemt("Assignment 1"));
        }

    }
}
