using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
#if !PLATFORM_WEBGL
    public Discord.Discord discord;
#endif
    public static long id = 1160178187666456606;

    // Use this for initialization
    void Start()
    {
#if !PLATFORM_WEBGL
        discord = new Discord.Discord(id, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = "Main Menu",
            Details = "Waiting to play a level..."
        };
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("Everything is fine!");
            }
        });
        //DontDestroyOnLoad(this);
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if !PLATFORM_WEBGL
        var activityManager = discord.GetActivityManager();
        var timestampStart = new ActivityTimestamps()
        {
            Start = Elapsed.elapsed.Ticks
        };
        var activity = new Discord.Activity
        {
            State = "Main Menu",
            Details = "Waiting to play a level...",
            Timestamps = timestampStart
        };
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("Everything is fine!");
            }
        });

        discord.RunCallbacks();
#endif
    }
}