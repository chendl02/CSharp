using System;
namespace List
{

	public class Glist<T>
	{
		private Node<T> head;
		private Node<T> tail;
		public Glist()
		{
			head=null;tail=null;
		}
		public Node<T> Head
        {
			get { return head; }
        }
		public void Add(T t)
        {
			Node<T> n = new Node<T>(t);
			
		}
	}
}