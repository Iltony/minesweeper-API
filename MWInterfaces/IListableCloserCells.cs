namespace MWInterfaces
{
    public interface IListableCloserCells
    {
        int CloserMinesNumber { get; set; }

        int GetCloserMines();
    }
}
