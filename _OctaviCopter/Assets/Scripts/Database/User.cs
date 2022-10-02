using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq.Dependencies.Sqlite;

[Table("User")]
public class User 
{
    [PrimaryKey] public long UserID { get; set; }
    public string UserName { get; set; }

    public static long CurrentUserID => 1;
}
