using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Coffee.GitDependencyResolver
{
    internal static class DirUtils
    {
        public static void Delete(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }

        public static void Move(string srcDir, string dstDir, Func<string, bool> pred = null)
        {
            Debug.Log("2");
            Create(dstDir);
            Debug.Log("3");
            // Move directories.
            foreach (var d in Directory.GetDirectories(srcDir)
                .Select(x => Path.GetFileName(x))
                .Where(x => pred == null || pred(x))
                )
            {
                string to = Path.Combine(dstDir, d);
                Directory.CreateDirectory(to);
                Move(Path.Combine(srcDir, d), to, pred);
            }

            Debug.Log("4");

            // Move files.
            foreach (var name in Directory.GetFiles(srcDir)
                .Select(x => Path.GetFileName(x))
                .Where(x => pred == null || pred(x)))
            {
                File.Move(Path.Combine(srcDir, name), Path.Combine(dstDir, name));
            }
            Debug.Log("5");
        }

        // WORKING
        /*
        public static void Move(string srcDir, string dstDir, Func<string, bool> pred = null)
        {
            Debug.Log("2");
            Create(dstDir);
            Debug.Log("3");
            // Move directories.
            foreach (var d in Directory.GetDirectories(srcDir)
                .Select(x => Path.GetFileName(x))
                .Where(x => pred == null || pred(x))
                )
            {
                Debug.Log("dir:" + d);
                string localPath = Path.Combine(srcDir, d);
                foreach (var name in Directory.GetFiles(localPath)
                   .Select(x => Path.GetFileName(x))
                   .Where(x => pred == null || pred(x)))
                {
                    string to = Path.Combine(Path.Combine(dstDir, d), name);
                    Debug.Log("MOVE : " + Path.Combine(localPath, name) + "\nTO " + to);
                    Directory.CreateDirectory(Path.Combine(dstDir, d));
                    File.Move(Path.Combine(localPath, name), to);
                }
            }

            Debug.Log("4");

            // Move files.
            foreach (var name in Directory.GetFiles(srcDir)
                .Select(x => Path.GetFileName(x))
                .Where(x => pred == null || pred(x)))
            {
                File.Move(Path.Combine(srcDir, name), Path.Combine(dstDir, name));
            }
            Debug.Log("5");
        }*/



        public static void Create(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
