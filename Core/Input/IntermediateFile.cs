using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace StoryParser
{
    public static class IntermediateFile
    {
        static IntermediateFile()
        {
            Current = new();
            Tags = new();
        }
        public static readonly Dictionary<string, File> Current;
        public static readonly Dictionary<string, Locator> Tags;
        public static event Action? Loading;
        public static event Action? Loaded;
        private static readonly char[] disableSigns = new[] { 'Y', 'y', 'T', 't', '是' };
        private static void AddLine(File file, string line)
        {
            if (line[0] == Separators.COMMENT || disableSigns.Contains(line[0])) return;
            file.AddLine(line[1..]);
        }
        /// <summary>
        /// 异步读取指定中间文件
        /// </summary>
        /// <param name="name">文件名（并非路径，只是用于标识）</param>
        /// <param name="stream">目标文件流</param>
        /// <param name="encoding">编码方式</param>
        public async static void LoadAsync(string name, Stream stream, Encoding encoding)
        {
            Loading?.Invoke();
            File file = new();
            Current.Add(name, file);
            using (StreamReader sr = new StreamReader(stream, encoding))
            {
                string? line;
                await Task.Run(() => { while ((line = sr.ReadLine()) != null) AddLine(file, line); });
            }
            Loaded?.Invoke();
        }
        /// <summary>
        /// 异步读取指定中间文件
        /// <param name="name">文件名（并非路径，只是用于标识）</param>
        /// <param name="content">文本内容</param>
        /// </summary>
        public async static void LoadAsync(string name, string[] content)
        {
            Loading?.Invoke();
            File file = new();
            Current.Add(name, file);
            await Task.Run(() => { foreach (string line in content) AddLine(file, line); });
            Loaded?.Invoke();
        }
        /// <summary>
        /// 同步读取指定中间文件
        /// <br/>感觉会卡，推荐使用<see cref="LoadAsync"/>
        /// </summary>
        /// <param name="name">文件名（并非路径，只是用于标识）</param>
        /// <param name="stream">目标文件流</param>
        /// <param name="encoding">编码方式</param>
        public static void Load(string name, Stream stream, Encoding encoding)
        {
            Loading?.Invoke();
            File file = new();
            Current.Add(name, file);
            using (StreamReader sr = new StreamReader(stream, encoding))
            {
                string? line;
                while ((line = sr.ReadLine()) != null) AddLine(file, line);
            }
            Loaded?.Invoke();
        }
        /// <summary>
        /// 异步读取指定中间文件
        /// <param name="name">文件名（并非路径，只是用于标识）</param>
        /// <param name="content">文本内容</param>
        /// </summary>
        public static void Load(string name, string[] content)
        {
            Loading?.Invoke();
            File file = new();
            Current.Add(name, file);
            foreach (string line in content) AddLine(file, line);
            Loaded?.Invoke();
        }
    }
}
