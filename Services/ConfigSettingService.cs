using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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

    /// <summary>
    /// Creates a ConfigSetting.
    /// </summary>
    void CreateConfigSetting(string name, string value, bool safeCreate);
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

    /// <summary>
    /// Creates a ConfigSetting. Duplicate error is ignored if using safeCreate.
    /// </summary>
    public void CreateConfigSetting(string name, string value, bool safeCreate = false)
    {
      bool alreadyExists = _context.ConfigSettings.Where(s => s.Name == name).ToList().Count > 0;

      if (alreadyExists && !safeCreate) {
        throw new ArgumentException($"A ConfigSetting with name: {name} already exists.");
      }
      else if (!alreadyExists)
      {
        ConfigSetting config = new ConfigSetting
        {
          Name = name,
          Value = value
        };
  
        _context.ConfigSettings.Add(config);
        _context.SaveChanges();
        }
    }
  }
}