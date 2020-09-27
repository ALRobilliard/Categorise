using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
    /// <summary>
    /// Config Setting entity model.
    /// </summary>
    public class ConfigSetting : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Config Setting name.
        /// </summary>
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Config Setting value.
        /// </summary>
        [Required]
        public string Value { get; set; }
    }
}