using System;

namespace List
{
	public class Node<T>
	{
		public Node<T> Next { get; set; }
		public T data { get; set; }
		public Node(T t)
        {
			Next = null;
			data = t;
        }
	}
}