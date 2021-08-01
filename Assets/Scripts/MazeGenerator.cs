using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Atributo para enumerar las posiciones de las paredes: izquierda, derecha, arriba, abajo
[Flags]
public enum WallState
{
    //0000 Sin paredes
    //1111 Cuatro paredes
    LEFT = 1, //0001
    RIGHT = 2, //0010
    UP = 4, //0100
    DOWN = 8, //1000

    //Para que la funcion de recursive backtracking pueda identificar una celda
    // visitada sin agregar paredes de mas al visitarla
    VISITED = 128, //1000 0000
}

//Coordenadas necesarias para llevar registro de la celda actual y las celdas visitadas
public struct Position
{
    public int X;
    public int Y;
}

//Guarda la información del recorrido trazado (posición de celda vecina y pared destruida)
public struct Neighbour
{
    public Position Position;
    public WallState SharedWall;
}

public static class MazeGenerator
{
    //Se crea la cuadricula inicial del laberinto con las dimensiones (width, height),
    //con paredes en todos sus lados.
    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                maze[i, j] = initial;
            }
        }
        //Devuelve el laberinto con las medidas dadas en MazeRenderer
        return ApplyRecursiveBacktracking(maze, width, height);
    }


    //Funcion que revisa todos los vecinos de la celda actual, y guarda cuales NO han sido visitados
    private static List<Neighbour> GetUnvisitedNeighbours(Position p, WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbour>();

        if (p.X > 0) //Revisa izquierda
        {
            if (!maze[p.X - 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.LEFT
                });
            }
        }

        if (p.Y > 0) //Revisa abajo
        {
            if (!maze[p.X, p.Y - 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y - 1
                    },
                    SharedWall = WallState.DOWN
                });
            }
        }

        if (p.Y < height - 1) //Revisa arriba
        {
            if (!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    SharedWall = WallState.UP
                });
            }
        }

        if (p.X < width - 1) //Revisa derecha
        {
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.RIGHT
                });
            }
        }

        return list;
    }

    //Funcion que devuelve la pared opuesta. Necesario para el recursive backtracking
    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }
    }

    //Algoritmo de recursive backtracking
    private static WallState[,] ApplyRecursiveBacktracking(WallState[,] maze, int width, int height)
    {
        var rng = new System.Random(/*seed*/);
        //Stack necesario para recordar el camino tomado
        var positionStack = new Stack<Position>();
        //Indica la celda inicial
        var position = new Position { X = rng.Next(0, width), Y = rng.Next(0, height) };

        //Visitada
        maze[position.X, position.Y] |= WallState.VISITED;
        //Posicion nueva en el stack
        positionStack.Push(position);

        //loop para reiterar hasta que se acabe el stack
        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze, width, height);

            //Si esta cuenta es mayor a cero, aun hay celdas que visitar
            if (neighbours.Count > 0)
            {
                positionStack.Push(current);

                //Busca un vecino aleatorio al cual visitar
                var randIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.Position;
                //Eliminar pared en medio
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);
                
                //Marcar como visitada la nueva celda
                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;

                //Empuja la posicion del vecino en el stack
                positionStack.Push(nPosition);
            }
        }

        return maze;
    }
}