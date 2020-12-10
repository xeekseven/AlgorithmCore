using System;
using System.Collections.Generic;
using System.IO;

namespace DataCompress
{
    public class HuffmanTrie
    {
        private static int R = 256;

        public static void Compress(string text)
        {
            char[] chars = text.ToCharArray();
            // 统计频次
            int[] freqs = new int[R];
            for (int i = 0; i < chars.Length; i++)
            {
                freqs[chars[i]]++;
            }
            HuffmanNode root = BuildTrie(freqs);
            // 构造编译表
            string[] st = new string[R];
            BuildCode(st, root, "");
            MemoryStream m = new MemoryStream();
            BinaryWriter outWriter = new BinaryWriter(m);
            WriteTrie(outWriter, root);
            Console.WriteLine(m.Length);

            for (int i = 0; i < text.Length; i++)
            {
                string code = st[text[i]];
                for (int j = 0; j < code.Length; j++)
                {
                    if (code[j] == '1')
                    {
                        outWriter.Write(true);
                    }
                    else
                    {
                        outWriter.Write(false);
                    }
                }
            }

        }

        private static string[] BuildCode(HuffmanNode root)
        {
            string[] st = new string[R];
            BuildCode(st, root, "");
            return st;
        }

        private static void BuildCode(string[] st, HuffmanNode x, string s)
        {
            if (x.isLeaf())
            {
                st[x.Ch] = s;
                return;
            }
            BuildCode(st, x.Left, s + '0');
            BuildCode(st, x.Right, s + '1');
        }

        private static HuffmanNode BuildTrie(int[] freqs)
        {
            // 对字母表进行频次如优先队列
            MinPQ<HuffmanNode> pq = new MinPQ<HuffmanNode>(1000);
            for (char ch = (char)0; ch < R; ch++)
            {
                if (freqs[ch] > 0)
                {
                    pq.Insert(new HuffmanNode(ch, freqs[ch], null, null));
                }
            }

            //合成频率相关最小的树
            while (pq.Count > 1)
            {
                HuffmanNode x = pq.DelMin();
                HuffmanNode y = pq.DelMin();
                HuffmanNode parent = new HuffmanNode('\0', x.Freq + y.Freq, x, y);
                pq.Insert(parent);
            }
            return pq.DelMin();
        }

        private static void WriteTrie(BinaryWriter writer, HuffmanNode x)
        {
            if (x.isLeaf())
            {
                writer.Write(true);
                writer.Write(x.Ch);
                return;
            }
            WriteTrie(writer, x.Left);
            WriteTrie(writer, x.Right);
        }
    }
}