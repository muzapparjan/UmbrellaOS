namespace UmbrellaOS.Lab.Brains.Stupid
{
    public class StupidBrain
    {
        public struct Node
        {
            public float weight;

            public static Node Default() => new()
            {
                weight = 0.5f
            };
            public static Node Random(Random random) => new()
            {
                weight = random.NextSingle()
            };
        }
        public struct Connection
        {
            public float weight;

            public static Connection Default() => new()
            {
                weight = 0.5f
            };
            public static Connection Random(Random random) => new()
            {
                weight = random.NextSingle()
            };
        }

        private int[] indices;
        private Node[] nodes;
        private Connection[,] connections;
        private List<float[]> history;

        public StupidBrain(Node[] nodes, Connection[,] connections)
        {
            this.nodes = nodes;
            this.connections = connections;
            indices = new int[nodes.Length];
            for (var i = 0; i < indices.Length; i++)
                indices[i] = i;
            history = new();
        }

        public void Input(float[] data, Random random)
        {
            history.Add(data);
            random.Shuffle(indices);
            foreach(var index in indices)
            {
                var node = nodes[index];
                //TODO
            }
        }

        public static int[] RandomIndices(int count, Random random)
        {
            var indices = new int[count];
            for (var i = 0; i < count; i++)
                indices[i] = i;
            random.Shuffle(indices);
            return indices;
        }
        public static Node[] RandomNodes(int count, Random random)
        {
            var nodes = new Node[count];
            for (var i = 0; i < count; i++)
                nodes[i] = Node.Random(random);
            return nodes;
        }
        public static Connection[,] RandomConnections(int nodeCount, int minConnectionCount, int maxConnectionCount, Random random)
        {
            var indices = RandomIndices(nodeCount, random);
            var connections = new Connection[nodeCount, nodeCount];
            for (var i = 0; i < nodeCount; i++)
            {
                random.Shuffle(indices);
                var count = Math.Max(Math.Min(random.Next(minConnectionCount, maxConnectionCount + 1), nodeCount), 0);
                for (var j = 0; j < count; j++)
                    connections[i, indices[j]] = Connection.Random(random);
            }
            return connections;
        }
        public static StupidBrain Random(int nodeCount, int minConnectionCount, int maxConnectionCount, Random random)
        {
            var nodes = RandomNodes(nodeCount, random);
            var connections = RandomConnections(nodeCount, minConnectionCount, maxConnectionCount, random);
            return new(nodes, connections);
        }
    }
}