namespace MateMachine.CodeChallenge.CurrencyConverterLib; 

//https://cis300.cs.ksu.edu/graphs/impl/
//https://www.koderdojo.com/blog/breadth-first-search-and-shortest-path-in-csharp-and-net-core
internal class Graph {
	public Graph(IEnumerable<string> vertices, IEnumerable<Tuple<string, string, double>> edges) {
		AdjacencyList = new();
		foreach (var vertex in vertices) {
			AddVertex(vertex);
		}

		foreach (var edge in edges) {
			AddEdge(edge);
		}
	}

	public Dictionary<string, HashSet<string>> AdjacencyList { get; }

	private void AddVertex(string vertex) {
		AdjacencyList[vertex] = new HashSet<string>();
	}

	private void AddEdge(Tuple<string, string, double> edge) {
		var (from, to, _) = edge;

		if (!AdjacencyList.ContainsKey(from) || !AdjacencyList.ContainsKey(to)) return;

		AdjacencyList[from].Add(to);
		AdjacencyList[to].Add(from);
	}
}