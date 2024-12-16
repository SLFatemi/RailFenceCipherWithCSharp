using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static void Main(string[] args)
    {
        string encoded = RailFenceCipher.Encode("HELLO", 3);
        Console.WriteLine($"Encoded: {encoded}");

        string decoded = RailFenceCipher.Decode(encoded, 3);
        Console.WriteLine($"Decoded: {decoded}");
    }
}

public class RailFenceCipher
{
    /// <summary>
    /// Encodes the given string using the Rail Fence Cipher with the specified number of rails.
    /// </summary>
    /// <param name="s">The string to encode.</param>
    /// <param name="n">The number of rails.</param>
    /// <returns>The encoded string.</returns>
    public static string Encode(string s, int n)
    {
        string[] rails = new string[n];
        int row = 0;
        while (row < n)
        {
            int i = row;
            string str = "";
            bool r = true;
            while (i < s.Length)
            {
                str += s[i];
                if (r)
                {
                    i += Calspace(row);
                    r = false;
                }
                else
                {
                    i += Calspace(n - row - 1);
                    r = true;
                }
            }
            rails[row] = str;
            row++;
        }
        string encoded = string.Join(null, rails);
        return encoded;

        /// <summary>
        /// Calculates the space between characters in the rail.
        /// </summary>
        /// <param name="row">The current row.</param>
        /// <returns>The space between characters.</returns>
        
        int Calspace(int row)
        {
            if (row == n - 1 || row == 0)
                return 2 * (n - 1);
            return 2 * (n - 1) - 2 * row;
        }
    }

    /// <summary>
    /// Decodes the given string using the Rail Fence Cipher with the specified number of rails.
    /// </summary>
    /// <param name="s">The string to decode.</param>
    /// <param name="n">The number of rails.</param>
    /// <returns>The decoded string.</returns>
    public static string Decode(string s, int n)
    {
        if (s.Length == 0)
        {
            return "";
        }

        string[] rails = new string[n];
        var l = s.Length;

        if (l % (2 * (n - 1)) == 0) // divisible
        {
            char[] results = new char[l];
            int K = l / (2 * (n - 1));
            string str1 = "";
            for (int i = 0; i < K; i++)
            {
                str1 += s[i];
            }
            rails[0] = str1;
            string str2 = "";
            for (int i = l - K; i < l; i++)
            {
                str2 += s[i];
            }

            int m = 1;
            for (int i = K; i < l - K; i += (l - (2 * K)) / (n - 2))
            {
                int x = (l - (2 * K)) / (n - 2);
                rails[m] = s.Substring(i, x);
                m++;
            }
            rails[n - 1] = str2;

            for (int i = 0; i < rails.Length; i++)
            {
                var f = i;
                int space = Calspace(i);
                bool r = true;
                foreach (var chr in rails[i])
                {
                    results[f] = chr;
                    if (r)
                    {
                        space = Calspace(i);
                        r = false;
                    }
                    else
                    {
                        space = Calspace(n - i - 1);
                        r = true;
                    }
                    f += space;
                }
            }
            string AbsoluteResults = string.Join(null, results);
            return AbsoluteResults;
        }
        else // not divisible
        {
            List<char> ls = s.ToList();
            int x = 0;
            int y;
            while ((n - 1) * x < l - n)
            {
                x++;
            }
            y = (n - 1) * x - l + n;
            char[] results = new char[ls.Count + y];

            if (x % 2 == 1) // top
            {
                int indexer = toppers(x) - 1;
                for (int i = 1; i < y; i++) // y-1 bar
                {
                    indexer += x + 1;
                }
                string str1 = "";
                for (int i = 0; i < toppers(x); i++)
                {
                    str1 += ls[i];
                }
                rails[0] = str1;
                string str2 = "";
                for (int i = ls.Count - toppers(x) + 1; i < ls.Count; i++)
                {
                    str2 += ls[i];
                }
                rails[n - 1] = str2;
                int m = 1;
                for (int i = toppers(x); i <= ls.Count - toppers(x); i += ((ls.Count - toppers(x) * 2 + 1) / (n - 2)))
                {
                    int o = (ls.Count - toppers(x) * 2 + 1) / (n - 2);
                    string u = string.Join(null, ls);
                    rails[m] = u.Substring(i, o);
                    m++;
                }
                for (int i = 0; i < rails.Length; i++)
                {
                    var f = i;
                    int space = 0;
                    bool r = true;
                    foreach (var chr in rails[i])
                    {
                        results[f] = chr;
                        if (r)
                        {
                            space = Calspace(i);
                            r = false;
                        }
                        else
                        {
                            space = Calspace(n - i - 1);
                            r = true;
                        }
                        f += space;
                    }
                }
                string AbsoluteResults = string.Join(null, results).Substring(0, ls.Count - y);
                return AbsoluteResults;
            }
            else // bottom
            {
                int indexer = ls.Count;
                for (int i = 1; i < y; i++) // y-1 bar
                {
                    if (i == 1)
                        indexer -= toppers(x);
                    else
                        indexer -= x;
                }
                string str1 = "";
                for (int i = 0; i < toppers(x); i++)
                {
                    str1 += ls[i];
                }
                rails[0] = str1;
                string str2 = "";
                for (int i = ls.Count - toppers(x); i < ls.Count; i++)
                {
                    str2 += ls[i];
                }
                rails[n - 1] = str2;
                int m = 1;
                for (int i = toppers(x); i <= ls.Count - toppers(x) - 1; i += ((ls.Count - toppers(x) * 2) / (n - 2)))
                {
                    int o = (ls.Count - toppers(x) * 2) / (n - 2);
                    string u = string.Join(null, ls);
                    rails[m] = u.Substring(i, o);
                    m++;
                }
                for (int i = 0; i < rails.Length; i++)
                {
                    var f = i;
                    int space = 0;
                    bool r = true;
                    foreach (var chr in rails[i])
                    {
                        results[f] = chr;
                        if (r)
                        {
                            space = Calspace(i);
                            r = false;
                        }
                        else
                        {
                            space = Calspace(n - i - 1);
                            r = true;
                        }
                        f += space;
                    }
                }
                string AbsoluteResults = string.Join(null, results).Substring(0, ls.Count - y);
                return AbsoluteResults;
            }
        }

        /// <summary>
        /// Calculates the space between characters in the rail.
        /// </summary>
        /// <param name="row">The current row.</param>
        /// <returns>The space between characters.</returns>
        int Calspace(int row)
        {
            if (row == n - 1 || row == 0)
                return 2 * (n - 1);
            return 2 * (n - 1) - 2 * row;
        }

        /// <summary>
        /// Calculates the number of characters in the top or bottom rail.
        /// </summary>
        /// <param name="x">The number of full cycles.</param>
        /// <returns>The number of characters in the top or bottom rail.</returns>
        int toppers(int x)
        {
            if (x % 2 == 0) // lowers
            {
                return x / 2 + 1;
            }
            x += 1; // toppers
            return x / 2 + 1;
        }
    }
}