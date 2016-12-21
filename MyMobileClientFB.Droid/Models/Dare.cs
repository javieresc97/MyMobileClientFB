using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace MyMobileClientFB.Droid.Models
{
    [DataTable("Dare")]
    public class Dare
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [CreatedAt]
        public DateTime CreatedAt { get; set; }
        [UpdatedAt]
        public DateTime UpdatedDate { get; set; }
        [Deleted]
        public bool Deleted { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
    }
}