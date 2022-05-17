namespace MateMachine.CodeChallenge.CurrencyConverterLib;

//https://cis300.cs.ksu.edu/graphs/impl/
//https://www.koderdojo.com/blog/breadth-first-search-and-shortest-path-in-csharp-and-net-core
internal static class Dijkstra {
	public static Func<string, IEnumerable<string>?> ShortestPathFunction(Graph graph, string start) {
		var previous = new Dictionary<string, string>();

		var queue = new Queue<string>();
		queue.Enqueue(start);

		while (queue.Count > 0) {
			var vertex = queue.Dequeue();
			if (!graph.AdjacencyList.ContainsKey(vertex)) {
				return _ => null;
			}

			foreach (var neighbor in graph.AdjacencyList[vertex].Where(neighbor => !previous.ContainsKey(neighbor))) {
				previous[neighbor] = vertex;
				queue.Enqueue(neighbor);
			}
		}

		return ShortestPath;

		IEnumerable<string>? ShortestPath(string destination) {
			if (!previous.ContainsKey(destination)) {
				return null;
			}
			var path = new List<string>();

			var current = destination;
			while (!current.Equals(start)) {
				path.Add(current);
				current = previous[current];
			}

			path.Add(start);
			path.Reverse();

			return !path.Any() ? null : path;
		}
	}
}