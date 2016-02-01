using System;
using Tests.Data;

namespace Tests.Configuration
{
    [Serializable]
    public class GeneralConfig
    {
        private static GeneralConfig instance;
        
        #region Configuration entries
        public string MainDomian { get; set; } // the 'tut.by' part from  http://www.tut.by/ that could be changed on different env    
        // webDriver waiter interval
        public int DefaultTimeoutSec { get; set; }  //sec
        public int DefaultPollingIntervalMs { get; set; }  // ms
        // to do : add browsers ?    
        #endregion
        public WeekendRateCheckData WeekendRateData;

        static GeneralConfig()
        {            
            instance = new GeneralConfig();
        }              

        public static GeneralConfig Instance { get { return instance; }   set { instance = value; } }

        public static GeneralConfig CreateDefault()
        {
            GeneralConfig config = new GeneralConfig();            
            config.MainDomian = "tut.by";
            config.DefaultTimeoutSec = 30;        
            config.DefaultPollingIntervalMs = 500;        
            return config;
        }
    }    
}

