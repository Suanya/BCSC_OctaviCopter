using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

[Table ("UserProgress")]
public class UserProgress 
{
   [PrimaryKey][AutoIncrement] public int LevelID { get; set; }
    
    public string UserName { get; set; } //FK on User
    public long UserID { get; set; }    
    public int LevelNumber { get; set; }
    public string LevelName { get; set; }

}
