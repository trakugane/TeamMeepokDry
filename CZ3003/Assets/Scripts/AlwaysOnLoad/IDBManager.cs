public interface IDBManager
{
    bool checkEmail(string email);
    void checkLogin(string sceneName);
    bool checkRegister(string sceneName);
    bool validateAccountCreation(string email, string password, string personalName);
    object validateUser(string email, string password);
}