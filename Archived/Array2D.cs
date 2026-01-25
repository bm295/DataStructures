using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {

    static void Main(String[] args) {
        int[][] arr = new int[6][];
        for(int arr_i = 0; arr_i < 6; arr_i++){
           string[] arr_temp = Console.ReadLine().Split(' ');
           arr[arr_i] = Array.ConvertAll(arr_temp,Int32.Parse);
        }
        int[] sums = SumsOfHourGlasses(arr);
        Console.WriteLine(LargestSum(sums));
    }
    
    static int LargestSum(int[] sums) {
        var max = sums[0];
        for(int i = 1; i < sums.Length; i++) {
            if (sums[i] > max)
                max = sums[i];
        }
        return max;
    }
    
    static int[] SumsOfHourGlasses(int[][] arr) {
        int[] sums = new int[16];
        for(int i = 0; i < 4; i++) {
            for(int j = 0; j < 4; j++) {
                sums[i * 4 + j] = SumOfHourGlass(arr, i, j);
            }
        }
        return sums;
    }
    
    static int SumOfHourGlass(int[][] arr, int i, int j) {
        return arr[i][j] + arr[i][j+1] + arr[i][j+2] + arr[i+1][j+1] + arr[i+2][j] + arr[i+2][j+1] + arr[i+2][j+2];
    }
}
