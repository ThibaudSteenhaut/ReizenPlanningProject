using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model
{
    public static class DummyDataSource
    {
        public static List<Reis> Reizen { get; set; } = new List<Reis>()
        {
            new Reis() { Bestemming="Rome", VertrekDatum=new DateTime(2020,12,30), TerugDatum=new DateTime(2021,1,10)},
             new Reis() { Bestemming="Parijs", VertrekDatum=new DateTime(2021,1,10),TerugDatum=new DateTime(2021,2,10)},
              new Reis() { Bestemming="Praag", VertrekDatum=new DateTime(2021,2,21), TerugDatum=new DateTime(2021,3,10)}
             };
    }
}
