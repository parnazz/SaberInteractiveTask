﻿namespace LinkedListSerialization
{
    class ListNode
    {
        public ListNode Prev;
        public ListNode Next;
        public ListNode Rand; // произвольный элемент внутри списка
        public string Data;

        public ListNode(string data)
        {
            Data = data;
        }
    }
}
