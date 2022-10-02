using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class UserDatabase 
{
    private static string databasePath = "UserDatabase.db";
    private static readonly SQLiteConnection connection;

    static UserDatabase()
    {
        connection = new SQLiteConnection(databasePath);
        connection.CreateTable<User>();
        connection.CreateTable<UserProgress>();
    }

    public static void RegisterUser(long userID, string userName)
    {
        connection.Insert(new User
        {
            UserID = userID,
            UserName = userName
        });

    }

    public static User GetUser(string userName)
    {
        try
        {
            // executed until an exception is thrown or it completes successfully
            return connection.Get<User>(userName);
        }
        catch
        {
            // catches exceptions
            return null;
        }
    }

}
