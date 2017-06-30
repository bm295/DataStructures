using System;
using System.Collections.Generic;
using System.IO;
class Solution {
    static void Main(String[] args) {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
        string[] firstLine = Console.ReadLine().Split(' ');
        int[] firstNumbers = Array.ConvertAll(firstLine, Int32.Parse);
        int n = firstNumbers[0];
        int d = firstNumbers[1];
        string[] secondLine = Console.ReadLine().Split(' ');
        int[] input = Array.ConvertAll(secondLine, Int32.Parse);
        int[] output = new int[n];
        for(int i = 0; i < n; i++) {
            int outputI = (i - d) >= 0 ? (i - d) : Convert.ToInt32(Math.Abs(i - d + n));
            output[outputI] = input[i];            
        }
        for(int i = 0; i < n; i++) {
            Console.Write(i == (n - 1) ? output[i].ToString() : output[i] + " ");
        }
    }
}
