namespace Dal;
using System.Xml.Linq;
using System.Xml.Serialization;

static class XMLTools
{
    const string s_dir = @"..\xml\";
    static XMLTools()
    {
        if (!Directory.Exists(s_dir))
            Directory.CreateDirectory(s_dir);
    }

    #region SaveLoadWithXElement
    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {dir + filePath}", ex);
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }

    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);
            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
        }
        catch (Exception ex)
        {
            //new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {dir + filePath}", ex);
            throw new Exception($"fail to load xml file: {filePath}", ex);
        }
    }
    #endregion

    #region SaveLoadWithXMLSerializer
    public static void SaveListToXMLSerializer<T>(List<T?> list, string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            XmlSerializer x = new(list.GetType());
            //XmlWriterSettings ws = new XmlWriterSettings();
            //ws.Indent = true;
            //XmlWriter writer = XmlWriter.Create(file, ws);
            x.Serialize(file, list);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {dir + filePath}", ex);            }
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }

    public static List<T?> LoadListFromXMLSerializer<T>(string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T?>));
            return x.Deserialize(file) as List<T?> ?? new();

        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {dir + filePath}", ex);            }
            throw new Exception($"fail to load xml file: {filePath}", ex);
        }
    }
    #endregion
}
