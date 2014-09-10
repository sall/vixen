﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vixen.Data.Flow;
using Vixen.Sys.Instrumentation;

namespace Vixen.Sys.Managers
{
	public class ElementManager : IEnumerable<Element>
	{
		private MillisecondsValue _elementUpdateTimeValue = new MillisecondsValue("   Elements update");
		private Stopwatch _stopwatch = Stopwatch.StartNew();
		private ElementDataFlowAdapterFactory _dataFlowAdapters;
		private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();

		// a mapping of element  GUIDs to element instances. Used for quick reverse mapping at runtime.
		//This was a ConcurrentDictionary for a while, but grabing an instance enumerator can be costly as it makes a read only copy
		//General locking on a Dictionary is sufficient here and more performant for iterating which is the highest traffic
		private readonly Dictionary<Guid, Element> _instances;

		// a mapping of elements back to their containing element nodes. Used in a few special cases, particularly for runtime, so we can
		// quickly and easily find the node that a particular element references (eg. if we're previewing the rendered data on a virtual display,
		// or anything else where we need to actually 'reverse' the rendering process).
		private readonly ConcurrentDictionary<Element, ElementNode> _elementToElementNode;

		public ElementManager()
		{
			_instances = new Dictionary<Guid, Element>();
			_elementToElementNode = new ConcurrentDictionary<Element, ElementNode>();
			_dataFlowAdapters = new ElementDataFlowAdapterFactory();

			VixenSystem.Instrumentation.AddValue(_elementUpdateTimeValue);
		}

		public ElementManager(IEnumerable<Element> elements)
			: this()
		{
			AddElements(elements);
		}

		public Element AddElement(string elementName)
		{
			elementName = _Uniquify(elementName);
			Element element = new Element(elementName);
			AddElement(element);
			return element;
		}

		public void AddElement(Element element)
		{
			if (element != null) {
				if (_instances.ContainsKey(element.Id))
					Logging.Error("ElementManager: Adding a element, but it's already in the instance map!");

				lock (_instances) {
					_instances[element.Id] = element;
				}

				_AddDataFlowParticipant(element);
			}
		}

		public void AddElements(IEnumerable<Element> elements)
		{
			foreach (Element element in elements) {
				AddElement(element);
			}
		}

		public void RemoveElement(Element element)
		{
			Element e;
			ElementNode en;
			lock (_instances)
			{
				_instances.Remove(element.Id);
			}

			_RemoveDataFlowParticipant(element);
			_elementToElementNode.TryRemove(element, out en);
			
		}

		public Element GetElement(Guid id)
		{
			Element element;
			_instances.TryGetValue(id, out element);
			return element;
		}

		public bool SetElementNodeForElement(Element element, ElementNode node)
		{
			if (element == null)
				return false;

			bool rv = _elementToElementNode.ContainsKey(element);

			_elementToElementNode[element] = node;
			return rv;
		}

		public ElementNode GetElementNodeForElement(Element element)
		{
			if (element == null)
				return null;

			ElementNode node;
			_elementToElementNode.TryGetValue(element, out node);
			return node;
		}

		public void Update()
		{
			_stopwatch.Restart();
			lock (_instances)
			{
				foreach (var x in _instances.Values) x.Update();				
			}
			_elementUpdateTimeValue.Set(_stopwatch.ElapsedMilliseconds);
		}

		/// <summary>
		/// This performs updates on the predicted elements and arbitrarily clears the rest.
		/// Saves the labor of doing Dictionary lookups in every context when we know it was not affected.
		/// </summary>
		/// <param name="elements"></param>
		public void Update(HashSet<Guid> elements)
		{
			_stopwatch.Restart();
			lock (_instances)
			{
				foreach (var x in _instances.Values)
				{
					if (elements.Contains(x.Id))
					{
						x.Update();
					} else
					{
						//No sense in checking each context as we know our element is not affected on this update.
						x.ClearStates();
					}
				}
			}
			_elementUpdateTimeValue.Set(_stopwatch.ElapsedMilliseconds);
		}

		public void ClearStates()
		{
			lock (_instances)
			{
				foreach (var x in _instances.Values) x.ClearStates();	
			}
		}

		private void _AddDataFlowParticipant(Element element)
		{
			VixenSystem.DataFlow.AddComponent(_dataFlowAdapters.GetAdapter(element));
		}

		private void _RemoveDataFlowParticipant(Element element)
		{
			VixenSystem.DataFlow.RemoveComponent(_dataFlowAdapters.GetAdapter(element));
		}

		public IDataFlowComponent GetDataFlowComponentForElement(Element element)
		{
			return _dataFlowAdapters.GetAdapter(element);
		}

		private string _Uniquify(string name)
		{
			if (_instances.Values.Any(x => x.Name == name)) {
				string originalName = name;
				bool unique;
				int counter = 2;
				do {
					name = string.Format("{0}-{1}", originalName, counter++);
					unique = !_instances.Values.Any(x => x.Name == name);
				} while (!unique);
			}
			return name;
		}

		public IEnumerator<Element> GetEnumerator()
		{
			lock (_instances) {
				Element[] elements = _instances.Values.ToArray();
				return ((IEnumerable<Element>)elements).GetEnumerator();
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}