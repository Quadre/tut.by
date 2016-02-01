using System;


namespace Tests.Data
{       
    [Serializable]
    public class WeekendRateCheckData
    {
        #region Configuration entries        
        public DateTime DateStamp { get; set; }        
        public string Rate { get; set; }                       
        #endregion

        public WeekendRateCheckData() { }              
        public WeekendRateCheckData(DateTime dateStamp, string rate)
        {
            DateStamp = dateStamp;
            Rate = rate;
        }
    }    
}
