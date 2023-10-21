using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Discord;
using UnityEngine.SocialPlatforms;

public class LevelRPC : MonoBehaviour
{
#if !PLATFORM_WEBGL
    private Discord.Discord discord;
#endif
    // Start is called before the first frame update
    void Start()
    {
#if !PLATFORM_WEBGL
        discord = new Discord.Discord(DiscordController.id, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        var timestampStart = new ActivityTimestamps()
        {
            Start = Elapsed.elapsed.Ticks
        };
        var activity = new Discord.Activity
        {
            State = "Playing a level",
            Details = "Level: " + SceneManager.GetActiveScene().name,
            Timestamps = timestampStart
        };
        discord.GetActivityManager().UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("All is fine!");
            }
        });
#endif
    }

    private void Update()
    {
#if !PLATFORM_WEBGL
        var timestampStart = new ActivityTimestamps()
        {
            Start = Elapsed.elapsed.Ticks
        };
        var activity = new Discord.Activity
        {
            State = "Playing a level",
            Details = "Level: " + SceneManager.GetActiveScene().name,
            Timestamps = timestampStart
        };
        discord.GetActivityManager().UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("All is fine!");
            }
        });
        discord.RunCallbacks();
#endif
    }
}
