using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq.Dependencies.Sqlite;

[Table ("UserProgress")]
public class UserProgress 
{
   [PrimaryKey] public long LevelID { get; set; }
    public long UserID { get; set; }    // FK on User
    public int lLevelNumber { get; set; }
    public string LevelName { get; set; }
}
