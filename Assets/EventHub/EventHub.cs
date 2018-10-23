using UnityEngine;
using System;
using System.Collections.Generic;

public enum EventKey {
	EquipItem,
	UnequipItem,
};

public class EquipItemMsg {
	public Position Position;
	public Item Item;
}

public class EventHub : MonoBehaviour {

	Dictionary<EventKey, Dictionary<string, Func<object, int>>> callbacks;

	public void Broadcast(EventKey eventKey, object msg) {
		if (!this.callbacks.ContainsKey(eventKey)) {
			return;
		}
		foreach(var entry in this.callbacks[eventKey]) {
			var callback = entry.Value;
			callback(msg);
		}
	}

	public void Register(EventKey eventKey, string processorName, Func<object, int> callback) {
		if (!this.callbacks.ContainsKey(eventKey)) {
			this.callbacks[eventKey] = new Dictionary<string, Func<object, int>>();
		}
		this.callbacks[eventKey][processorName] = callback;
	}

	public void Awake() {
		this.callbacks = new Dictionary<EventKey, Dictionary<string, Func<object, int>>>();
	}
}
