using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace MewsiferConsole
{
  /// <summary>
  /// Implementation of an <see cref="IBindingListView"/> for <see cref="LogEventViewModel"/>
  /// Supports filtering.
  /// </summary>
  internal class LogMessageFilterView : IBindingListView, ITypedList
  {
    /// <summary>
    /// Underlying model containing all log events, unfiltered
    /// </summary>
    private readonly BindingList<LogEventViewModel> ViewModel;

    /// <summary>
    /// The set of events from the underlying <see cref="ViewModel"/> that are currently filtered-in, by index
    /// </summary>
    private readonly List<int> Remap = new();

    /// <summary>
    /// Mapping from an event in the underlying <see cref="ViewModel"/> -> index in <see cref="Remap"/>
    /// </summary>
    private readonly Dictionary<int, int> Reverse = new();

    /// <summary>
    /// Type information (for <see cref="ITypedList"/>) so that a <see cref="DataGridView"/> can name its columns and pull out properties of events
    /// </summary>
    private readonly PropertyDescriptorCollection Properties;

    /// <summary>
    /// Apply the current filter to an event, raise a change notification if requested and the event was filtered-in.
    /// </summary>
    /// <param name="index">index in <see cref="ViewModel"/> of the event</param>
    /// <param name="raiseEvent">if true, and the event was filtered-in, raise a <see cref="ListChangedType.ItemAdded"> change notification/></param>
    private void TryFilter(int index, bool raiseEvent)
    {
      LogEventViewModel model = ViewModel[index];

      bool channelMatch = currentFilter.MatchChannel(model.ChannelName);
      bool rawMatch = currentFilter.MatchRaw(model.Message);

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

    /// <summary>
    /// Create a new filter view
    /// </summary>
    /// <param name="underlying">The underlying model to filter</param>
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

    private FilterModel currentFilter = new();
    private HashSet<string> rawTerms = new();

    public string? Filter
    {
      get => currentFilter.Render;
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

    /// <summary>
    /// Apply the given filter to all elements in the underlying <see cref="ViewModel"/>
    /// If the filter is the same as the currently applied filter it is not re-applied.
    /// </summary>
    /// <param name="text">Textual representation of the filter</param>
    private void ApplyFilter(string text)
    {
      text = text.Trim();

      var components = Regex.Split(text, @"\s+");

      FilterModel newFilter = new();

      foreach (var c in components)
      {
        if (c.Contains(':'))
        {
          var kv = c.Split(':', 2);
          if (kv.Length == 2 && kv[0] is "ch" or "channel" or "c")
          {
            newFilter.channel = kv[1];
          }
          else
          {
            newFilter.rawTerms.Add(c);
          }
        }
        else
        {
          newFilter.rawTerms.Add(c);
        }
      }

      if (newFilter == currentFilter)
      {
        return;
      }

      currentFilter = newFilter;

      Remap.Clear();
      Reverse.Clear();

      for (int i = 0; i < ViewModel.Count; i++)
      {
        TryFilter(i, false);
      }

      ListChanged?.Invoke(this, new(ListChangedType.Reset, -1));
    }
  }

  public class FilterModel
  {
    public HashSet<string> rawTerms = new();
    public string channel = "";

    private IEnumerable<string> RenderComponents
    {
      get
      {
        if (channel.Length > 0)
          yield return $"ch:{channel}";

        foreach (var term in rawTerms)
          yield return term;
      }
    }
    public string Render => String.Join(" ", RenderComponents);

    internal bool MatchChannel(string channelName)
    {
        return channel.Length == 0 || channelName.Contains(channel, StringComparison.OrdinalIgnoreCase);
    }

    internal bool MatchRaw(string message)
    {
        return rawTerms.Count == 0 || rawTerms.Any(term => message.Contains(term, StringComparison.OrdinalIgnoreCase));
    }

    public override bool Equals(object? obj)
    {
      return obj is FilterModel model &&
             EqualityComparer<HashSet<string>>.Default.Equals(rawTerms, model.rawTerms) &&
             channel == model.channel;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(rawTerms, channel);
    }


    public static bool operator ==(FilterModel lhs, FilterModel rhs) => lhs.Equals(rhs);
    public static bool operator !=(FilterModel lhs, FilterModel rhs) => !(lhs == rhs);
  }
}
