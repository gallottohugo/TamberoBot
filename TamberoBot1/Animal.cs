using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamberoBot1
{
    public class Animal
    {
        public string current_herd { get; set; }
        public string current_feed { get; set; }
        public string heat_status { get; set; }
        public string rp_number { get; set; }
        public string id_animal { get; set; }
        public string icon { get; set; }
        public string diffprod_icon { get; set; }
        public string days_in_milk { get; set; }
        public string last_weight { get; set; }
        public string medal_icon { get; set; }
        public string last_production { get; set; }
        public string bestProduction { get; set; }
        public string avgProduction { get; set; }
        public string avgaAnimalControl { get; set; }
        public string nocontrol_icon { get; set; }
        public string heat_detected_icon { get; set; }
        public string heat_icon { get; set; }
        public string heat_icon_text { get; set; }
        public string todry_icon { get; set; }
        public string biotics_icon { get; set; }
        public string warning_icon { get; set; }
        public string move_icon { get; set; }
        public string weighing_icon_before { get; set; }
        public string weighing_icon_after { get; set; }
        public string body_weight_gain_icon { get; set; }
        public string weaning_icon { get; set; }
        public string HeatStressLevel { get; set; }
        public string TemperatureHumidityIndex { get; set; }
        public string HeatStressLevelDesc { get; set; }
        public string HeatStressLevelStatus { get; set; }

        public class RootObject
        {
            public Animal animal { get; set; }
        }
    }
}