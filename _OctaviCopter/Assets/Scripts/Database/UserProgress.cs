using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

[Table ("UserProgress")]
public class UserProgress 
{
   [PrimaryKey] public long LevelID { get; set; }
    public long UserID { get; set; }    // FK on User
    public int LevelNumber { get; set; }
    public string LevelName { get; set; }
}
