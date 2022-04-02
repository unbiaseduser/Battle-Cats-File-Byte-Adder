using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using System.Windows.Forms;
// using System.Drawing;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CA2211 // Non-constant fields should not be visible

namespace filemodder;
public class FileMod
    {
        public static string[] paths;
        public static void SelectFile()
        {
            Console.WriteLine("Select files to make them encryptable");
            OpenFileDialog fd = new()
            {
                Multiselect = true,
                Filter = "bc files(*.*)|*.*",
                Title = "Select a battle cats game file"
            };
        if (fd.ShowDialog() != DialogResult.OK)
            {
                Console.WriteLine("Please select files");
                SelectFile();
            }
        paths = fd.FileNames;
        foreach (string path in paths)
            Console.WriteLine("Selected file: " + path);
    }
        public static byte[] AddExtraBytes(string path, bool overwrite = true, byte[] allBytes = null)
        {
            if (allBytes == null)
            {
                allBytes = File.ReadAllBytes(path);
            }
            // Make sure file length is divisible by 16, so it encrypts properly
            List<byte> ls = allBytes.ToList();
            int rem = (int)Math.Ceiling((decimal)ls.Count / 16);
            rem *= 16;
            rem -= ls.Count;
            for (int i = 0; i < rem && rem != 16; i++)
            {
                ls.Add((byte)rem);
            }
            if (overwrite)
            {
                File.WriteAllBytes(path, ls.ToArray());
            }
            return ls.ToArray();
        }
        [STAThread]
        public static void Main()
        {
            SelectFile();
            foreach (string path in paths)
                AddExtraBytes(path, true, null);
            Console.WriteLine("Done! You should be able to encrypt these files");
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
    }
    }