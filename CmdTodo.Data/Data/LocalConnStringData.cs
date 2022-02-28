using Newtonsoft.Json;

namespace MyTodo.Data.Data
{
    public static class LocalConnStringData
    {
        private static string fileName = "./settings.json";
        public static string GetConn()
        {
            if(!System.IO.File.Exists(fileName))
            {
                InitFile();
            }

            LocalFile connData = GetLocalFile();

            return connData.ConnString;
        }
        public static string GetTable()
        {
            if (!System.IO.File.Exists(fileName))
            {
                InitFile();
            }

            LocalFile connData = GetLocalFile();

            return connData.TableName;
        }
        private static LocalFile GetLocalFile()
        {
            string fileString = System.IO.File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<LocalFile>(fileString);
        }
        private static void InitFile()
        {
            var localFile = new LocalFile();
            var json = JsonConvert.SerializeObject(localFile);
            System.IO.File.WriteAllText(fileName, json);
        }

        public static void SetConn(string newConn)
        {
            if (!System.IO.File.Exists(fileName))
            {
                InitFile();
            }

            LocalFile connData = GetLocalFile();
            connData.ConnString = newConn;
            WriteFile(JsonConvert.SerializeObject(connData));
        }

        public static void SetTable(string newTable)
        {
            if (!System.IO.File.Exists(fileName))
            {
                InitFile();
            }

            LocalFile connData = GetLocalFile();
            connData.TableName = newTable;
            WriteFile(JsonConvert.SerializeObject(connData));
        }

        private static void WriteFile(string json)
        {
            var localFile = new LocalFile();
            System.IO.File.WriteAllText(fileName, json);
        }
    }
}
