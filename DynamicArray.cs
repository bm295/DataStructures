using System;
using System.Collections.Generic;
using System.IO;
class Solution {
    static void Main(String[] args) {
        int[] firstLine = Array.ConvertAll(Console.ReadLine().Split(' '), Int32.Parse);
        int n = firstLine[0];
        int q = firstLine[1];
        int[][] inputs = new int[q][];
        for (int i = 0; i < q; i++) {
            string[] temp = Console.ReadLine().Split(' ');
            inputs[i] = Array.ConvertAll(temp, Int32.Parse);
        }
        PrintLastAns(n, q, inputs);
    }
    
    static void PrintLastAns(int n, int q, int[][] inputs) {
        List<List<int>> seqs = new List<List<int>>();
        for (int i = 0; i < n; i++) {
            seqs.Add(new List<int>());
        }
        int lastAns = 0;
        for (int i = 0; i < q; i++) {
            int seqIndex = (inputs[i][1] ^ lastAns) % n;
            if (inputs[i][0] == 1) {                
                seqs[seqIndex].Add(inputs[i][2]);
            }
            else {
                int size = seqs[seqIndex].Count;
                int key = inputs[i][2] % size;
                lastAns = seqs[seqIndex][key];
                Console.WriteLine(lastAns);
            }
        }
    }
}
