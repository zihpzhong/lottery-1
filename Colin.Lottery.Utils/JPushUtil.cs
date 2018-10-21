﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jiguang.JPush;
using Jiguang.JPush.Model;

namespace Colin.Lottery.Utils
{
    public static class JPushUtil
    {
        private static readonly JPushClient Client;

        static JPushUtil()
        {
            Client = new JPushClient(ConfigUtil.Configuration["JPush:AppKey"],
                ConfigUtil.Configuration["JPush:MasterSecret"]);
        }

        public static async Task PushNotificationAsync(string title, string alert, object audience)
        {
            var pushPayload = new PushPayload()
            {
                Platform = new List<string> {"android", "ios"},
                Audience = audience,
                Notification = new Notification
                {
                    Alert = alert,
                    Android = new Android
                    {
                        Alert = alert,
                        Title = title
                    },
                    IOS = new IOS
                    {
                        Alert = alert,
                        Badge = "+1"
                    }
                },
                Options = new Options
                {
                    IsApnsProduction = true // 设置 iOS 推送生产环境。不设置默认为开发环境。
                }
            };

            await Client.SendPushAsync(pushPayload);
        }

        public static async Task PushMessageAsync(string title, string content, object audience,
            Dictionary<string, string> extras = null)
        {
            var pushPayload = new PushPayload()
            {
                Platform = new List<string> {"android", "ios"},
                Audience = audience,
                Message = new Message
                {
                    Title = title,
                    Content = content,
                    Extras = extras
                },
                Options = new Options
                {
                    IsApnsProduction = true // 设置 iOS 推送生产环境。不设置默认为开发环境。
                }
            };

            await Client.SendPushAsync(pushPayload);
        }

//        private static void ExecuteDeviceEample()
//        {
//            var registrationId = "12145125123151";
//            var devicePayload = new DevicePayload
//            {
//                Alias = "alias1",
//                Mobile = "12300000000",
//                Tags = new Dictionary<string, object>
//                {
//                    {"add", new List<string>() {"tag1", "tag2"}},
//                    {"remove", new List<string>() {"tag3", "tag4"}}
//                }
//            };
//            var response = _client.Device.UpdateDeviceInfo(registrationId, devicePayload);
//            Console.WriteLine(response.Content);
//        }
//
//        private static void ExecuteReportExample()
//        {
//            var response = _client.Report.GetMessageReport(new List<string> {"1251231231"});
//            Console.WriteLine(response.Content);
//        }
//
//        private static void ExecuteScheduleExample()
//        {
//            var pushPayload = new PushPayload
//            {
//                Platform = "all",
//                Notification = new Jiguang.JPush.Model.Notification
//                {
//                    Alert = "Hello JPush"
//                }
//            };
//            var trigger = new Trigger
//            {
//                StartDate = "2017-08-03 12:00:00",
//                EndDate = "2017-12-30 12:00:00",
//                TriggerTime = "12:00:00",
//                TimeUnit = "week",
//                Frequency = 2,
//                TimeList = new List<string>
//                {
//                    "wed", "fri"
//                }
//            };
//            var response = _client.Schedule.CreatePeriodicalScheduleTask("task1", pushPayload, trigger);
//            Console.WriteLine(response.Content);
//        }
    }
}