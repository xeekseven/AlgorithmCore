namespace LinkedList
{
    public class LinkList<TData> : ILinkList<TData>
    {
        public Node<TData> _head;
        public Node<TData> _last;
        public int _count = 0;

        public LinkList()
        {
            _last = _head;
        }

        public int Count { get => _count; }

        public TData DeleteHead()
        {
            TData data = _head.Data;
            _head = _head.Next;
            _count--;
            return data;
        }

        public TData DeleteTail()
        {
            if (_head.Next == null)
            {
                TData d = _head.Data;
                _head = null;
                _last = null;
                return d;
            }
            Node<TData> node = _head;
            while (node.Next.Next != null)
            {
                node = _head.Next;
            }
            TData data = node.Next.Data;
            _last = node;
            return data;
        }

        public void InsertHead(TData data)
        {
            Node<TData> next = _head;
            Node<TData> head = new Node<TData>
            {
                Data = data,
                Next = next

            };
            _head = head;
            _count++;
        }

        public void InsertTail(TData data)
        {
            Node<TData> next = new Node<TData>()
            {
                Data = data
            };
            if (_last == null)
            {
                _head.Next = next;
            }
            else
            {
                _last.Next = next;
            }
            _last = next;
            _count++;
        }

        public int Size()
        {
            return _count;
        }
    }
}