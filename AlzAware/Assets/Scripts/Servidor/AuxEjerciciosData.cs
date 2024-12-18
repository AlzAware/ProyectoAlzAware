using SQLite;

[Table("auxEjercicios")]
public class AuxEjerciciosData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string NombreEjercicio { get; set; }
}
