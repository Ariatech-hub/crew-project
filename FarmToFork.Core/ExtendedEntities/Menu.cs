namespace FarmToFork.Core.Entities;
public partial class Menu
{
    [NotMapped]
    public IEnumerable<Menu> Menus { get; set; } = new List<Menu>();

    [NotMapped]
    public bool IsSelected { get; set; }
}