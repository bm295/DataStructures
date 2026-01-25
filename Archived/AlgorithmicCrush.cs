using System;
using System.Collections.Generic;
using System.IO;
class Solution {
    static void Main(String[] args) {
        string[] firstLine = Console.ReadLine().Split(' ');
        int[] nm = Array.ConvertAll(firstLine, Int32.Parse);
        int n = nm[0];
        int m = nm[1];
        long[] diffArr = new long[n + 1];
        for(int i = 0; i < m; i++) {
            var line = Console.ReadLine().Split(' ');
            int[] input = Array.ConvertAll(line, Int32.Parse);
            var a = input[0] - 1;
            var b = input[1];
            var k = input[2];
            diffArr[a] += k;
            diffArr[b] -= k;
        }
        long max = 0;
        long temp = 0;
        for(int i = 0; i < diffArr.Length; i++) {
            temp += diffArr[i];
            max = Math.Max(max, temp);
        }
        Console.WriteLine(max);
    }
}
