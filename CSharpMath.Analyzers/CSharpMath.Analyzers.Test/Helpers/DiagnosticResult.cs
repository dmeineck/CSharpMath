using Microsoft.CodeAnalysis;
using System;

namespace TestHelper {
  /// <summary>
  /// Location where the diagnostic appears, as determined by path, line number, and column number.
  /// </summary>
  public struct DiagnosticResultLocation {
    public DiagnosticResultLocation(string path, int line, int column) {
      if (line < -1)
        throw new ArgumentOutOfRangeException(nameof(line), "line must be >= -1");
      if (column < -1)
        throw new ArgumentOutOfRangeException(nameof(column), "column must be >= -1");
      Path = path;
      Line = line;
      Column = column;
    }
    public string Path { get; }
    public int Line { get; }
    public int Column { get; }
  }
  /// <summary>
  /// Struct that stores information about a Diagnostic appearing in a source
  /// </summary>
  public struct DiagnosticResult {
    private DiagnosticResultLocation[] locations;
    public DiagnosticResultLocation[] Locations {
      get => locations ??= new DiagnosticResultLocation[] { };
      set => locations = value;
    }
    public DiagnosticSeverity Severity { get; set; }
    public string Id { get; set; }
    public string Message { get; set; }
    public string Path => Locations.Length > 0 ? Locations[0].Path : "";
    public int? Line => Locations.Length > 0 ? Locations[0].Line : new int?();
    public int? Column => Locations.Length > 0 ? Locations[0].Column : new int?();
  }
}
