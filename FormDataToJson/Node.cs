using System.Linq;

namespace FormDataToJson
{
    public class Node
    {
        public Node()
        {
            this.Children = new NodeCollection();
        }
        public bool HasChildren
        {
            get
            {
                return this.Children.Count() > 0;
            }
        }

        public bool IsList { get; set; }

        public string Key { get; set; }
        public string Data { get; set; }
        public NodeCollection Children { get; set; }

    }

}
