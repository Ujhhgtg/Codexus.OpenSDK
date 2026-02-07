using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Codexus.Development.SDK.Manager;

// Token: 0x0200001A RID: 26
public class EventManager
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x0600008E RID: 142 RVA: 0x00003DC0 File Offset: 0x00001FC0
	public static EventManager Instance
	{
		get
		{
			EventManager? eventManager;
			if ((eventManager = _instance) == null)
			{
				eventManager = _instance = new EventManager();
			}
			return eventManager;
		}
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00003DD8 File Offset: 0x00001FD8
	// public void RegisterHandler<T>(string channel, EventHandler<T> handler, int priority = 0) where T : IEventArgs
	// {
	// 	ArgumentNullException.ThrowIfNull(handler);
	// 	var flag = string.IsNullOrEmpty(channel);
	// 	if (flag)
	// 	{
	// 		throw new ArgumentNullException(nameof(channel));
	// 	}
	// 	var typeFromHandle = typeof(T);
	// 	var flag2 = !_eventHandlers.TryGetValue(typeFromHandle, out var dictionary);
	// 	if (flag2)
	// 	{
	// 		dictionary = new Dictionary<string, SortedDictionary<int, List<Delegate>>>();
	// 		_eventHandlers[typeFromHandle] = dictionary;
	// 	}
	//
	// 	var flag3 = !dictionary.TryGetValue(channel, out var sortedDictionary);
	// 	if (flag3)
	// 	{
	// 		int Comparison(int x, int y) => y.CompareTo(x);
	// 		sortedDictionary = dictionary[channel] = new SortedDictionary<int, List<Delegate>>(Comparer<int>.Create(Comparison));
	// 	}
	//
	// 	var flag4 = !sortedDictionary.TryGetValue(priority, out var list);
	// 	if (flag4)
	// 	{
	// 		list = sortedDictionary[priority] = [];
	// 	}
	// 	list.Add(handler);
	// }
	public void RegisterHandler<T>(string channel, EventHandler<T> handler, int priority = 0) where T : IEventArgs
	{
		ArgumentException.ThrowIfNullOrEmpty(channel);
		ArgumentNullException.ThrowIfNull(handler);

		// Get or create the inner dictionary for the Type
		if (!_eventHandlers.TryGetValue(typeof(T), out var channelDict))
		{
			channelDict = new Dictionary<string, SortedDictionary<int, List<Delegate>>>();
			_eventHandlers[typeof(T)] = channelDict;
		}

		// Get or create the priority map for the Channel
		if (!_eventHandlers[typeof(T)].TryGetValue(channel, out var priorityMap))
		{
			// Sorts priorities in descending order (highest int first)
			var descendingComparer = Comparer<int>.Create((x, y) => y.CompareTo(x));
			priorityMap = channelDict[channel] = new SortedDictionary<int, List<Delegate>>(descendingComparer);
		}

		// Get or create the delegate list for the Priority
		if (!priorityMap.TryGetValue(priority, out var handlers))
		{
			handlers = priorityMap[priority] = [];
		}

		handlers.Add(handler);
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00003EC0 File Offset: 0x000020C0
	// public bool UnregisterHandler<T>(string channel,  EventHandler<T>? handler) where T : IEventArgs
	// {
	// 	var flag = string.IsNullOrEmpty(channel) || handler == null || !_eventHandlers.TryGetValue(typeof(T), out var dictionary) || !dictionary.TryGetValue(channel, out var sortedDictionary);
	// 	bool flag2;
	// 	if (!flag)
	// 	{
	// 		foreach (var num in sortedDictionary.Keys.ToList())
	// 		{
	// 			var list = sortedDictionary[num];
	// 			var flag3 = list.Remove(handler);
	// 			if (flag3)
	// 			{
	// 				var flag4 = list.Count != 0;
	// 				if (flag4)
	// 				{
	// 					return true;
	// 				}
	//
	// 				sortedDictionary.Remove(num);
	// 				var flag5 = sortedDictionary.Count != 0;
	// 				if (flag5)
	// 				{
	// 					return true;
	// 				}
	//
	// 				dictionary.Remove(channel);
	// 				var flag6 = dictionary.Count == 0;
	// 				if (flag6)
	// 				{
	// 					_eventHandlers.Remove(typeof(T));
	// 				}
	//
	// 				return true;
	// 			}
	// 		}
	// 	}
	//
	// 	flag2 = false;
	// 	return flag2;
	// }
	public bool UnregisterHandler<T>(string channel, EventHandler<T>? handler) where T : IEventArgs
	{
		if (string.IsNullOrEmpty(channel) || handler == null) return false;

		if (!_eventHandlers.TryGetValue(typeof(T), out var channelDict) || 
		    !channelDict.TryGetValue(channel, out var priorityMap))
		{
			return false;
		}

		foreach (var priority in priorityMap.Keys.ToList())
		{
			var list = priorityMap[priority];
        
			if (list.Remove(handler))
			{
				if (list.Count == 0)
				{
					priorityMap.Remove(priority);
					if (priorityMap.Count == 0)
					{
						channelDict.Remove(channel);
						if (channelDict.Count == 0)
						{
							_eventHandlers.Remove(typeof(T));
						}
					}
				}
				return true;
			}
		}

		return false;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00003FE0 File Offset: 0x000021E0
	public T TriggerEvent<T>(string channel, T args) where T : IEventArgs
	{
		return TriggerEvent([channel], args);
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00004004 File Offset: 0x00002204
	// public T TriggerEvent<T>(string[] channels, T? args) where T : IEventArgs
	// {
	// 	var flag = args == null;
	// 	if (flag)
	// 	{
	// 		throw new ArgumentNullException(nameof(args));
	// 	}
	// 	foreach (var text in channels)
	// 	{
	// 		var flag2 = !_eventHandlers.TryGetValue(typeof(T), out var dictionary) || !dictionary.TryGetValue(text, out var priorityDict);
	// 		if (!flag2)
	// 		{
	// 			IEnumerable<int> keys = priorityDict.Keys;
	// 			Func<int, List<Delegate>> func;
	// 			Func<int, List<Delegate>> func2;
	// 			if ((func = func2) == null)
	// 			{
	// 				func = func2 = (int priority) => priorityDict[priority];
	// 			}
	// 			foreach (var @delegate in keys.Select(func).SelectMany(handlers => handlers.ToList()))
	// 			{
	// 				var eventHandler = (EventHandler<T>)@delegate;
	// 				eventHandler(args);
	// 			}
	// 		}
	// 	}
	// 	return args;
	// }
	public T TriggerEvent<T>(string[] channels, T args) where T : IEventArgs
	{
		ArgumentNullException.ThrowIfNull(args);

		if (!_eventHandlers.TryGetValue(typeof(T), out var channelDict))
			return args;

		foreach (var channel in channels)
		{
			if (channelDict.TryGetValue(channel, out var priorityDict))
			{
				foreach (var handler in priorityDict.Keys
					         .Select(priority => priorityDict[priority].ToList())
					         .SelectMany(handlers => handlers.Cast<EventHandler<T>?>()))
				{
					handler(args);
				}
			}
		}
		return args;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00004134 File Offset: 0x00002334
	// public bool HasHandlersForEvent<T>(string channel) where T : IEventArgs
	// {
	// 	var flag = string.IsNullOrEmpty(channel);
	// 	bool flag2;
	// 	if (flag)
	// 	{
	// 		flag2 = false;
	// 	}
	// 	else
	// 	{
	// 		var flag3 = _eventHandlers.TryGetValue(typeof(T), out var dictionary) && dictionary.TryGetValue(channel, out var sortedDictionary);
	// 		flag2 = flag3 && sortedDictionary.Values.Any(list => list.Count > 0);
	// 	}
	// 	return flag2;
	// }
	public bool HasHandlersForEvent<T>(string channel) where T : IEventArgs
	{
		if (string.IsNullOrEmpty(channel)) return false;

		return _eventHandlers.TryGetValue(typeof(T), out var channelDict) && 
		       channelDict.TryGetValue(channel, out var priorityDict) && 
		       priorityDict.Values.Any(list => list.Count > 0);
	}

	// Token: 0x06000094 RID: 148 RVA: 0x000041AF File Offset: 0x000023AF
	public void ClearAllHandlers()
	{
		_eventHandlers.Clear();
	}

	// Token: 0x06000095 RID: 149 RVA: 0x000041BE File Offset: 0x000023BE
	public void ClearHandlersForEventType<T>() where T : IEventArgs
	{
		_eventHandlers.Remove(typeof(T));
	}

	// Token: 0x06000096 RID: 150 RVA: 0x000041D8 File Offset: 0x000023D8
	public void ClearHandlersForEvent<T>(string channel) where T : IEventArgs
	{
		var flag = !string.IsNullOrEmpty(channel) && _eventHandlers.TryGetValue(typeof(T), out var dictionary) && dictionary.Remove(channel) && dictionary.Count == 0;
		if (flag)
		{
			_eventHandlers.Remove(typeof(T));
		}
	}

	// Token: 0x0400003F RID: 63
	private static EventManager? _instance;

	// Token: 0x04000040 RID: 64
	private readonly Dictionary<Type, Dictionary<string, SortedDictionary<int, List<Delegate>>>> _eventHandlers = new Dictionary<Type, Dictionary<string, SortedDictionary<int, List<Delegate>>>>();
}