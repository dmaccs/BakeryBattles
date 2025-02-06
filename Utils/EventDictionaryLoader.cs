//#if SCRIPT
#r "nuget: CsvHelper, 27.1.1"

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration;

var csvFilePath = "C:/Users/Dylan/Documents/Dylan Coding Projects/autobattler/Data/EventBook.csv";
if (!File.Exists(csvFilePath))
{
    Console.WriteLine($"CSV file not found: {csvFilePath}");
    return;
}

try
{
    var dictionary = ReadCsvToDictionary(csvFilePath);
    SaveDictionaryToFile(dictionary, "C:/Users/Dylan/Documents/Dylan Coding Projects/autobattler/Data/dictionary.json");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

static List<Dictionary<string, string>> ReadCsvToDictionary(string filePath)
{
    var list = new List<Dictionary<string, string>>();

    using (var reader = new StreamReader(filePath))
    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
    {
        csv.Read();
        csv.ReadHeader();
        var headers = csv.HeaderRecord;

        if (headers == null)
        {
            throw new Exception("CSV headers are null");
        }

        while (csv.Read())
        {
            var row = new Dictionary<string, string>();
            for (int i = 0; i < headers.Length; i++)
            {
                row[headers[i]] = csv.GetField(i);
            }
            list.Add(row);
        }
    }

    return list;
}

static void SaveDictionaryToFile(List<Dictionary<string, string>> list, string filePath)
{
    var options = new JsonSerializerOptions { WriteIndented = true };
    var json = JsonSerializer.Serialize(list, options);
    File.WriteAllText(filePath, json);
}
//#endif