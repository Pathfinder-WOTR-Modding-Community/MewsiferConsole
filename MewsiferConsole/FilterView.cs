using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace MewsiferConsole
{
  internal class LogMessageFilterView : IBindingListView, ITypedList
  {
    private readonly BindingList<LogEventViewModel> ViewModel;
    private readonly List<int> Remap = new();
    private readonly Dictionary<int, int> Reverse = new();
    private readonly PropertyDescriptorCollection Properties;

    private void TryFilter(int index, bool raiseEvent)
    {
      LogEventViewModel model = ViewModel[index];

      bool channelMatch =
        filterChannel.Length == 0 || model.ChannelName.Contains(filterChannel, StringComparison.OrdinalIgnoreCase);
      bool rawMatch =
        rawTerms.Count == 0 || rawTerms.Any(term => model.Message.Contains(term, StringComparison.OrdinalIgnoreCase));

      if (rawMatch && channelMatch)
      {
        int remappedIndex = Remap.Count;
        Remap.Add(index);
        Reverse[index] = remappedIndex;

        if (raiseEvent)
        {
          ListChanged?.Invoke(this, new(ListChangedType.ItemAdded, remappedIndex));
        }
      }
    }

    public LogMessageFilterView(BindingList<LogEventViewModel> underlying)
    {
      this.ViewModel = underlying;
      this.ViewModel.ListChanged += (obj, evt) =>
      {
        if (evt.ListChangedType == ListChangedType.ItemAdded)
        {
          TryFilter(evt.NewIndex, true);
        }
        else if (
          evt.ListChangedType == ListChangedType.PropertyDescriptorChanged
          || evt.ListChangedType == ListChangedType.PropertyDescriptorAdded)
        {
        }
        else if (evt.ListChangedType == ListChangedType.ItemChanged)
        {
          if (Reverse.TryGetValue(evt.NewIndex, out var index))
          {
            ListChanged?.Invoke(this, new(ListChangedType.ItemChanged, index));
          }
        }
        else if (evt.ListChangedType == ListChangedType.Reset)
        {
        }
      };

      // Get the 'shape' of the list.
      // Only get the public properties marked with Browsable = true.
      PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(
        typeof(LogEventViewModel),
        new Attribute[] { new BrowsableAttribute(true) });

      // Sort the properties.
      Properties = pdc.Sort();
    }

    public object? this[int index]
    {
      get
      {
        var obj = ViewModel[Remap[index]];
        return obj;
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    private string _Filter = "";
    private string filterChannel = "";
    private HashSet<string> rawTerms = new();

    public string? Filter
    {
      get => _Filter;
      set
      {
        ApplyFilter(value ?? "");
      }
    }

    public ListSortDescriptionCollection SortDescriptions => throw new NotSupportedException();

    public bool SupportsAdvancedSorting => false;
    public bool SupportsFiltering => true;
    public bool AllowEdit => false;
    public bool AllowNew => false;
    public bool AllowRemove => false;
    public bool IsSorted => false;

    public ListSortDirection SortDirection => ListSortDirection.Descending;
    public PropertyDescriptor? SortProperty => null;

    public bool SupportsChangeNotification => true;
    public bool SupportsSearching => false;
    public bool SupportsSorting => false;
    public bool IsFixedSize => false;
    public bool IsReadOnly => true;
    public int Count => Remap.Count;

    public bool IsSynchronized => throw new NotImplementedException();
    public object SyncRoot => throw new NotImplementedException();

    public event ListChangedEventHandler? ListChanged;

    public int Add(object? value)
    {
      throw new NotImplementedException();
    }

    public void AddIndex(PropertyDescriptor property)
    {
      throw new NotImplementedException();
    }

    public object? AddNew()
    {
      throw new NotImplementedException();
    }

    public void ApplySort(ListSortDescriptionCollection sorts)
    {
      throw new NotImplementedException();
    }

    public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
    {
      throw new NotImplementedException();
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public bool Contains(object? value)
    {
      throw new NotImplementedException();
    }

    public void CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }

    public int Find(PropertyDescriptor property, object key)
    {
      throw new NotImplementedException();
    }

    public IEnumerator GetEnumerator()
    {
      throw new NotSupportedException();
    }

    public int IndexOf(object? value)
    {
      throw new NotSupportedException();
    }

    public void Insert(int index, object? value)
    {
      throw new NotImplementedException();
    }

    public void Remove(object? value)
    {
      throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    public void RemoveFilter()
    {
      Filter = "";
    }

    public void RemoveIndex(PropertyDescriptor property)
    {
      throw new NotImplementedException();
    }

    public void RemoveSort()
    {
      throw new NotImplementedException();
    }

    public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
    {
      PropertyDescriptorCollection pdc;

      if (listAccessors != null && listAccessors.Length > 0)
      {
        // Return child list shape.
        pdc = ListBindingHelper.GetListItemProperties(listAccessors[0].PropertyType);
      }
      else
      {
        // Return properties in sort order.
        pdc = Properties;
      }

      return pdc;
    }

    public string GetListName(PropertyDescriptor[] listAccessors)
    {
      return typeof(LogEventViewModel).Name;
    }

    private void ApplyFilter(string text)
    {
      text = text.Trim();
      if (text == _Filter) { return; }

      var components = Regex.Split(text, @"\s+");

      HashSet<string> newTerms = new();
      string newFilterChannel = "";

      foreach (var c in components)
      {
        if (c.Contains(':'))
        {
          var kv = c.Split(':', 2);
          if (kv.Length == 2 && kv[0] is "ch" or "channel" or "c")
          {
            newFilterChannel = kv[1];
          }
          else
          {
            newTerms.Add(c);
          }
        }
        else
        {
          newTerms.Add(c);
        }
      }

      if (filterChannel == newFilterChannel && rawTerms.SetEquals(newTerms))
      {
        return;
      }

      filterChannel = newFilterChannel;
      rawTerms = newTerms;

      Remap.Clear();
      Reverse.Clear();
      _Filter = text;

      for (int i = 0; i < ViewModel.Count; i++)
      {
        TryFilter(i, false);
      }

      ListChanged?.Invoke(this, new(ListChangedType.Reset, -1));
    }
  }

  internal class FilterTerm
  {
    public readonly string Key;
    public readonly string Value;

    public FilterTerm(string key, string value)
    {
      Key = key;
      Value = value;
    }
  }
}
