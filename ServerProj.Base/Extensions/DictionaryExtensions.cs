using ServerProj.Base.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ServerProj.Base.Extensions
{
    /// <summary>
    /// DictionaryExtensions
    /// </summary>
    public static class DictionaryExtensions
    {
        #region IDictionary
        /// <summary>
        /// 哈希是否有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static bool HasData<T, V>(this IDictionary<T, V> dic)
        {
            if (dic != null && dic.Count > 0) return true;
            return false;
        }

        /// <summary>
        /// Values
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="that"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<TValue> Values<TKey, TValue>(this IDictionary<TKey, TValue> that, Func<KeyValuePair<TKey, TValue>, bool> func = null)
        {
            if (that == null)
                return null;

            return func != null ? that.Where(func).Select(o => o.Value).ToList() : that.Select(o => o.Value).ToList();
        }

        /// <summary>
        /// Keys
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="that"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<TKey> Keys<TKey, TValue>(this IDictionary<TKey, TValue> that, Func<KeyValuePair<TKey, TValue>, bool> func = null)
        {
            if (that == null)
                return null;

            return func != null ? that.Where(func).Select(o => o.Key).ToList() : that.Select(o => o.Key).ToList();
        }

        /// <summary>
        /// ToDictionary
        /// </summary>
        /// <param name="that"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IDictionary<TKey, TValue> that, Func<KeyValuePair<TKey, TValue>, bool> func = null)
        {
            if (that == null)
                return null;

            return func != null ? that.Where(func).ToDictionary(o => o.Key, k => k.Value) : that.ToDictionary(o => o.Key, k => k.Value);
        }

        #endregion

        #region Dictionary

        /// <summary>
        /// Keys
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="that"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Set<TKey, TValue>(this Dictionary<TKey, TValue> that, TKey key, TValue value)
        {
            if (that == null || key == null)
                return false;

            if (that.ContainsKey(key))
                that[key] = value;
            else
                that.Add(key, value);
            return true;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="that"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> that, TKey key)
        {
            return that.ContainsKey(key) ? that[key] : default(TValue);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="that"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TResult Get<TKey, TValue, TResult>(this Dictionary<TKey, TValue> that, TKey key)
        {
            return CommonTools.Parse<TResult>(that.Get(key));
        }

        /// <summary>
        /// AddRange
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="that"></param>
        /// <param name="dic"></param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> that, IDictionary<TKey, TValue> dic)
        {
            if (!dic.HasData())
                return that;

            foreach (var pair in dic)
            {
                that.Set(pair.Key, pair.Value);
            }
            return that;
        }

        /// <summary>
        /// Set
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <param name="that"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Set<TBase>(this Dictionary<string, TBase> that, TBase value)
        {
            var key = typeof(TBase).ToString();
            return that.Set(key, value);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <param name="that"></param>
        /// <returns></returns>
        public static TBase Get<TBase>(this Dictionary<string, TBase> that)
        {
            var key = typeof(TBase).ToString();
            return that.Get(key);
        }

        /// <summary>
        /// Set
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <typeparam name="TSub"></typeparam>
        /// <param name="that"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Set<TBase, TSub>(this Dictionary<string, TBase> that, TSub value)
            where TSub : TBase
        {
            var key = typeof(TSub).ToString();
            return that.Set(key, value);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <typeparam name="TSub"></typeparam>
        /// <param name="that"></param>
        /// <returns></returns>
        public static TSub Get<TBase, TSub>(this Dictionary<string, TBase> that)
            where TSub : TBase
        {
            var key = typeof(TSub).ToString();
            return (TSub)that.Get(key);
        }

        /// <summary>
        /// GetOrAdd
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="that"></param>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> that, TKey key, Func<TKey, TValue> func = null)
        {
            TValue value;
            if (that.ContainsKey(key))
            {
                value = that[key];
            }
            else if (func != null)
            {
                value = func(key);
                that.Set(key, value);
            }
            else
            {
                value = default(TValue);
            }
            return value;
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="that"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Remove<TKey, TValue>(this Dictionary<TKey, TValue> that, TKey key, out TValue value)
        {
            value = that.Get(key);
            return that.Remove(key);
        }

        #endregion

        #region ConcurrentDictionary

        /// <summary>
        /// Set
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="that"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Set<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> that, TKey key, TValue value)
        {
            if (that == null || key == null)
                return false;

            that.AddOrUpdate(key, value, (k, v) => value);
            return true;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="that"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> that, TKey key)
        {
            TValue value;
            that.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="that"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TResult Get<TKey, TValue, TResult>(this ConcurrentDictionary<TKey, TValue> that, TKey key)
        {
            return CommonTools.Parse<TResult>(that.Get(key));
        }

        /// <summary>
        /// AddRange
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="that"></param>
        /// <param name="dic"></param>
        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> that, IDictionary<TKey, TValue> dic)
        {
            if (!dic.HasData())
                return that;

            foreach (var pair in dic)
            {
                that.Set(pair.Key, pair.Value);
            }
            return that;
        }

        #endregion
    }
}
