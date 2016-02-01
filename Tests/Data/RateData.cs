using System;

namespace Tests.Data
{       
    [Serializable]
    public class RateData
    {
        #region Configuration entries               
        public DateTime DateStamp { get; set; }        
        public string Rate { get; set; }                       
        #endregion        

        public const string DEFAULT_FILE_NAME = "\\WeekendRateCheckData.xml";

        public RateData() { }              
        public RateData(DateTime dateStamp, string rate)
        {
            DateStamp = dateStamp;
            Rate = rate;
        }
    }    
}
