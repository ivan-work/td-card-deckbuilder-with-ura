using System;
using System.Collections.Generic;
using Architecture;
using ObservableCollections;
using UnityEngine;

namespace UnitSelection {
  public class SelectionManager : Singleton<SelectionManager> {
    private readonly ObservableHashSet<SelectableComponent> selection = new();

    public event EventHandler<IEnumerable<SelectableComponent>>? SelectionChanged;

    public SelectionManager() {
      NotifyCollectionChanged += OnNotifyCollectionChanged;
    }

    public event NotifyCollectionChangedEventHandler<SelectableComponent> NotifyCollectionChanged {
      add => selection.CollectionChanged += value;
      remove => selection.CollectionChanged -= value;
    }

    private void OnNotifyCollectionChanged(in NotifyCollectionChangedEventArgs<SelectableComponent> e) {
      SelectionChanged?.Invoke(this, selection);
    }

    public void Select(SelectableComponent selectableComponent) {
      selection.Add(selectableComponent);
    }

    private void Update() {
      if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift)) {
        selection.Clear();
      }
    }
  }
}
