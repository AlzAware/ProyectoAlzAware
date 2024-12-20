using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SQLiteManager : MonoBehaviour
{
    private string dbPath;
    public bool IsInitialized { get; private set; } = false;

    void Start()
    {
        // Define la ruta de la base de datos
        dbPath = Path.Combine(Application.persistentDataPath, "servidor.db");

        // Muestra la ruta en la consola
        Debug.Log("Database path: " + dbPath);

        // Verifica si la base de datos ya existe
        if (File.Exists(dbPath))
        {
            Debug.Log("Base de datos encontrada en: " + dbPath);
        }
        else
        {
            Debug.LogWarning("La base de datos no existe, se creará ahora...");
            // Crea la base de datos y las tablas
            CreateDatabase();
        }

        // Inicialización de SQLiteManager
        dbPath = Path.Combine(Application.persistentDataPath, "servidor.db");
        Debug.Log("SQLiteManager inicializado en: " + dbPath);

        IsInitialized = true;

    }

    private void CreateDatabase()
    {
        var db = new SQLiteConnection(dbPath);

        // Crear tabla usuarios
        db.CreateTable<UsuarioData>();
        Debug.Log("Tabla 'usuarios' creada en la base de datos.");

        // Crear tabla estadisticas
        db.CreateTable<EstadisticasData>();
        Debug.Log("Tabla 'estadisticas' creada en la base de datos.");

        // Crear tabla auxEjercicios
        db.CreateTable<AuxEjerciciosData>();
        Debug.Log("Tabla 'auxEjercicios' creada en la base de datos.");

        // Insertar datos iniciales en auxEjercicios
        InsertarDatosInicialesAuxEjercicios(db);
    }

    private void InsertarDatosInicialesAuxEjercicios(SQLiteConnection db)
    {
        // Verificar si ya existen datos en la tabla
        var existeDatos = db.Table<AuxEjerciciosData>().Any();

        if (!existeDatos) // Si no hay datos, insertar los valores iniciales
        {
            var ejercicios = new[]
            {
                new AuxEjerciciosData { NombreEjercicio = "Sopa de Letras" },
                new AuxEjerciciosData { NombreEjercicio = "Busca Objetos" },
                new AuxEjerciciosData { NombreEjercicio = "Parejas" },
                new AuxEjerciciosData { NombreEjercicio = "Puzzle" },
                new AuxEjerciciosData { NombreEjercicio = "Refranes" },
                new AuxEjerciciosData { NombreEjercicio = "Figuras" }
            };

            db.InsertAll(ejercicios);
            Debug.Log("Datos iniciales insertados en 'auxEjercicios'.");
        }
        else
        {
            Debug.Log("La tabla 'auxEjercicios' ya tiene datos.");
        }
    }

    public void InsertUsuario(string usuario, string contrasena, string correo)
    {
        var db = new SQLiteConnection(dbPath);
        var nuevoUsuario = new UsuarioData
        {
            Usuario = usuario,
            Contrasena = contrasena,
            CorreoElectronico = correo
        };
        db.Insert(nuevoUsuario);
        Debug.Log("Usuario insertado: " + usuario);
    }

    public bool CheckLogin(string usuario, string contrasena)
    {
        var db = new SQLiteConnection(dbPath);
        var query = db.Table<UsuarioData>()
            .Where(u => (u.Usuario == usuario || u.CorreoElectronico == usuario) && u.Contrasena == contrasena);

        return query.Any();
    }

    public void InsertEstadistica(int idUsuario, int idEjercicio, int puntuacion, string fecha)
    {
        var db = new SQLiteConnection(dbPath);
        var nuevaEstadistica = new EstadisticasData
        {
            IdUsuario = idUsuario,
            IdEjercicio = idEjercicio,
            Puntuacion = puntuacion,
            Fecha = fecha
        };
        db.Insert(nuevaEstadistica);
        Debug.Log("Estadística guardada: IDUsuario=" + idUsuario + ", IDEjercicio=" + idEjercicio + ", Puntuación=" + puntuacion);
    }

    public List<(string fecha, int puntuacion)> ObtenerMejoresEstadisticas(int idEjercicio)
    {
        var estadisticas = new List<(string fecha, int puntuacion)>();

        // Abre la conexión con la base de datos
        using (var db = new SQLiteConnection(dbPath))
        {
            // Consulta para obtener las 7 mejores puntuaciones ordenadas de mayor a menor
            var resultados = db.Table<EstadisticasData>()
                               .Where(e => e.IdEjercicio == idEjercicio)
                               .OrderByDescending(e => e.Fecha)
                               .Take(7)
                               .ToList();

            // Añadir los resultados a la lista
            foreach (var resultado in resultados)
            {
                estadisticas.Add((resultado.Fecha, resultado.Puntuacion));
            }
        }

        return estadisticas;
    }

    public void CambiarContrasena(string nombreUsuario, string nuevaContrasena)
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            var usuario = db.Table<UsuarioData>().FirstOrDefault(u => u.Usuario == nombreUsuario);

            if (usuario != null)
            {
                usuario.Contrasena = nuevaContrasena;
                db.Update(usuario);
                Debug.Log($"Contraseña actualizada para el usuario: {nombreUsuario}");
            }
            else
            {
                throw new System.Exception($"Usuario '{nombreUsuario}' no encontrado.");
            }
        }
    }

    // Elimina un usuario si el nombre de usuario y la contraseña coinciden.
    // True si se eliminó el usuario, False si no coincide o no existe.
    public bool EliminarUsuario(string usuario, string contrasena)
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            // Verifica si existe el usuario con la contraseña dada
            var usuarioExistente = db.Table<UsuarioData>()
                .FirstOrDefault(u => u.Usuario == usuario && u.Contrasena == contrasena);

            if (usuarioExistente != null)
            {
                db.Delete(usuarioExistente); // Elimina al usuario
                Debug.Log($"Usuario eliminado: {usuario}");
                return true;
            }
            else
            {
                Debug.LogWarning($"Usuario no encontrado o contraseña incorrecta: {usuario}");
                return false;
            }
        }
    }


}
