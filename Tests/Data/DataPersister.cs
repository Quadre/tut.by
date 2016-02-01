using System;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace Tests.Data
{        
    public class DataPersister<T>
    {

        /// <summary>
        /// Get folder path to ExecutingAssembly place
        /// </summary>
        public string DefaultConfigFolder
        {
            get
            {                
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        /// <summary>
        /// Helper function to check whether given fileName is present at folder with ExecutingAssembly
        /// </summary>
        /// <param name="defaultFileName">File to search</param>
        /// <returns></returns>
        public bool FileExist (string defaultFileName)
        {
            if  (defaultFileName.Length > 0)
                if (defaultFileName[0] != '\\')
                {
                    defaultFileName = "\\" + defaultFileName;
                }
            return File.Exists(DefaultConfigFolder + defaultFileName);            
        }

        /// <summary>
        /// Serialize & save data to filePath
        /// </summary>
        /// <param name="obj">object to be serialized</param>
        /// <param name="filePath">Fulliy qualified file path where serialized data ill be stored</param>
        public void Save(T obj, string filePath)
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                var stream = File.Exists(filePath) ? new FileStream(filePath, FileMode.Truncate) : new FileStream(filePath, FileMode.Create);
                xml.Serialize(stream, obj);
                stream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save object to XML document.", ex);
            }
            
        }

        /// <summary>
        /// De-Serialize previosly saved data from given filePath
        /// </summary>
        /// <param name="filePath">Fulliy qualified file path</param>
        /// <returns></returns>
        public T Load(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {                    
                    XmlSerializer xml = new XmlSerializer(typeof(T));
                    var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    T config = (T)xml.Deserialize(stream);
                    stream.Close();
                    return config;
                }
                else
                {
                    throw new FileNotFoundException("File '" + filePath + "' not found.");
                }
            }
            catch (Exception ex)
            {                
                throw new Exception("Failed to parse XML document, when converting it to object.", ex);
            }     
        }        
    }   
}
