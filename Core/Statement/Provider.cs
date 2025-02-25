namespace StoryTable
{
    public partial class Provider
    {
        public IFileProvider File { get; set; }
        public IDataProvider Data { get; set; }
    }
    /// <summary>
    /// 提供寻找文件相关的方法
    /// <br/>通过<see cref="FindFile(string, string)"/>寻找文件
    /// </summary>
    public interface IFileProvider
    {
        bool Find(string name);
    }
    /// <summary>
    /// 提供数据存取相关的方法
    /// <br/>通过<see cref="GetValue{T}(string)"/>读取数据
    /// <br/>通过<see cref="SetInt(string, int)"/>存储数据
    /// </summary>
    public interface IDataProvider
    {
        int GetInt(string key);
        void SetInt(string key, int value);
        string GetString(string key);
        void SetString(string key, string value);
    }
}
