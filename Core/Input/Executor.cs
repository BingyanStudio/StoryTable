using System;
using System.Threading.Tasks;

namespace StoryParser
{
    public class Executor
    {
        public Locator Position { get; private set; }
        private int count;
        /// <summary>
        /// <see cref="Process"/>中的刷新时间，单位是毫秒
        /// </summary>
        public int RefreshTime { get; set; } = 10;
        /// <summary>
        /// 正在处理语句中
        /// </summary>
        public bool Processing => count > 0;
        /// <summary>
        /// 立刻执行完当前语句
        /// </summary>
        public bool Skip { get; private set; }
        /// <summary>
        /// 暂停等待用户输入
        /// </summary>
        public bool Pause { get; set; }
        /// <summary>
        /// 开始处理文件
        /// </summary>
        public event Action? FileProcessing;
        /// <summary>
        /// 文件处理完毕
        /// </summary>
        public event Action? FileProcessed;
        /// <summary>
        /// 执行了End语句
        /// </summary>
        public event Action<string>? End;
        /// <summary>
        /// 开始执行一系列语句
        /// </summary>
        public event Action? Executing;
        /// <summary>
        /// 一系列语句执行完毕
        /// </summary>
        public event Action? Executed;
        /// <summary>
        /// 开始处理一行语句
        /// </summary>
        public event Action<Locator>? LineProcessing;
        /// <summary>
        /// 一行语句处理完毕
        /// </summary>
        public event Action<Locator>? LineProcessed;
        public Line CurrentLine =>
            IntermediateFile.Current[Position.FileName][Position.LineIndex];
        public File CurrentFile =>
            IntermediateFile.Current[Position.FileName];
        /// <summary>
        /// 定位到指定行数
        /// </summary>
        /// <param name="lineIndex">目标行数</param>
        public void Locate(int lineIndex)
            => Position = new Locator(Position.FileName, lineIndex);
        /// <summary>
        /// 定位到指定文件的指定行数
        /// </summary>
        /// <param name="fileName">目标文件名称</param>
        /// <param name="lineIndex">目标行数</param>
        public void Locate(string fileName, int lineIndex)
            => Position = new Locator(fileName, lineIndex);
        /// <summary>
        /// 定位到指定文件的指定行数
        /// </summary>
        /// <param name="line">目标位置</param>
        public void Locate(Locator position) => Position = position;
        private void NextLine()
            => Locate(Position.FileName, Position.LineIndex + 1);
        private void Launch(int count) => this.count = count;
        /// <summary>
        /// 语句执行完毕
        /// </summary>
        public void Complete() => count--;
        internal void EndWith(string value) => End?.Invoke(value);
        public async void Execute()
        {
            if (count > 0)
            {
                Skip = true;
                return;
            }
            if (Position.LineIndex == CurrentFile.Length)
            {
                FileProcessed?.Invoke();
                return;
            }
            if (Position.LineIndex == 0) FileProcessing?.Invoke();
            Executing?.Invoke();
            Pause = Skip = false;
            while (!Pause) await Process();
            Executed?.Invoke();
        }
        private async Task Process()
        {
            LineProcessing?.Invoke(Position);
            Launch(CurrentLine.Length);
            Skip = false;
            CurrentLine.Execute(this);
            while (Processing) await Task.Delay(RefreshTime);
            LineProcessed?.Invoke(Position);
            NextLine();
        }
    }
}
