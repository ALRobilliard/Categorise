using System;
using System.Collections.Generic;
using System.Linq;
using CategoriseApi.Models;

namespace CategoriseApi.Services
{
  /// <summary>
  /// Service for exposing common actions for ConfigSetting.
  /// </summary>
  public interface IConfigSettingService
  {
    /// <summary>
    /// Retrieves a ConfigSetting by setting name.
    /// </summary>
    ConfigSetting GetConfigSettingByName(string name);
  }

  /// <summary>
  /// Service for exposing common actions for ConfigSetting.
  /// </summary>
  public class ConfigSettingService : IConfigSettingService
  {
    CategoriseContext _context { get; set; }

    /// <summary>
    /// Constructor for the ConfigSettingService.
    /// </summary>
    public ConfigSettingService(CategoriseContext context)
    {
      _context = context;
    }

    /// <summary>
    /// Retrieves the newest created ConfigSetting by setting name.
    /// </summary>
    public ConfigSetting GetConfigSettingByName(string name)
    {
      return _context.ConfigSettings
        .Where(s => s.Name == name)
        .OrderByDescending(s => s.CreatedOn)
        .FirstOrDefault();
    }
  }
}