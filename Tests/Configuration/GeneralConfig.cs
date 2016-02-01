using System;
using Tests.Data;

namespace Tests.Configuration
{
    [Serializable]
    public class GeneralConfig
    {
        #region Configuration entries
        /// <summary>
        /// The main dmain part ['tut.by']t from  http://www.tut.by/ that could be changed on different env    
        /// </summary>
        public string MainDomian { get; set; }

        /// <summary>
        /// WebDriver  Timeout in seconds, that should be used default
        /// </summary>
        public int DefaultTimeoutSec { get; set; }  //sec

        /// <summary>
        /// WebDriver  Polling interval, in milliseconds that should be used default
        /// </summary>
        public int DefaultPollingIntervalMs { get; set; }  // ms
        
        // to do : add multi-browsers support?  
        #endregion

        public const string DEFAULT_FILE_NAME = "\\config.xml";

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

