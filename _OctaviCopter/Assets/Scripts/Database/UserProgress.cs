using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

[Table ("UserProgress")]
public class UserProgress 
{
   [PrimaryKey] public int LevelID { get; set; }

    public string userName { get; set; } //FK on User
    public int UserID { get; set; }    
    public int LevelNumber { get; set; }
    public string LevelName { get; set; }
}
