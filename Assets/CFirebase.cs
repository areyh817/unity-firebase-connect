﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class CFirebase : MonoBehaviour
{
    DatabaseReference m_Reference;

    void Start()
    {
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;

        WriteUserData("150", "김세린");
        WriteUserData("351", "김하늘");
        WriteUserData("12354", "조혜라");
        //WriteUserData("3", "dddd");

        ReadUserData();


    }

    void ReadUserData()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users")
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    for (int i = 0; i < snapshot.ChildrenCount; i++)
                        Debug.Log(snapshot.Child(i.ToString()).Child("username").Value);

                }
            });
    }

    void WriteUserData(string userId, string username)
    {
        m_Reference.Child("users").Child(userId).Child("username").SetValueAsync(username);
    }

}