using SQLite;

[Table("estadisticas")]
public class EstadisticasData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public int IdEjercicio { get; set; }
    public int Puntuacion { get; set; }
    public string Fecha { get; set; } 
}
