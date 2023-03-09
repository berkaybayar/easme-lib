
using EasMe.Extensions;

namespace EasMe.Test
{

    public class CustomFileReader
    {
        public CustomFileReader(string filePath)
        {
            _filePath = filePath;
        }
        private readonly string _filePath;
        private bool _isRead = false;
        private List<string> _result = new();

        public List<string> Read()
        {
            if (_isRead)
                return _result;
            var allLines = File.ReadAllLines(_filePath);
            var mainKey = "";
            foreach (var item in allLines)
            {
                if(item.IsNullOrEmpty()) 
                    continue; //Skip empty lines
                if (item.StartsWith("  ")) 
                {
                    //SubKey
                    var addString = $"{mainKey}.{item.Trim()}";
                    _result.Add(addString);
                }
                //MainKey 
                if (item.Contains(':')) //If a mainKey with a value
                {
                    _result.Add(item);
                    continue;
                }
                mainKey = item;
            }

            _isRead = true;
            return _result;
        }
    }
}