using System.IO;

namespace LinkedListSerialization
{
    partial class Program
    {
        static void Main(string[] args)
        {
            // Пример с сериализацией двусвязного списка
            using (FileStream s = new FileStream("Result.json", FileMode.Create))
            {
                string[] data = new string[] { "one", "two", "three", "four" };
                ListRand list = new ListRand(data);

                list.Serialize(s);
            }

            // Пример с десериализацией двусвязного списка
            using (FileStream s = File.OpenRead("Result.json"))
            {
                ListRand list = new ListRand();

                list.Deserialize(s);
            }
        }
    }
}
