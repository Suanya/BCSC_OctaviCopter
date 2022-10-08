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

    public static User AddNewUser(string userName)
    {
        
        int userID = GenerateRandomID();
        connection.Insert(new User
        {
            UserID = userID,
            UserName = userName
        });

        return GetUser(userName);
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

    public static int GenerateRandomID()
    {
        // note: in public release, this should not be this
        
        return Random.Range(1, 1000000);

        // once PlayFab is set up, compare to PlayFab IDs and see if unique
        // keep generating until unique id is generated
    }

    public static string GetLastCompletedLevel(string userName)
    {
        return "BoltScene";
    }


}
