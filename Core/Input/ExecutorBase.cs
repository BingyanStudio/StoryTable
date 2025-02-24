using System;
using System.Collections.Generic;

namespace StoryTable
{
    public abstract class ExecutorBase
    {
        public Locator Position { get; protected set; }
        public abstract void Execute();

        /// <summary>
        /// 用于通知Executor语句已执行完毕
        /// </summary>
        public virtual void Complete() { }

        /// <summary>
        /// 正在处理语句中
        /// </summary>
        public virtual bool Processing { get; protected set; }
        /// <summary>
        /// 立刻执行完当前语句
        /// </summary>
        public virtual bool Skip { get; protected set; }
        /// <summary>
        /// 暂停等待用户输入
        /// </summary>
        public virtual bool Pause { get; protected set; }

        /// <summary>
        /// 开始处理文件
        /// </summary>
        public event Action OnFileProcessing;
        protected void FileProcessing() => OnFileProcessing?.Invoke();
        /// <summary>
        /// 文件处理完毕
        /// </summary>
        public event Action OnFileProcessed;
        protected void FileProcessed() => OnFileProcessed?.Invoke();
        /// <summary>
        /// 开始执行一系列语句
        /// </summary>
        public event Action OnExecuting;
        protected void Executing() => OnExecuting?.Invoke();
        /// <summary>
        /// 一系列语句执行完毕
        /// </summary>
        public event Action OnExecuted;
        protected void Executed() => OnExecuted?.Invoke();
        /// <summary>
        /// 开始处理一行语句
        /// </summary>
        public event Action<Locator> OnLineProcessing;
        protected void LineProcessing(Locator locator) => OnLineProcessing?.Invoke(locator);
        /// <summary>
        /// 一行语句处理完毕
        /// </summary>
        public event Action<Locator> OnLineProcessed;
        protected void LineProcessed(Locator locator) => OnLineProcessed?.Invoke(locator);

        /// <summary>
        /// 执行了End语句
        /// </summary>
        public event Action<string> End;
        internal void EndWith(string value) => End?.Invoke(value);

        public Line CurrentLine => CurrentFile[Position.LineIndex];
        public File CurrentFile =>
            IntermediateFile.Current.ContainsKey(Position.FileName) || Provider.File.Find(Position.FileName) ?
            IntermediateFile.Current[Position.FileName] : throw new KeyNotFoundException(Position.FileName);

        /// <summary>
        /// 定位到指定行数
        /// </summary>
        /// <param name="lineIndex">目标行数</param>
        public void Locate(int lineIndex)
            => Position = new Locator(Position.FileName, lineIndex);
        /// <summary>
        /// 定位到指定文件首行
        /// </summary>
        /// <param name="fileName">指定文件</param>
        public void Locate(string fileName)
            => Position = new Locator(fileName, 1);
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
    }
}
