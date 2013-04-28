// Type: Microsoft.Phone.Shell.ShellTile
// Assembly: Microsoft.Phone, Version=8.0.0.0, Culture=neutral, PublicKeyToken=24eec0d8c86cda1e
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\WindowsPhone\v8.0\Microsoft.Phone.dll

using System;
using System.Collections.Generic;
using System.Security;

namespace Microsoft.Phone.Shell
{
  /// <summary>
  /// Manages the Application Tile and secondary Tiles for an application.
  /// </summary>
  public sealed class ShellTile
  {
    /// <summary>
    /// Creates a new secondary Tile.
    /// </summary>
    /// <param name="navigationUri">URI for the Tile being created. The URI can contain custom launch parameters.</param><param name="initialData">Text and image information for the Tile being created.</param>
    [SecuritySafeCritical]
    public static void Create(Uri navigationUri, ShellTileData initialData);
    /// <summary>
    /// Creates a new secondary Tile.
    /// </summary>
    /// <param name="navigationUri">URI for the Tile being created. The URI can contain custom launch parameters.</param><param name="initialData">Text and image information for the Tile being created.</param><param name="supportsWideTile">true if the wide tile size is supported; otherwise, false.</param>
    [SecuritySafeCritical]
    public static void Create(Uri navigationUri, ShellTileData initialData, bool supportsWideTile);
    /// <summary>
    /// Updates an Application Tile or secondary Tile.
    /// </summary>
    /// <param name="data">New text and image data for the Tile.</param>
    [SecuritySafeCritical]
    public void Update(ShellTileData data);
    /// <summary>
    /// Unpins and deletes a secondary Tile.
    /// </summary>
    [SecuritySafeCritical]
    public void Delete();
    /// <summary>
    /// This API is not intended to be used directly from your code.
    /// </summary>
    /// 
    /// <returns>
    /// This API is not intended to be used directly from your code.
    /// </returns>
    /// <param name="str">This API is not intended to be used directly from your code.</param>
    public static string ConvertToXMLFormat(string str);
    /// <summary>
    /// Gets the URI and launch parameter this is navigated to when a secondary Tile is tapped.
    /// </summary>
    /// 
    /// <returns>
    /// The URI and launch parameter this is navigated to when a secondary Tile is tapped.
    /// </returns>
    public Uri NavigationUri { get; }
    /// <summary>
    /// Gets the collection of an application’s Tiles pinned to Start.
    /// </summary>
    /// 
    /// <returns>
    /// The collection of an application’s Tiles pinned to Start.
    /// </returns>
    public static IEnumerable<ShellTile> ActiveTiles { get; }
  }
}
