using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    [SerializeField] GameObject terrain;
    ImprovedNoise noise;
    int size = 200;
    int seed = 3257935;

    float[,] heights;

    float lacunarity = 2f,
          persistance = .5f;

    // Start is called before the first frame update
    void Start()
    {
        heights = new float[size, size];
        noise = new ImprovedNoise();


        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {

                float frequency = 1,
                      amplitude = 1;

                for (int oct = 0; oct < 4; oct++) {

                    float nx = (float)x / 20 * frequency,
                          nz = (float)y / 20 * frequency;

                    heights[x, y] += (float)noise.noise(nx, nz, seed)*amplitude;

                    frequency *= Mathf.Pow(lacunarity, oct);
                    amplitude *= Mathf.Pow(persistance, oct);
                }
            }
        }

        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                Instantiate(terrain, new Vector3(x, Mathf.Floor(heights[x, y]), y), Quaternion.identity);
            }
        }
    }
}
