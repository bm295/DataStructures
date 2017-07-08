using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {

    static void Main(String[] args) {
        string a = Console.ReadLine();
        string b = Console.ReadLine();
        int cnt = 0;
        Dictionary<char, int> dicA = new Dictionary<char, int>();
        Dictionary<char, int> dicB = new Dictionary<char, int>();
        foreach(char x in a) {
            bool found = false;
            bool hasKey = false;
            if (dicA.ContainsKey(x)) {
                hasKey = true;
            }
            foreach(char y in b) {
                if (x == y) {
                    found = true;
                    if (!hasKey) {
                        dicA[x] = dicA.ContainsKey(x) ? dicA[x] + 1 : 1;
                    }
                }
            }
            if (!found) {
                cnt++;
            }
        }
        foreach(char x in b) {
            bool found = false;
            bool hasKey = false;
            if (dicB.ContainsKey(x)) {
                hasKey = true;
            }
            foreach(char y in a) {
                if (x == y) {
                    found = true;
                    if (!hasKey) {
                        dicB[x] = dicB.ContainsKey(x) ? dicB[x] + 1 : 1;
                    }
                }
            }
            if (!found) {
                cnt++;
            }
        }
        foreach(var item in dicA) {
            cnt += Math.Abs(item.Value - dicB[item.Key]);
        }
        Console.WriteLine(cnt);
    }
}
