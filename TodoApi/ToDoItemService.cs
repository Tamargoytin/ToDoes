using System.Collections.Generic;
using System.Linq;
using TodoApi;
public class ToDoItemService
{
    private readonly ToDoDbContext _dbContext;

    public ToDoItemService(ToDoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Item> GetAllItems()
    {
        return _dbContext.Items.ToList();
    }

    public Item GetItemById(int id)
    {
        return _dbContext.Items.Find(id);
    }

    public void AddItem(Item item)
    {
        _dbContext.Items.Add(item);
        _dbContext.SaveChanges();
    }

    public void UpdateItem(Item item)
    {
        _dbContext.Items.Update(item);
        _dbContext.SaveChanges();
    }

    public void DeleteItem(int id)
    {
        var item = _dbContext.Items.Find(id);
        if (item != null)
        {
            _dbContext.Items.Remove(item);
            _dbContext.SaveChanges();
        }
    }
}
