using MewsiferConsole.Common;
using Newtonsoft.Json;
using static MewsiferConsole.Common.PipeContract;

namespace MewsiferConsole
{
  public delegate void HandleLogEvent(LogEvent evt);
  public delegate void HandleCommand(ClientToServerCommand cmd);

  public delegate void HandleRawMessage(string msg);
  public delegate void TitleChanged(string title);

  public class MessageSource
  {
    private IRawMessageSource? _Source;
    public IRawMessageSource? Source
    {
      get => _Source;
      set
      {
        if (_Source != null)
        {
          _Source.TitleChanged -= OnSourceTitleChanged;
          _Source.RawMessage -= OnRawMessageReceived;
          _Source.Completed -= OnSourceCompleted;

        }
        _Source = value;
        if (_Source != null)
        {
          _Source.TitleChanged += OnSourceTitleChanged;
          _Source.RawMessage += OnRawMessageReceived;
          _Source.Completed += OnSourceCompleted;
        }

      }
    }

    public bool IsBounded => _Source?.IsBounded ?? false;

    /// <summary>
    /// Raised when a <see cref="IsBounded"/> source has raised events for all its messages
    /// </summary>
    public event Action? Completed;

    /// <summary>
    /// Raised when a new <see cref="LogEvent"/> is ready
    /// </summary>
    public event HandleLogEvent? LogEvent;

    /// <summary>
    /// Raised when a new <see cref="ClientToServerCommand>"/> is ready
    /// </summary>
    public event HandleCommand? Command;

    /// <summary>
    /// Raised when the title of this source changes
    /// </summary>
    public event TitleChanged? TitleChanged;

    private void OnSourceTitleChanged(string title)
    {
      TitleChanged?.Invoke(title);
    }

    private void OnSourceCompleted() => Completed?.Invoke();

    private void OnRawMessageReceived(string raw)
    {
      var obj = JsonConvert.DeserializeObject<PipeMessage>(raw);
      if (obj != null)
      {
        if (obj.LogEvent != null)
          LogEvent?.Invoke(obj.LogEvent);
        else if (obj.ServerCommand != null)
          Command?.Invoke(obj.ServerCommand);
      }
      else
      {
        Console.Error.WriteLine("Message is nonsense: " + raw);
      }

    }
    /// <summary>
    /// Enter a busy loop consuming all messages from the raw source
    /// </summary>
    public void Start()
    {
      if (_Source == null)
      {
        throw new Exception("Source has not yet been set");
      }

      Task.Run(() =>
      {
        _Source.Start();
      });
    }
  }

  /// <summary>
  /// Source of raw messages, can't just be an <see cref="IEnumerable{String}"/> since they can't handle exceptions/reconnects nicely
  /// </summary>
  public interface IRawMessageSource
  {
    /// <summary>
    /// Raised when a new <see cref="ClientToServerCommand>"/> is ready
    /// </summary>
    public event HandleRawMessage RawMessage;

    public event TitleChanged TitleChanged;

    public event Action Completed;

    public bool IsBounded { get; }

    /// <summary>
    /// Enter a busy loop consuming all messages
    /// </summary>
    public void Start();
  }

  public class MessagesFromFile : IRawMessageSource
  {
    public event HandleRawMessage? RawMessage;
    public event TitleChanged? TitleChanged;
    public event Action? Completed;

    private readonly string path;
    public bool IsBounded => true;

    public void Start()
    {
      TitleChanged?.Invoke("local file - " + path);

      var decompressedFile = MewFile.Read(path);
      foreach (var line in File.ReadLines(decompressedFile))
      {
        RawMessage?.Invoke(line);
      }

      Completed?.Invoke();
    }

    public MessagesFromFile(string path)
    {
      this.path = path;
    }
  }
}
