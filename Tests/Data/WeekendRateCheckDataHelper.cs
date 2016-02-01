using System;
using Tests.Configuration;

namespace Tests.Data
{
    public class WeekendRateCheckDataHelper : ConfigHelperBase<WeekendRateCheckData>
    {

        public override string DefaultConfigName
        {
            get
            {
                return "\\WeekendRateCheckData.xml";
            }
        }
    }
}
