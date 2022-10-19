using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class UserDatabase 
{
    private static string databasePath = Path.Combine(Application.persistentDataPath, "UserDatabase.db");
    private static readonly SQLiteConnection connection;

    static UserDatabase()
    {
        connection = new SQLiteConnection(databasePath);
        connection.CreateTable<User>();
        connection.CreateTable<UserProgress>();
    }

    public static void AddNewUser(string userName)
    {
        int userID = GenerateRandomID();
        connection.Insert(new User
        {
            UserID = userID,
            UserName = userName
        });

    }

    public static void UpdateLevel(string userName, long userID, int levelNumber, string levelName)
    {
        // try to find user in progress table
        UserProgress currentUserProgress = GetUserProgress(userName);

        if (currentUserProgress == null)
        {
            // first entry - just insert
            connection.Insert(new UserProgress
            {
                UserID = userID,
                UserName = userName,
                LevelNumber = levelNumber,
                LevelName = levelName
            });
        }
        else
        {
            // subsequent entry - update the values
            currentUserProgress.LevelNumber = levelNumber;
            currentUserProgress.LevelName = levelName;
            // save them
            connection.Update(currentUserProgress);
        }

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

    public static UserProgress GetUserProgress(string userName)
    {
        try
        {
            // executed until an exception is thrown or it completes successfully
            return connection.Get<UserProgress>(userName);

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
