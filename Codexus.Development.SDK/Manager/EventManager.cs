using System;
using System.Collections.Generic;
using System.Linq;

namespace Codexus.Development.SDK.Manager;
public class EventManager
{
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

	public T TriggerEvent<T>(string channel, T args) where T : IEventArgs
	{
		return TriggerEvent([channel], args);
	}

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

	public void ClearAllHandlers()
	{
		_eventHandlers.Clear();
	}

	public void ClearHandlersForEventType<T>() where T : IEventArgs
	{
		_eventHandlers.Remove(typeof(T));
	}

	public void ClearHandlersForEvent<T>(string channel) where T : IEventArgs
	{
		var flag = !string.IsNullOrEmpty(channel) && _eventHandlers.TryGetValue(typeof(T), out var dictionary) && dictionary.Remove(channel) && dictionary.Count == 0;
		if (flag)
		{
			_eventHandlers.Remove(typeof(T));
		}
	}
	private static EventManager? _instance;
	private readonly Dictionary<Type, Dictionary<string, SortedDictionary<int, List<Delegate>>>> _eventHandlers = new Dictionary<Type, Dictionary<string, SortedDictionary<int, List<Delegate>>>>();
}