using System;
using System.Collections.Generic;
using System.IO;
class Solution {
    static void Main(String[] args) {
        var n = Convert.ToInt32(Console.ReadLine());
        string[] inputs = new string[n];
        for (int i = 0; i < n; i++) {
            inputs[i] = Console.ReadLine();
        }
        var q = Convert.ToInt32(Console.ReadLine());
        int[] patterns = new int[q];
        for (int i = 0; i < q; i++) {
            var pattern = Console.ReadLine();
            patterns[i] = 0;
            for(int j = 0; j < n; j++) {
                if (inputs[j] == pattern)
                    patterns[i]++;
            }
        }
        for (int i = 0; i < q; i++) {
            Console.WriteLine(patterns[i]);
        }
    }
}
