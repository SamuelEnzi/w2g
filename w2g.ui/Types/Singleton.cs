namespace w2g.ui.Types
{
    public class Singleton<T> where T : class, new()
    {
        private static T instance;
        public static bool IsInstanced { get => instance != null; }
        public static T Instance
        {
            get
            {
                return instance ??
                    (instance = new T());
            }
        }
    }
}
