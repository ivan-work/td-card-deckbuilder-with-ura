using System;
using System.Collections.Generic;
using Architecture;
using ObservableCollections;
using UnityEngine;
using UnityEngine.Events;

namespace UnitSelection {
  public class SelectionManager : Singleton<SelectionManager> {
    private readonly ObservableHashSet<SelectableComponent> selection = new();

    public UnityEvent<IEnumerable<SelectableComponent>> SelectionChanged = new();

    public SelectionManager() {
      selection.CollectionChanged += onNotifyCollectionChanged;
    }

    private void onNotifyCollectionChanged(in NotifyCollectionChangedEventArgs<SelectableComponent> e) {
      SelectionChanged.Invoke(selection);
    }

    public void Select(SelectableComponent selectableComponent) {
      selection.Add(selectableComponent);
    }

    private void Update() {
      if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftShift)) {
        selection.Clear();
      }
    }
  }
}
