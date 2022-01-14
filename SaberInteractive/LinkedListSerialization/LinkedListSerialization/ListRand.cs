using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LinkedListSerialization
{
    class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        // Конструктор, необходимый для создания "пустого" списка
        public ListRand()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        // Конструктор, который автоматически заполняет список данными
        public ListRand(string[] data)
        {
            InitList(data);
            SetRandomNodes();
        }

        // Метод, предназначенный для инициализации списка и присвоения данных каждому из узлов
        private void InitList(string[] data)
        {
            Count = 0;

            while (Count < data.Length)
            {
                ListNode currentNode = new ListNode(data[Count]);

                if (Head == null)
                {
                    Head = currentNode;
                }
                else
                {
                    Tail.Next = currentNode;
                    currentNode.Prev = Tail;
                }

                Tail = currentNode;
                Count++;
            }
        }

        // Метод, предназначенный для назначения ссылки на случайный элмент списка каждому из узлов
        private void SetRandomNodes()
        {
            Dictionary<ListNode, int> dict = new Dictionary<ListNode, int>();

            int index = 0;
            ListNode currentNode = Head;

            while (currentNode != null)
            {
                dict.Add(currentNode, index);
                index++;
                currentNode = currentNode.Next;
            }

            currentNode = Head;
            System.Random randomGenerator = new Random(); 

            while (currentNode != null)
            {
                int randomIndex = randomGenerator.Next(0, Count);
                currentNode.Rand = dict.First(x => x.Value == randomIndex).Key;
                currentNode = currentNode.Next;
            }
        }

        public void Serialize(FileStream s)
        {
            Dictionary<ListNode, int> dict = new Dictionary<ListNode, int>();

            int index = 0;
            ListNode currentNode = Head;

            while (currentNode != null)
            {
                dict.Add(currentNode, index);
                index++;
                currentNode = currentNode.Next;
            }

            currentNode = Head;
            StringBuilder stringBuilder = new StringBuilder();

            while (currentNode != null)
            {
                if (dict.TryGetValue(currentNode.Rand, out int randIndex))
                {
                    stringBuilder.Append(currentNode.Data);
                    stringBuilder.Append(",");
                    stringBuilder.Append(randIndex.ToString());
                    if (currentNode.Next != null)
                        stringBuilder.Append("\n");
                }
                currentNode = currentNode.Next;
            }

            byte[] bytes = System.Text.Encoding.Default.GetBytes(stringBuilder.ToString());
            s.Write(bytes, 0, bytes.Length);
        }

        public void Deserialize(FileStream s)
        {
            byte[] bytes = new byte[s.Length];
            s.Read(bytes, 0, bytes.Length);
            string info = System.Text.Encoding.Default.GetString(bytes);
            string[] infoArr = info.Split('\n');

            string[] dataArr = new string[infoArr.Length];
            string[] randArr = new string[infoArr.Length];

            for (int i = 0; i < infoArr.Length; i++)
            {
                string[] temp = infoArr[i].Split(',');
                dataArr[i] = temp[0];
                randArr[i] = temp[1];
            }

            InitList(dataArr);

            Dictionary<ListNode, int> dict = new Dictionary<ListNode, int>();

            int index = 0;
            ListNode currentNode = Head;

            while (currentNode != null)
            {
                dict.Add(currentNode, index);
                index++;
                currentNode = currentNode.Next;
            }

            currentNode = Head;

            for (int i = 0; i < randArr.Length; i++)
            {
                currentNode.Rand = dict.First(x => x.Value == int.Parse(randArr[i])).Key;
                currentNode = currentNode.Next;
            }
        }
    }
}
