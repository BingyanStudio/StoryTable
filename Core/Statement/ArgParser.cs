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

        /// <summary>
        /// 获取参数并将 currentIndex 向下推进
        /// </summary>
        /// <param name="arg">获取的参数</param>
        /// <returns>参数是否不为空</returns>
        private bool Parse(out string arg)
        {
            arg = string.Empty;
            currentIndex++;

            if (currentArgs.Length <= currentIndex - 1) return false;
            arg = currentArgs[currentIndex - 1];
            return arg.Length > 0;
        }

        public int Int()
        {
            if (!Parse(out var arg))
            {
                Err($"参数为空值");
                return 0;
            }

            if (int.TryParse(arg, out int result)) return result;
            else { Err($"{arg} 不是整数"); return 0; }
        }

        public int IntOr(int defaultValue)
        {
            if (!Parse(out var arg)) return defaultValue;
            if (int.TryParse(arg, out int result)) return result;
            else { Err($"{arg} 不是整数"); return defaultValue; }
        }

        public float Float()
        {
            if (!Parse(out var arg))
            {
                Err($"参数为空值");
                return 0;
            }

            if (float.TryParse(arg, out float result)) return result;
            else { Err($"{arg} 不是小数"); return 0; }
        }

        public float FloatOr(float defaultValue)
        {
            if (!Parse(out var arg)) return defaultValue;
            if (float.TryParse(arg, out float result)) return result;
            else { Err($"{arg} 不是小数"); return defaultValue; }
        }

        public string String()
        {
            if (!Parse(out var arg))
            {
                Err($"参数为空值");
                return string.Empty;
            }
            return arg;
        }

        public string StringOr(string defaultValue)
        {
            if (!Parse(out var arg)) return defaultValue;
            return arg;
        }

        public T Enum<T>(bool nullable = false) where T : struct
        {
            // TODO: 使用 Attribute + 字典 标记枚举的字符串映射，并用静态字典缓存起来提高映射速度
            return default;
            
            // var idx = currentIndex;
            // currentIndex++;

            // if (currentArgs.Length <= idx)
            //     if (!nullable) return Err($"参数不足！数量: {currentArgs.Length}, 应当至少有 {currentIndex} 个");
            //     else return default;

            // // To PascalCase
            // var key = currentArgs[idx].ToLower();
            // key = $"{char.ToUpper(key[0])}{key[1..]}";

            // if (System.Enum.TryParse(key, out T result)) return result;
            // else
            // {
            //     var sb = new System.Text.StringBuilder($"解析失败！参数 {currentArgs[idx]} 不是 ");
            //     foreach (T e in System.Enum.GetValues(typeof(T))) sb.Append($"{e}, ");
            //     sb.Remove(sb.Length - 2, 2);
            //     sb.Append(" 中的任意一项");
            //     return Err(sb.ToString());
            // }
        }

        public string Raw() => currentArgs[currentIndex++];

        // 这里 dynamic 是为了回避显式类型转换
        public readonly void Err(string message)
            => throw new ArgumentException(message);
    }
}