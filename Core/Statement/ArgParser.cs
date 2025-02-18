
using System.Reflection;

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

        private static readonly Dictionary<Type, Dictionary<string, Enum>> enumCache = new();

        private static Dictionary<string, Enum> BuildEnumCache<T>() where T : Enum
        {
            var map = new Dictionary<string, Enum>();
            foreach (T e in System.Enum.GetValues(typeof(T)))
            {
                map.Add(e.ToString().ToLower(), e);

                var members = e.GetType().GetMember(e.ToString());
                AliasAttribute attr;
                if (members.Length > 0 && (attr = members[0].GetCustomAttribute<AliasAttribute>(true)) != null)
                    foreach (var alia in attr.Alias)
                        if (!map.TryAdd(alia, e))
                            throw new InvalidDataException($"枚举别名 {alia} 被同时分配给了 {e} 和 {map[alia]}");
            }
            enumCache[typeof(T)] = map;
            return map;
        }

        public T Enum<T>() where T : Enum
        {
            if (!enumCache.TryGetValue(typeof(T), out Dictionary<string, Enum> map))
                map = BuildEnumCache<T>();

            if (!Parse(out var arg))
            {
                Err($"参数为空值");
                return default;
            }

            if (map.TryGetValue(arg, out var val)) return (T)val;
            else { Err($"{arg} 应当为 {string.Join(", ", map.Keys)} 中的一项"); return default; }
        }

        public T EnumOr<T>(T defaultValue) where T : Enum
        {
            if (!enumCache.TryGetValue(typeof(T), out Dictionary<string, Enum> map))
                map = BuildEnumCache<T>();

            if (!Parse(out var arg))
            {
                Err($"参数为空值");
                return defaultValue;
            }

            if (map.TryGetValue(arg, out var val)) return (T)val;
            else { Err($"{arg} 应当为 {string.Join(", ", map.Keys)} 中的一项"); return defaultValue; }
        }

        public string Raw() => currentArgs[currentIndex++];

        // 这里 dynamic 是为了回避显式类型转换
        public readonly void Err(string message)
            => throw new ArgumentException(message);
    }
}