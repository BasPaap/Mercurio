using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Settings 
{
    private const string directoryName = "Mercurio";
    private const string fileName = "settings.xml";

    public string PortName { get; set; }
    public int BaudRate { get; set; }

    public static Settings Load()
    {
        var settingsDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), directoryName);

        if (!Directory.Exists(settingsDirectoryPath))
        {
            Directory.CreateDirectory(settingsDirectoryPath);
        }

        var fullPath = Path.Combine(settingsDirectoryPath, fileName);
        Settings settings = null;
        var serializer = new XmlSerializer(typeof(Settings));

        if (!File.Exists(fullPath))
        {
            settings = new Settings { PortName = "COM1", BaudRate = 9600 };

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                serializer.Serialize(fileStream, settings);
            }
        }
        else
        {
            using (var fileStream = new FileStream(fullPath, FileMode.Open))
            {
                settings = (Settings)serializer.Deserialize(fileStream);
            }
        }

        return settings;
    }

}
