using System;
using System.Collections.Generic;
using System.Reflection;

namespace GholfReg.Domain.Data
{
    public static class MockData
    {
        public static List<GolfDay> GolfDays { get; set; } = new List<GolfDay>();

        static MockData()
        {
            GolfDays.Add(new GolfDay() { Name = "GK Brackenhof", Date = new DateTime(2015, 05, 07), Description = "ons kerk se gholf dag" });
        }
    }
}
