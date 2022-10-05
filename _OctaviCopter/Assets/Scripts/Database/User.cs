using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

[Table("User")]
public class User 
{
    [PrimaryKey] public string UserName { get; set; }
    public long UserID { get; set; }        // used for external identification on PlayFab

    public static long CurrentUserID => 1;
}
