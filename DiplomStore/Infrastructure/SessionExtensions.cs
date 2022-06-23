using Newtonsoft.Json;

namespace DiplomStore.Infrastructure
{
    public static class SessionExtensions
    {
        /// <summary>
        /// Добовляем метод к ISession для сохранения объекта в сессии
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetJson(this ISession session, string key, object value)
        {
            ///Сохраняем в сессию, то есть делаем JSON объект из данных и привязываем к ключу
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// Добовляем метод к ISession для получения объекта в сессии
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            ///Получаем данные из сессии или же получаем тот же класс
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
