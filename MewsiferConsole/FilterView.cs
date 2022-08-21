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

      if (currentFilter.Matches(model))
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
      ViewModel = underlying;
      ViewModel.ListChanged += (obj, evt) =>
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
      ViewModel.Clear();
      ApplyFilter("");
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
      ListChanged?.Invoke(this, new(ListChangedType.Reset, -1));
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
        newFilter.Add(c);
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
    public MatchTermList messageTerms = new();

    public MatchTermList sevTerms = new()
    {
      CombinePositiveWith = TermCombiner.Or
    };

    public MatchTermList channelTerms = new()
    {
      CombinePositiveWith = TermCombiner.Or
    };

    private IEnumerable<string> RenderComponents
    {
      get
      {
        foreach (var rendered in sevTerms.RenderComponents("sev:"))
          yield return rendered;

        foreach (var rendered in channelTerms.RenderComponents("ch:"))
          yield return rendered;

        foreach (var rendered in messageTerms.RenderComponents(""))
          yield return rendered;
      }
    }
    public string Render => String.Join(" ", RenderComponents);

    internal bool MatchChannel(string channelName)
    {
      return channelTerms.Matches(channelName);
    }

    internal bool MatchRaw(string message)
    {
      return messageTerms.Matches(message);
    }
    internal bool MatchSev(string sev)
    {
      return sevTerms.Matches(sev);
    }

    internal void Add(string c)
    {
      if (c.Length == 0) return;

      bool addedSpecial = false;
      if (c.Contains(':'))
      {
        var kv = c.Split(':', 2);
        if (kv.Length == 2 && kv[0] is "ch" or "-ch")
        {
          channelTerms.Add(kv[0][0] is '-', kv[1]);
          addedSpecial = true;
        }
        else if (kv.Length == 2 && kv[0] is "sev" or "-sev")
        {
          sevTerms.Add(kv[0][0] is '-', kv[1]);
          addedSpecial = true;
        }
      }

      if (!addedSpecial)
      {
        if (c[0] is '-')
          messageTerms.Add(true, c[1..]);
        else
          messageTerms.Add(false, c);
      }
    }

    public override bool Equals(object? obj)
    {
      return obj is FilterModel model &&
             EqualityComparer<MatchTermList>.Default.Equals(messageTerms, model.messageTerms) &&
             EqualityComparer<MatchTermList>.Default.Equals(sevTerms, model.sevTerms) &&
             EqualityComparer<MatchTermList>.Default.Equals(channelTerms, model.channelTerms);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(messageTerms, channelTerms, sevTerms);
    }

    internal bool Matches(LogEventViewModel model)
    {
      return MatchChannel(model.ChannelName) && MatchRaw(model.MessageText) && MatchSev(model.Severity);
    }

    public static bool operator ==(FilterModel lhs, FilterModel rhs) => lhs.Equals(rhs);
    public static bool operator !=(FilterModel lhs, FilterModel rhs) => !(lhs == rhs);
  }

  public enum TermCombiner
  {
    Or,
    And,
  }

  public class MatchTermList
  {
    private readonly List<MatchTerm> terms = new();
    public TermCombiner CombinePositiveWith = TermCombiner.And;
    public TermCombiner CombineNegativeWith = TermCombiner.And;

    public void Add(bool negate, string raw)
    {
      if (raw.Length == 0) return;

      terms.Add(new()
      {
        negated = negate,
        value = new(raw),
      });

    }

    public override bool Equals(object? obj)
    {
      return obj is MatchTermList list &&
             EqualityComparer<List<MatchTerm>>.Default.Equals(terms, list.terms);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(terms);
    }

    public bool Matches(string input)
    {
      if (terms.Count == 0) return true;

      bool ret = terms[0].Matches(input);

      foreach (var term in terms.Skip(1))
      {
        TermCombiner combine = term.negated ? CombineNegativeWith : CombinePositiveWith;

        if (combine == TermCombiner.Or)
          ret = ret || term.Matches(input);
        else if (combine == TermCombiner.And)
          ret = ret && term.Matches(input);
      }

      return ret;
    }

    internal IEnumerable<string> RenderComponents(string prefix)
    {
      foreach (var t in terms)
        yield return $"{t.RenderPre}{prefix}{t.value}";
    }
  }

  public class MatchTerm
  {
    public string value = ""; //regex??
    public bool negated = false;

    public string RenderPre => negated ? "-" : "";

    public override bool Equals(object? obj)
    {
      return obj is MatchTerm term &&
             value == term.value &&
             negated == term.negated;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(value, negated);
    }

    public bool Matches(string input)
    {
      return input.Contains(value, StringComparison.OrdinalIgnoreCase) != negated;
    }

  }
}
