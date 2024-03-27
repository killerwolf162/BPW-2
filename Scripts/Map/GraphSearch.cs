using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GraphSearch
{
    public static Dictionary<Vector2Int, Vector2Int?> BFS(MapGrid map_graph, Vector2Int start_point, int movement_points) //breadth first search algorithm to make a highlight of all possible positions player can move to
    {
        Dictionary<Vector2Int, Vector2Int?> visited_nodes = new Dictionary<Vector2Int, Vector2Int?>();
        Dictionary<Vector2Int, int> cost_so_far = new Dictionary<Vector2Int, int>();
        Queue<Vector2Int> nodes_to_visit_queue = new Queue<Vector2Int>();

        nodes_to_visit_queue.Enqueue(start_point);
        cost_so_far.Add(start_point, 0);
        visited_nodes.Add(start_point, null);

        while (nodes_to_visit_queue.Count > 0)
        {

            Vector2Int current_node = nodes_to_visit_queue.Dequeue();
            foreach (Vector2Int neighbour_position in map_graph.get_neighbours_for(current_node))
            {
                if (map_graph.check_if_positiion_is_valid(neighbour_position) == false)
                    continue;

                int node_cost = map_graph.get_movement_cost(neighbour_position);
                int current_cost = cost_so_far[current_node];
                int new_cost = current_cost + node_cost;

                if (new_cost <= movement_points)
                {
                    if(!visited_nodes.ContainsKey(neighbour_position))
                    {
                        visited_nodes[neighbour_position] = current_node;
                        cost_so_far[neighbour_position] = new_cost;
                        nodes_to_visit_queue.Enqueue(neighbour_position);
                    }
                    else if (cost_so_far[neighbour_position] > new_cost)
                    {
                        cost_so_far[neighbour_position] = new_cost;
                        visited_nodes[neighbour_position] = current_node;
                    }
                }


            }


        }

        return visited_nodes;
    }
}
