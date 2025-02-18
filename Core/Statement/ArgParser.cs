/// <summary>
/// 参数解析工具函数
/// </summary>

namespace StoryTable
{
    public struct ArgParser
    {
        internal readonly string[] currentArgs;
        internal int currentIndex;

        internal ArgParser(string[] args)
        {
            currentArgs = args;
            currentIndex = 0;
        }

        public int Int(bool nullable = false)
        {
            var idx = currentIndex;
            currentIndex++;

            if (currentArgs.Length <= idx)
                if (!nullable) return Err($"参数不足！数量: {currentArgs.Length}, 应当至少有 {currentIndex} 个");
                else return 0;


            if (int.TryParse(currentArgs[idx], out int result)) return result;
            else return Err($"解析失败！参数 {currentArgs[idx]} 不是整数");
        }

        public float Float(bool nullable = false)
        {
            var idx = currentIndex;
            currentIndex++;

            if (currentArgs.Length <= idx)
                if (!nullable) return Err($"参数不足！数量: {currentArgs.Length}, 应当至少有 {currentIndex} 个");
                else return 0;


            if (float.TryParse(currentArgs[idx], out float result)) return result;
            else return Err($"解析失败！参数 {currentArgs[idx]} 不是整数");
        }

        public string String(bool nullable = false)
        {
            var idx = currentIndex;
            currentIndex++;

            if (currentArgs.Length <= idx)
                if (!nullable) return Err($"参数不足！数量: {currentArgs.Length}, 应当至少有 {currentIndex} 个");
                else return string.Empty;

            return currentArgs[idx];
        }

        public T Enum<T>(bool nullable = false) where T : struct
        {
            var idx = currentIndex;
            currentIndex++;

            if (currentArgs.Length <= idx)
                if (!nullable) return Err($"参数不足！数量: {currentArgs.Length}, 应当至少有 {currentIndex} 个");
                else return default;

            // To PascalCase
            var key = currentArgs[idx].ToLower();
            key = $"{char.ToUpper(key[0])}{key[1..]}";

            if (System.Enum.TryParse(key, out T result)) return result;
            else
            {
                var sb = new System.Text.StringBuilder($"解析失败！参数 {currentArgs[idx]} 不是 ");
                foreach (T e in System.Enum.GetValues(typeof(T))) sb.Append($"{e}, ");
                sb.Remove(sb.Length - 2, 2);
                sb.Append(" 中的任意一项");
                return Err(sb.ToString());
            }
        }

        public string Raw() => currentArgs[currentIndex++];

        // 这里 dynamic 是为了回避显式类型转换
        public readonly dynamic Err(string message)
            => throw new ArgumentException(message);
    }
}