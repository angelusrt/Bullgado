using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour {
    //Serve para cham-los em outros contextos
    public static string WALL = "Wall";
    public static string FRUIT = "Fruit";
    public static string BOMB = "Bomb";
    public static string TAIL = "Tail";
    public static string BLUEFRUIT = "BlueFruit";

}

public class Metrics {
    //Metrica que dita o movimento do jogo
    public static float NODE = 1.2f;
}
public class Tile{
    public enum Type{Empty,Full}
    Type type;
    public int x{get; private set;}
    public int y{get; private set;}
    public int z{get; private set;}

    public Tile(int x,int y, int z){

        this.x = x;
        this.y = y;
        this.z = z;

        type = Type.Full;
    }
}

public enum PlayerDirection {
    //Nome dado as direções
    LEFT = 0,
    UP = 1,
    RIGHT = 2,
    DOWN = 3,
    COUNT = 4
}
