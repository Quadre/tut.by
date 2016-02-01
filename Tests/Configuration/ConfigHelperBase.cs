using System;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace Tests.Configuration
{        
    public class ConfigHelperBase<SerializableClass>
    {
        public virtual string DefaultConfigName
        {
            get
            {
                return "\\Config.xml";
            }
        }

        public string DefaultConfigFolder
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }
        public string DefaultConfigFullPath
        {
            get
            {
                return DefaultConfigFolder + DefaultConfigName;
            }
        }

        public bool DefaultConfigExist()
        {
            return File.Exists(DefaultConfigFullPath);
        }



        public void Save(SerializableClass cfg)
        {
            Save(cfg, DefaultConfigFullPath); 
        }

        public void Save(SerializableClass cfg, string cfgPath)
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(SerializableClass));
                var stream = File.Exists(cfgPath) ? new FileStream(cfgPath, FileMode.Truncate) : new FileStream(cfgPath, FileMode.Create);
                xml.Serialize(stream, cfg);
                stream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save object to XML document.", ex);
            }
            
        }

        public SerializableClass Load()
        {
            return Load(DefaultConfigFullPath);
        }
        public SerializableClass Load(string cfgPath)
        {
            try
            {
                if (File.Exists(cfgPath))
                {                    
                    XmlSerializer xml = new XmlSerializer(typeof(SerializableClass));
                    var stream = new FileStream(cfgPath, FileMode.Open, FileAccess.Read);
                    SerializableClass config = (SerializableClass)xml.Deserialize(stream);
                    stream.Close();
                    return config;
                }
                else
                {
                    throw new FileNotFoundException("File '" + cfgPath + "' not found.");
                }
            }
            catch (Exception ex)
            {                
                throw new Exception("Failed to parse XML document, when converting it to object.", ex);
            }     
        }        
    }   
}
