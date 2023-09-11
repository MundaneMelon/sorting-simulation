using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class testing : MonoBehaviour
{
    public GameObject block;
    public int length;
    private int[] num_array;
    public int shuffle_num;
    private GameObject[] blocks;


    //Shuffling variables
    public float timer;
    private bool time_check = false;
    private double next_draw = 0;
    private int shuffled = 0;
    public int shuffle_per_frame;
    
    //Sorting variables
    private int index = 0;
    private bool sorting = true;
    public float sort_per_frame;
    private float sort_counter = 0;
    

    void Start()
    {
        blocks = new GameObject[length];
        num_array = random_array(length);
        for (int i = 0; i < length; i++)
        {
            GameObject temp;
            temp = Instantiate(block, new Vector3(0, 0, (float).1), Quaternion.identity);
            temp.transform.localScale = new Vector3(1, 1, 1);
            blocks[i] = temp;
        }
        Draw();

    }

    void Update()
    {
        if (shuffled < shuffle_num)
        {
            if (time_check == false)
            {
                next_draw = Time.time + timer;
                time_check = true;
            }

            if (time_check == true && Time.time > next_draw)
            {
                for (int i = 0; i < shuffle_per_frame; i++)
                {
                    Shuffle();
                }
                Draw();
                shuffled += shuffle_per_frame;
            }

            Debug.Log(Time.time);
        }
        else
        {
            if (sort_per_frame >= 1)
            {
                for (int i = 0; i < sort_per_frame; i++)
                {
                    Sort();
                }
                Draw();
            }
            else
            {
                sort_counter += sort_per_frame;
                Debug.Log(sort_counter);
                if (sort_counter >= 1)
                {
                    Sort();
                    Draw();
                    sort_counter = 0;
                }
            }
        }
    }

    void Draw()
    {
        float scale = 16f / (float)length;
        float yscale = 9f / (float)length;
        for (int i = 0; i < length; i++)
        {
            float x = 0;
            x += 10 * ((1 - scale) / 2);
            x -= ((i) * scale * 10);
            float y = 1;
            y = (9f / length) * (num_array[i]);
            float ypos = -45f + (y * 5);
            
            blocks[i].transform.position = new Vector3(x, ypos, (float).1);
            blocks[i].transform.localScale = new Vector3(scale, y, 1);
        }
            

    }
    void Shuffle()
    {
        bool check = false;
        int index1 = 0;
        int index2 = 0;
        while (check == false)
        {
            index1 = Random.Range(0, length);
            index2 = Random.Range(0, length);
            if (index1 != index2)
            {
                check = true;
            }
        }
        (num_array[index1], num_array[index2]) = (num_array[index2], num_array[index1]);
    }

    void Sort()
    {
        if (index < num_array.Length)
        {
            int smallest = index;
            for (int i = index; i < num_array.Length; i++)
            {
                if (num_array[i] < num_array[smallest])
                {
                    smallest = i;
                }
            }
            (num_array[index], num_array[smallest]) = (num_array[smallest], num_array[index]);
            index++;
        }
    }

    public static int[] random_array(int size)
    {
        int[] result = new int[size];
        for (int i = 0; i < size; i++)
        {
            result[i] = i+1;
        }

        return result;

    }
    
}
