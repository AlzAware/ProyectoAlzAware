using SQLite;

[Table("usuarios")]
public class UsuarioData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Usuario { get; set; }
    public string Contrasena { get; set; }
    public string CorreoElectronico { get; set; }
}
