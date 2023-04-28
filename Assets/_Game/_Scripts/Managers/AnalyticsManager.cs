using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

namespace LogicPlatformer
{
    public class AnalyticsManager : MonoBehaviour
    {
        async void Start()
        {
            try
            {
                await UnityServices.InitializeAsync();
                List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
            }
            catch (ConsentCheckException e)
            {
                // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
                Debug.Log(e.ToString());
            }
        }

        public void AnaliticLoadLevel(int loadLevel)
        {
            AppMetrica.Instance.ReportEvent($"LevelWin: {loadLevel}");

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "levelName", "level_" + loadLevel.ToString()}
            };

            // The ‘levelCompleted’ event will get cached locally 
            //and sent during the next scheduled upload, within 1 minute
            AnalyticsService.Instance.CustomData("levelCompleted", parameters);
            // You can call Events.Flush() to send the event immediately
            AnalyticsService.Instance.Flush();
        }
    }
}

