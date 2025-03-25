namespace Hakaton.Utility
{
    public static class MockDataUtility
    {
        public static string GetMockDataFromFile(string jsonFilename)
        {
            string rootPath=Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            rootPath = Directory.GetParent(rootPath).Parent.Parent.FullName;
            var JsonFilePath=Path.Combine(rootPath, "ConfigPrompt", jsonFilename);
            return JsonFilePath;
            //var jsonStringData=File.ReadAllText(JsonFilePath);
            //return jsonStringData;
        }
    }
}
